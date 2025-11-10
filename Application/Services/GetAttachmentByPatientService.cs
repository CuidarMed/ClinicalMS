using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetAttachmentByPatientService : IGetAttachmentByPatientService
    {
        private readonly IAttachmentQuery query;

        public GetAttachmentByPatientService(IAttachmentQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<AttachmentResponce>> GetAllByPatientAsync(long patientId, int? encounterId = null)
        {
            var attachments = await query.GetAttachmentsByPatientAsync(patientId);

            if (attachments == null)
            {
                return null;
            }

            var filtered = encounterId.HasValue
                ? attachments.Where(a => a.EncounterId == encounterId.Value)
                : attachments;

            var attachmentResponces = attachments.Select(a => new AttachmentResponce
            (
                a.AttachmentId,
                a.PatientId,
                a.EncounterId,
                a.FileName,
                a.FileType,
                a.FileUrl,
                a.Notes,
                a.CreatedAt

            ));
            return attachmentResponces;
        }
    }
}
