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
    public class GetEncounterRangeService : IGetEncounterRangeService
    {
        private readonly IEncounterQuery query;

        public GetEncounterRangeService(IEncounterQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<EncounterResponse>> GetEncounterRangeAsync(long patientId, DateTime from, DateTime to)
        {
            var encounters = await query.GetByPatientAsync(patientId);

            if (encounters == null || !encounters.Any())
                return Enumerable.Empty<EncounterResponse>();

             
            var filtered = encounters
                .Where(e => e.Date >= from && e.Date <= to)
                // Incluir todos los estados válidos: COMPLETED, SIGNED, OPEN
                // COMPLETED es el estado que se usa cuando se completa una consulta
                .Where(e => e.Status == "OPEN" || e.Status == "SIGNED" || e.Status == "COMPLETED" || 
                           e.Status == "open" || e.Status == "signed" || e.Status == "completed")
                .ToList();

            
            var responce = filtered.Select(e => new EncounterResponse(
                e.EncounterId,
                e.PatientId,
                e.DoctorId,
                e.AppointmentId,
                e.Reasons,
                e.Subjective,
                e.Objetive,
                e.Assessment,
                e.Plan,
                e.Notes,
                e.Status,
                e.Date,
                e.CreatedAt,
                e.UpdatedAt
            ));

            return responce;

        }
    }
}
