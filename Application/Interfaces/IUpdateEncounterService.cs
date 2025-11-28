using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUpdateEncounterService
    {
        Task<UpdateEncounterResponse> UpdateAsync(int encounterId, UpdateEncounterRequest request);
    }
}
