using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using System.Threading.Tasks;

namespace Infrastructure.Command
{
    public class PrescriptionCommand : IPrescriptionCommand
    {
        private readonly AppDbContext context;

        public PrescriptionCommand(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> InsertAsync(Prescription prescription)
        {
            await context.Prescriptions.AddAsync(prescription);
            await context.SaveChangesAsync();
            return prescription.PrescriptionId;
        }
    }
}




