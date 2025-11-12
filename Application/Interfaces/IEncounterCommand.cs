using Domain.Entities;
﻿using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEncounterCommand
    {
        Task<int> InsertAsync(Encounter encounter);
        Task SignEncounter(int id, long DoctorId, EncounterSign sign);
    }
}
