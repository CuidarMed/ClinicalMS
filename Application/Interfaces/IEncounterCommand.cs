using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEncounterCommand
    {
        Task SignEncounter(int id, long DoctorId, EncounterSign sign);
    }
}
