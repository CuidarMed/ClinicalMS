using Domain.Entities;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IEncounterCommand
    {
        Task<int> InsertAsync(Encounter encounter);
        Task SignEncounter(int id, long DoctorId, EncounterSign sign);
        Task UpdateEncounter(Encounter encounter);
    }
}
