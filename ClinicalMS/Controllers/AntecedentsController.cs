using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/patients/{patientId}/antedecents")]
    public class AntedecentsController : ControllerBase
    {
        private readonly IDeleteAntecedentService deleteAntecedent;

        public AntedecentsController(IDeleteAntecedentService deleteAntecedent)
        {
            this.deleteAntecedent = deleteAntecedent;
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
    }
}
