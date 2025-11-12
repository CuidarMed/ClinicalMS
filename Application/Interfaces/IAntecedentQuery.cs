using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAntecedentQuery
    {
        Task<Antecedent?> GetByIdAsync(long id);
        Task<List<Antecedent>> GetAllAsync();

    }
}
