using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalMS.Controllers
{
    [Route("api/v1/patients/{patientId}/encounters")]
    [ApiController]
    public class EncounterController : ControllerBase
    {
        private readonly IGetEncounterRangeService encounterRangeService;
        private readonly ICreateEncounterService createEncounter;

        public EncounterController(IGetEncounterRangeService encounterRangeService,ICreateEncounterService createEncounter)
        {
            this.encounterRangeService = encounterRangeService;
            this.createEncounter = createEncounter;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EncounterResponce>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEncountersByRange(long patientId, DateTime from, DateTime to)
        {

            try
            {
                var result = await encounterRangeService.GetEncounterRangeAsync(patientId, from, to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,new { message = ex.Message });

            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(EncounterResponce), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEncounter(long patientId, [FromBody] CreateEncounterRequest request)
        {
            try
            {
                var encounter = await createEncounter.CreateAsync(request);

                return Created(string.Empty, encounter);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

    }
}
