using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrescriptionQuery
    {
        Task<IEnumerable<Prescription>> GetByPatientIdAsync(long patientId);
        Task<IEnumerable<Prescription>> GetByDoctorIdAsync(long doctorId);
        Task<Prescription> GetByIdAsync(int prescriptionId);
        Task<IEnumerable<Prescription>> GetByEncounterIdAsync(int encounterId);
    }
}



