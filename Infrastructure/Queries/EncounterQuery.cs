using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class EncounterQuery : IEncountersQuery
    {
        private readonly AppDbContext _context;

        public EncounterQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Encounter>> GetAllEncounter()
            => await _context.Encounters.AsNoTracking()
                .ToListAsync();

        public async Task<Encounter> GetEnocunterById(int id)
        {     
            Encounter? encounter = await _context.Encounters.AsNoTracking()
                                        .Include(e => e.attachments)
                                        .FirstOrDefaultAsync(e => e.EncounterId == id);
            return encounter;
        }
    }
}
