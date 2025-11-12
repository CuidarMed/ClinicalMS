using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreateEncounterService : ICreateEncounterService
    {
        private readonly IEncounterCommand command;
        private readonly IEncounterQuery query;

        public CreateEncounterService(IEncounterCommand command, IEncounterQuery query)
        {
            this.command = command;
            this.query = query;
        }
        public async Task<EncounterResponse> CreateAsync(CreateEncounterRequest request)
        {
            // Verificar si ya existe un encounter para este appointment
            var existingEncounters = await query.GetByAppointmentIdAsync(request.AppointmentId);
            if (existingEncounters != null && existingEncounters.Any())
            {
                var existingEncounter = existingEncounters.First();
                throw new ConflictException($"Ya existe un encuentro clínico para este turno (EncounterId: {existingEncounter.EncounterId}). No se puede crear otro.");
            }

            var encounter = new Encounter
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentId = request.AppointmentId,
                Reasons = request.Reasons,
                Subjective = request.Subjective,
                Objetive = request.Objetive,
                Assessment = request.Assessment,
                Plan = request.Plan,
                Notes = request.Notes,
                Status = request.Status,
                Date = request.Date
            };
            var encounterId = await command.InsertAsync(encounter);
            return new EncounterResponse(
                encounterId,
                encounter.PatientId,
                encounter.DoctorId,
                encounter.AppointmentId,
                encounter.Reasons,
                encounter.Subjective,
                encounter.Objetive,
                encounter.Assessment,
                encounter.Plan,
                encounter.Notes,
                encounter.Status,
                encounter.Date,
                encounter.CreatedAt,
                encounter.UpdatedAt
                );


        }
    }
}
