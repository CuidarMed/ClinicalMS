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
        private readonly IEnconunterQuery query;

        public GetEncounterRangeService(IEnconunterQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<EncounterResponce>> GetEncounterRangeAsync(long patientId, DateTime from, DateTime to)
        {
            var encounters = await query.GetByPatientAsync(patientId);

            if (encounters == null || !encounters.Any())
                return Enumerable.Empty<EncounterResponce>();

             
            var filtered = encounters
                .Where(e => e.Date >= from && e.Date <= to)
                .Where(e => e.Status == "OPEN" || e.Status == "SIGNED")
                .ToList();

            
            var responce = filtered.Select(e => new EncounterResponce(
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
