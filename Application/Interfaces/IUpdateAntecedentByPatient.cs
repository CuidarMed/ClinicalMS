using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUpdateAntecedentByPatient
    {
        Task<AntecedentResponse> UpdateAntecedentByPatientAsync(long patientId, int encounterId, AntecedentUpdate update);
    }
}
