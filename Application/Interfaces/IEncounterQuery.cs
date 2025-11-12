using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEncounterQuery
    {
        Task<IEnumerable<Encounter>> GetByPatientAsync(long patientId);
        Task<List<Encounter>> GetAllEncounter();
        Task<Encounter> GetEncounterById(int encounterId);
        Task<IEnumerable<Encounter>> GetByAppointmentIdAsync(long appointmentId);
    }
}
