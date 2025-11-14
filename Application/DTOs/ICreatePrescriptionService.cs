using System.Threading.Tasks;

namespace Application.DTOs
{
    public interface ICreatePrescriptionService
    {
        Task<PrescriptionResponse> CreateAsync(CreatePrescriptionRequest request);
    }
}



