using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Reflection;

namespace ClinicalMS.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EncounterController : ControllerBase
    {
        private readonly ISearchEncounterService _searchEncounterService;
        private readonly ISignEncouterService _signEncouterService;
        private readonly IGetEncounterRangeService _encounterRangeService;
        private readonly ICreateEncounterService _createEncounter;

        public EncounterController(ISearchEncounterService searchEncounterService, ISignEncouterService signEncouterService, IGetEncounterRangeService getEncounterRange, ICreateEncounterService createEncounter)
        {
            _searchEncounterService = searchEncounterService;
            _signEncouterService = signEncouterService;
            _encounterRangeService = getEncounterRange;
            _createEncounter = createEncounter;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EncounterResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEncountersByRange(
            [FromQuery] long? patientId, 
            [FromQuery] DateTime? from, 
            [FromQuery] DateTime? to,
            [FromQuery] long? appointmentId)
        {
            try
            {
                // Si se proporciona appointmentId, buscar por appointmentId
                if (appointmentId.HasValue)
                {
                    var encounterQuery = HttpContext.RequestServices.GetRequiredService<Application.Interfaces.IEncounterQuery>();
                    var encounters = await encounterQuery.GetByAppointmentIdAsync(appointmentId.Value);
                    
                    if (encounters == null || !encounters.Any())
                        return Ok(Enumerable.Empty<EncounterResponse>());

                    var response = encounters.Select(e => new EncounterResponse(
                        e.EncounterId,
                        e.PatientId,
                        e.DoctorId,
                        e.AppointmentId,
                        e.Reasons,
                        e.Subjective,
                        e.Objetive,
                        e.Assessment,
                        e.Plan,
                        e.Notes,
                        e.Status,
                        e.Date,
                        e.CreatedAt,
                        e.UpdatedAt
                    ));
                    
                    return Ok(response);
                }

                // Si se proporciona patientId, from y to, buscar por rango
                if (patientId.HasValue && from.HasValue && to.HasValue)
                {
                    var result = await _encounterRangeService.GetEncounterRangeAsync(patientId.Value, from.Value, to.Value);
                    return Ok(result);
                }

                return BadRequest(new { message = "Debe proporcionar appointmentId o (patientId, from, to)" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(EncounterResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEncounter(long patientId, [FromBody] CreateEncounterRequest request)
        {
            try
            {
                var encounter = await _createEncounter.CreateAsync(request);

                // Generar resumen HL7 en background (no bloquear la respuesta)
                var httpClientFactory = HttpContext.RequestServices.GetRequiredService<IHttpClientFactory>();
                var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                var logger = HttpContext.RequestServices.GetRequiredService<ILogger<EncounterController>>();
                _ = Task.Run(async () => await GenerateHl7SummaryAsync(encounter, request, httpClientFactory, configuration, logger));

                return Created(string.Empty, encounter);
            }
            catch (Application.Exceptions.ConflictException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        private async Task GenerateHl7SummaryAsync(EncounterResponse encounter, CreateEncounterRequest request, 
            IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<EncounterController> logger)
        {
            try
            {

                var hl7GatewayUrl = configuration["Hl7Gateway:BaseUrl"] ?? "http://localhost:5000";
                var httpClient = httpClientFactory.CreateClient();
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                // Obtener información del paciente desde DirectoryMS
                var directoryMsUrl = configuration["DirectoryMS:BaseUrl"] ?? "http://localhost:8081";
                var directoryClient = httpClientFactory.CreateClient();
                directoryClient.BaseAddress = new Uri(directoryMsUrl);

                var patient = await directoryClient.GetFromJsonAsync<dynamic>($"/api/v1/Patient/{encounter.PatientId}");
                var doctor = await directoryClient.GetFromJsonAsync<dynamic>($"/api/v1/Doctor/{encounter.DoctorId}");

                // Obtener información del appointment desde SchedulingMS
                var schedulingMsUrl = configuration["SchedulingMS:BaseUrl"] ?? "http://localhost:8083";
                var schedulingClient = httpClientFactory.CreateClient();
                schedulingClient.BaseAddress = new Uri(schedulingMsUrl);

                var appointment = await schedulingClient.GetFromJsonAsync<dynamic>($"/api/v1/Appointments/{encounter.AppoinmentId}");

                // Construir request para Hl7Gateway
                var summaryRequest = new
                {
                    EncounterId = encounter.EncounterId,
                    PatientId = encounter.PatientId,
                    DoctorId = encounter.DoctorId,
                    AppointmentId = encounter.AppoinmentId,
                    PatientDni = GetPropertyValue(patient, "dni") ?? GetPropertyValue(patient, "Dni"),
                    PatientFirstName = GetPropertyValue(patient, "firstName") ?? GetPropertyValue(patient, "FirstName"),
                    PatientLastName = GetPropertyValue(patient, "lastName") ?? GetPropertyValue(patient, "LastName"),
                    PatientDateOfBirth = GetDateTimeProperty(patient, "dateOfBirth") ?? GetDateTimeProperty(patient, "DateOfBirth"),
                    PatientPhone = GetPropertyValue(patient, "phone") ?? GetPropertyValue(patient, "Phone"),
                    PatientAddress = GetPropertyValue(patient, "adress") ?? GetPropertyValue(patient, "Adress"),
                    DoctorFirstName = GetPropertyValue(doctor, "firstName") ?? GetPropertyValue(doctor, "FirstName"),
                    DoctorLastName = GetPropertyValue(doctor, "lastName") ?? GetPropertyValue(doctor, "LastName"),
                    DoctorSpecialty = GetPropertyValue(doctor, "specialty") ?? GetPropertyValue(doctor, "Specialty"),
                    AppointmentStartTime = GetDateTimeOffsetProperty(appointment, "startTime") ?? GetDateTimeOffsetProperty(appointment, "StartTime"),
                    AppointmentEndTime = GetDateTimeOffsetProperty(appointment, "endTime") ?? GetDateTimeOffsetProperty(appointment, "EndTime"),
                    AppointmentReason = GetPropertyValue(appointment, "reason") ?? GetPropertyValue(appointment, "Reason"),
                    EncounterReasons = encounter.Reasons,
                    EncounterAssessment = encounter.Assessment,
                    EncounterDate = encounter.Date
                };

                // Llamar al Hl7Gateway (usar URL completa)
                var generateUrl = $"{hl7GatewayUrl}/api/v1/Hl7Summary/generate";
                logger.LogInformation("Llamando a Hl7Gateway para generar resumen: {Url}", generateUrl);
                var response = await httpClient.PostAsJsonAsync(generateUrl, summaryRequest);
                if (response.IsSuccessStatusCode)
                {
                    logger.LogInformation("Resumen HL7 generado exitosamente para EncounterId: {EncounterId}", encounter.EncounterId);
                }
                else
                {
                    logger.LogWarning("Error generando resumen HL7 para EncounterId: {EncounterId}, Status: {Status}", 
                        encounter.EncounterId, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error generando resumen HL7 para EncounterId: {EncounterId}", encounter.EncounterId);
                // No lanzar excepción para no afectar la creación del encounter
            }
        }

        private static string? GetPropertyValue(dynamic? obj, string propertyName)
        {
            try
            {
                if (obj == null) return null;
                var prop = ((object)obj).GetType().GetProperty(propertyName);
                if (prop == null) return null;
                var value = prop.GetValue(obj);
                return value?.ToString();
            }
            catch
            {
                return null;
            }
        }

        private static DateTime? GetDateTimeProperty(dynamic? obj, string propertyName)
        {
            try
            {
                if (obj == null) return null;
                var prop = ((object)obj).GetType().GetProperty(propertyName);
                if (prop == null) return null;
                var value = prop.GetValue(obj);
                if (value is DateTime dt) return dt;
                if (value is DateTimeOffset dto) return dto.DateTime;
                if (DateTime.TryParse(value?.ToString(), out var parsed)) return parsed;
                return null;
            }
            catch
            {
                return null;
            }
        }

        private static DateTime? GetDateTimeOffsetProperty(dynamic? obj, string propertyName)
        {
            try
            {
                if (obj == null) return null;
                var prop = ((object)obj).GetType().GetProperty(propertyName);
                if (prop == null) return null;
                var value = prop.GetValue(obj);
                if (value is DateTimeOffset dto) return dto.DateTime;
                if (value is DateTime dt) return dt;
                if (DateTimeOffset.TryParse(value?.ToString(), out var parsed)) return parsed.DateTime;
                return null;
            }
            catch
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEncounterById(int id)
        {
            try
            {
                var result = await _searchEncounterService.SeachEncounterService(id);

                if (result == null)
                    return NotFound(new { message = "Cita no encontrada" });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}/sign")]
        public async Task<IActionResult> SignEncounter(int id, long doctorId, EncounterSign sign)
        {
            try
            {
                var result = await _signEncouterService.SignEncounter(id, doctorId, sign);
                if (result == null)
                    return NotFound(new { message = "Cita no encontrada" });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
