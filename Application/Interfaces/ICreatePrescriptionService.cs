using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICreatePrescriptionService
    {
        Task<PrescriptionResponse> CreateAsync(CreatePrescriptionRequest request);
    }
}




