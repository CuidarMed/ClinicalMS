using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGetEncounterRangeService
    {
        Task<IEnumerable<EncounterResponse>> GetEncounterRangeAsync(long patientId, DateTime from, DateTime to);
    }
}
