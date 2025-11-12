using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs;
using Application.Services;

namespace ClinicalMS.Controllers
{
    [ApiController]
    [Route("api/v1/patients/{patientId}/[controller]")]
    public class AntecedentController : ControllerBase
    {
        private readonly ISearchAntecedentService _searchAntecedentService;
        private readonly ICreateAntecedentService _createAntecedentService;

        public AntecedentController(ISearchAntecedentService searchAntecedentService, ICreateAntecedentService createAntecedentService)
        {
            _searchAntecedentService = searchAntecedentService;
            _createAntecedentService = createAntecedentService;
        }

        /// <summary>
        /// Obtiene todos los antecedentes clínicos de un paciente específico.
        /// </summary>
        /// <param name="patientId">Identificador del paciente</param>
        /// <returns>Lista de antecedentes clínicos</returns>
        [HttpGet]
        public async Task<IActionResult> GetAntecedentsByPatientId(long patientId)
        {
            try
            {
                var allAntecedents = await _searchAntecedentService.GetAllAsync();

                if (allAntecedents == null || !allAntecedents.Any())
                    return NotFound(new { message = "No se encontraron antecedentes clínicos." });

                var patientAntecedents = allAntecedents
                    .Where(a => a.PatientId == patientId)
                    .ToList();

                if (!patientAntecedents.Any())
                    return NotFound(new { message = "El paciente no tiene antecedentes clínicos registrados." });

                return Ok(patientAntecedents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al obtener antecedentes clínicos: {ex.Message}" });
            }
        }

        /// <summary>
        /// Crea un nuevo antecedente clínico para un paciente específico.
        /// </summary>
        /// <param name="patientId">Identificador del paciente</param>
        /// <param name="request">Datos del antecedente a crear</param>
        /// <returns>Antecedente creado</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAntecedent(long patientId, [FromBody] AntecedentRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { message = "El cuerpo de la solicitud no puede ser nulo." });

                var result = await _createAntecedentService.CreateAsync(patientId, request);

                return CreatedAtAction(
                    nameof(GetAntecedentsByPatientId),
                    new { patientId = result.PatientId },
                    result
                );
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error al crear el antecedente clínico: {ex.Message}" });
            }
        }
    }
}
