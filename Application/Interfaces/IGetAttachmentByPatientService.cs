using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGetAttachmentByPatientService 
    {
        Task<IEnumerable<AttachmentResponce>> GetAllByPatientAsync(long patientId, int? encounterId = null);
    }
}
