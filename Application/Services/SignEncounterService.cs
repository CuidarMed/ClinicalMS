using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SignEncounterService : ISignEncouterService
    {
        private readonly IEncounterQuery _query;
        private readonly IEncounterCommand _command;

        public SignEncounterService(IEncounterQuery query, IEncounterCommand command)
        {
            _query = query;
            _command = command;
        }

        public async Task<EncounterResponse> SignEncounter(int id, long doctorId, EncounterSign sign)
        {
            var encounter = await _query.GetEncounterById(id);

            if (encounter == null)
                throw new Exception("Cita no encontrada");

            if (encounter.Status != "Open")
                throw new Exception("La cita ya fue firmada y no puede modificarse");

            await _command.SignEncounter(id, doctorId, sign);

            encounter = await _query.GetEncounterById(id);

            return await Task.FromResult(new EncounterResponse(
                    EncounterId: encounter.EncounterId,
                    PatientId: encounter.PatientId,
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
