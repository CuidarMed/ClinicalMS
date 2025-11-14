using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPrescriptionCommand
    {
        Task<int> InsertAsync(Prescription prescription);
    }
}



