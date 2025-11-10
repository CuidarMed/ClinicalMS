using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public interface ICreateEncounterService
    {
        Task<EncounterResponce> CreateAsync(CreateEncounterRequest request);
    }
}
