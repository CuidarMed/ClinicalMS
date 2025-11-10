using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
