using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
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
        private readonly IMapper mapper;

        public GetAttachmentByPatientService(IAttachmentQuery query, IMapper mapper)
        {
            this.query = query;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<AttachmentResponse>> GetAllByPatientAsync(long patientId, int? encounterId = null)
        {
            var attachments = await query.GetAttachmentsByPatientAsync(patientId);

            if (attachments == null)
            {
                return null;
            }

            var filtered = encounterId.HasValue
                ? attachments.Where(a => a.EncounterId == encounterId.Value)
                : attachments;

            // Convertir Entidad => Responce
            return mapper.Map<IEnumerable<AttachmentResponse>>(attachments);
        }
    }
}
