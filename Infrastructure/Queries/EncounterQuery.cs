using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Queries
{
    public class EncounterQuery : IEnconunterQuery
    {
        private readonly AppDbContext context;

        public EncounterQuery(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Encounter?>> GetByPatientAsync(long patientId)
        {
            var encounters =await context.Encounters
                .AsNoTracking()
                .Where(e => e.PatientId == patientId)
                .ToListAsync();
            return encounters;
            
        }

    }
}
