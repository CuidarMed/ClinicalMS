using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class PrescriptionQuery : IPrescriptionQuery
    {
        private readonly AppDbContext context;

        public PrescriptionQuery(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Prescription>> GetByPatientIdAsync(long patientId)
        {
            return await context.Prescriptions
                .Where(p => p.PatientId == patientId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetByDoctorIdAsync(long doctorId)
        {
            return await context.Prescriptions
                .Where(p => p.DoctorId == doctorId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }

        public async Task<Prescription> GetByIdAsync(int prescriptionId)
        {
            return await context.Prescriptions
                .FirstOrDefaultAsync(p => p.PrescriptionId == prescriptionId);
        }

        public async Task<IEnumerable<Prescription>> GetByEncounterIdAsync(int encounterId)
        {
            return await context.Prescriptions
                .Where(p => p.EncounterId == encounterId)
                .OrderByDescending(p => p.PrescriptionDate)
                .ToListAsync();
        }
    }
}




