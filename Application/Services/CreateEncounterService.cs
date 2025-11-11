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
    public class CreateEncounterService : ICreateEncounterService
    {
        private readonly IEncounterCommand command;

        public CreateEncounterService(IEncounterCommand command)
        {
            this.command = command;
        }
        public async Task<EncounterResponce> CreateAsync(CreateEncounterRequest request)
        {
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
            var encounterId =await command.InsertAsync(encounter);
            return new EncounterResponce(
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
