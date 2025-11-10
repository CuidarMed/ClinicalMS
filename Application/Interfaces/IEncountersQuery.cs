using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEncountersQuery
    {
        Task<List<Encounter>> GetAllEncounter();
        Task<Encounter> GetEnocunterById(int id);
    }
}
