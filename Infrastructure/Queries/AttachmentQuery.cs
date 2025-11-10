using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Queries
{
    public class AttachmentQuery : IAttachmentQuery
    {
        private readonly AppDbContext context;

        public AttachmentQuery(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Attachment>> GetAttachmentsByPatientAsync(long patientId)
        {
            var attachments = await context.Attachments
                .AsNoTracking()
                .Where(a => a.PatientId == patientId)
                .ToListAsync();

            return attachments;


        }
    }
}
