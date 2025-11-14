using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreatePrescriptionService : ICreatePrescriptionService
    {
        private readonly IPrescriptionCommand command;

        public CreatePrescriptionService(IPrescriptionCommand command)
        {
            this.command = command;
        }

        public async Task<PrescriptionResponse> CreateAsync(CreatePrescriptionRequest request)
        {
            var prescription = new Prescription
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                EncounterId = request.EncounterId,
                Diagnosis = request.Diagnosis,
                Medication = request.Medication,
                Dosage = request.Dosage,
                Frequency = request.Frequency,
                Duration = request.Duration,
                AdditionalInstructions = request.AdditionalInstructions,
                PrescriptionDate = DateTime.UtcNow
            };

            var prescriptionId = await command.InsertAsync(prescription);
            
            return new PrescriptionResponse(
                prescriptionId,
                prescription.PatientId,
                prescription.DoctorId,
                prescription.EncounterId,
                prescription.Diagnosis,
                prescription.Medication,
                prescription.Dosage,
                prescription.Frequency,
                prescription.Duration,
                prescription.AdditionalInstructions,
                prescription.PrescriptionDate,
                prescription.CreatedAt,
                prescription.UpdatedAt
            );
        }
    }
}



