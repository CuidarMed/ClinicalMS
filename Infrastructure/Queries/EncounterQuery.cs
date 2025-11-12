using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Queries
{
    public class EncounterQuery : IEncounterQuery
    {
        private readonly AppDbContext context;

        public EncounterQuery(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Encounter?>> GetByPatientAsync(long patientId)
        {
            var encounters = await context.Encounters
                .AsNoTracking()
                .Where(e => e.PatientId == patientId)
                .ToListAsync();
            return encounters;

        }

        public async Task<List<Encounter>> GetAllEncounter()
            => await context.Encounters.AsNoTracking()
                .ToListAsync();

        public async Task<Encounter> GetEncounterById(int encounterid)
        {
            Encounter? encounter = await context.Encounters.AsNoTracking()
                                        .Include(e => e.attachments)
                                        .FirstOrDefaultAsync(e => e.EncounterId == encounterid);
            return encounter;
        }

        public async Task<IEnumerable<Encounter>> GetByAppointmentIdAsync(long appointmentId)
        {
            var encounters = await context.Encounters
                .AsNoTracking()
                .Where(e => e.AppointmentId == appointmentId)
                .ToListAsync();
            return encounters;
        }
    }
}
