using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ClinicalMS.Controllers
{
    [Route("api/v1/patients/{patientId}/attachments")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IGetAttachmentByPatientService attachmentByPatientService;

        public AttachmentController(IGetAttachmentByPatientService attachmentByPatientService)
        {
            this.attachmentByPatientService = attachmentByPatientService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Application.DTOs.AttachmentResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttachmentsByPatient(long patientId, [FromQuery] int? encounterId = null)
        {
            try
            {
                var attachments = await attachmentByPatientService.GetAllByPatientAsync(patientId, encounterId);
                if (attachments == null)
                {
                    return Ok(Enumerable.Empty<Application.DTOs.AttachmentResponse>());
                }
                return Ok(attachments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
