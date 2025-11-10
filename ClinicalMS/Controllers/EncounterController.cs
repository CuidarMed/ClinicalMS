using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
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

        public EncounterController(ISearchEncounterService searchEncounterService, ISignEncouterService signEncouterService)
        {
            _searchEncounterService = searchEncounterService;
            _signEncouterService = signEncouterService;
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
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}/sign")]
        public async Task<IActionResult> SignEncounter(int id ,long doctorId, EncounterSign sign) {
            try
            {
                var result = await _signEncouterService.SignEncounter(id, doctorId, sign);
                if (result == null)
                    return NotFound(new { message = "Cita no encontrada" });

                return new JsonResult(result);
            }
            catch (Exception ex) {
                return BadRequest(new {message = ex.Message});
            } 
        }
    }
}
