using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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
        public async Task<IActionResult> GetEncounters([FromQuery] long? patientId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] long? appointmentId)
        {
            // Si se proporciona appointmentId, buscar por appointmentId
            if (appointmentId.HasValue)
            {
                var encounterQuery = HttpContext.RequestServices.GetRequiredService<IEncounterQuery>();
                var encounters = await encounterQuery.GetByAppointmentIdAsync(appointmentId.Value);
                
                // Mapear a DTOs
                var mapper = HttpContext.RequestServices.GetRequiredService<AutoMapper.IMapper>();
                var result = encounters.Select(e => mapper.Map<EncounterResponse>(e));
                return Ok(result);
            }
            
            // Si se proporcionan patientId, from y to, buscar por rango
            if (patientId.HasValue && from.HasValue && to.HasValue)
            {
                var result = await _encounterRangeService.GetEncounterRangeAsync(patientId.Value, from.Value, to.Value);
                return Ok(result);
            }
            
            return BadRequest("Debe proporcionar appointmentId o (patientId, from, to)");
        }

        [HttpPost]
        [ProducesResponseType(typeof(EncounterResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEncounter(long patientId, [FromBody] CreateEncounterRequest request)
        {
             var encounter = await _createEncounter.CreateAsync(request);
             return Created(string.Empty, encounter);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEncounterById(int id)
        {
             var result = await _searchEncounterService.SeachEncounterService(id);
             return new JsonResult(result);   
        }

        [HttpPatch("{id}/sign")]
        public async Task<IActionResult> SignEncounter(int id, long doctorId, EncounterSign sign)
        {
             var result = await _signEncouterService.SignEncounter(id, doctorId, sign);             
             return new JsonResult(result);
        }

    }
}
