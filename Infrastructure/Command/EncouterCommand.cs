using Application.DTOs;
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
    public class EncouterCommand : IEncounterCommand
    {
        private readonly AppDbContext _appDbContext;

        public EncouterCommand(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task SignEncounter(int id, long DoctorId, EncounterSign sign)
        {
            var encounter = new Encounter {
                EncounterId = id,
                DoctorId = DoctorId,
                Status = sign.Status,
                Notes = sign.Notes,
            };

            _appDbContext.Encounters.Attach(encounter);
            _appDbContext.Entry(encounter).Property(e => e.DoctorId).IsModified = true;
            _appDbContext.Entry(encounter).Property(e => e.Status).IsModified = true;
            _appDbContext.Entry(encounter).Property(e => e.Notes).IsModified = true;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
