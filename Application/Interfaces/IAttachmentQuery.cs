using Domain.Entities;


namespace Application.Interfaces
{
    public interface IAttachmentQuery
    {
        Task<IEnumerable<Attachment>> GetAttachmentsByPatientAsync(long patientId);
    }
}
