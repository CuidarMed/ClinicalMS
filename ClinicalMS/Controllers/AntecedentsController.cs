using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/patients/{patientId}/antedecents")]
    public class AntedecentsController : ControllerBase
    {
        private readonly IDeleteAntecedentService deleteAntecedent;
        private readonly IUpdateAntecedentByPatient updateAntecedent;

        public AntedecentsController(IDeleteAntecedentService deleteAntecedent, IUpdateAntecedentByPatient updateAntecedent)
        {
            this.deleteAntecedent = deleteAntecedent;
            this.updateAntecedent = updateAntecedent;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAntedecent(long patientId, int id)
        {

            try
            {
                var deleted = await deleteAntecedent.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAntecedent(long patientId, int antecedentId, AntecedentUpdate antecedentUpdate)
        {
            try
            {
                var result = await updateAntecedent.UpdateAntecedentByPatientAsync(patientId, antecedentId, antecedentUpdate);
                if (result == null)
                    return NotFound(new { message = "Antecedente no encontrado" });

                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
