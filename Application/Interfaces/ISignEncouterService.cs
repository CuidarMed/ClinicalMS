using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISignEncouterService
    {
        Task<EncounterResponse> SignEncounter(int id, long doctorId, EncounterSign sign);
    }
}
