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
             var deleted = await deleteAntecedent.DeleteAsync(id);
             return NoContent();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAntecedent(long patientId, int antecedentId, AntecedentUpdate antecedentUpdate)
        {
            
             var result = await updateAntecedent.UpdateAntecedentByPatientAsync(patientId, antecedentId, antecedentUpdate);
             return new JsonResult(result);
        }
    }
}
