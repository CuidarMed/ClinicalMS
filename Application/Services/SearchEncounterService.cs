using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SearchEncounterService : ISearchEncounterService
    {
        private readonly IEncountersQuery _encountersQuery;

        public SearchEncounterService(IEncountersQuery encountersQuery)
        {
            _encountersQuery = encountersQuery;
        }

        public async Task<EncounterResponse> SeachEncounterService(int id)
        {
            var encounter = await _encountersQuery.GetEnocunterById(id);

            if (encounter == null)
            {
                throw new Exception("No se encontro la cita.");
            }

            if (encounter.Status == "Open")
                throw new Exception("La cita esta en curso o todavia no se realizo");
            else
                return await Task.FromResult(new EncounterResponse(
                        EncounterId: encounter.EncounterId,
                        PatientId: encounter.PateientId,
                        DoctorId: encounter.DoctorId,
                        AppoinmentId: encounter.AppointmentId,
                        Reasons: encounter.Reasons,
                        Subjetive: encounter.Subjective,
                        Objetive: encounter.Objetive,
                        Assessment: encounter.Assessment,
                        Plan: encounter.Plan,
                        Notes: encounter.Notes,
                        Status: encounter.Status,
                        Date: encounter.Date,
                        createdAt: encounter.CreatedAt,
                        UpdatedAt: encounter.UpdatedAt
                    ));
        }
    }
}
