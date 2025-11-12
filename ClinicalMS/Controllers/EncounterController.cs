using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetEncountersByRange(long patientId, DateTime from, DateTime to)
        {

            try
            {
                var result = await _encounterRangeService.GetEncounterRangeAsync(patientId, from, to);
                return Ok(result);
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

                return Created(string.Empty, encounter);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
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
