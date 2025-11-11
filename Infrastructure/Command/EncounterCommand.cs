using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class EncounterCommand : IEncounterCommand
    {
        private readonly AppDbContext context;

        public EncounterCommand(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> InsertAsync(Encounter encounter)
        {
            await context.Encounters.AddAsync(encounter);
            await context.SaveChangesAsync();
            return encounter.EncounterId;
        }
    }
}
