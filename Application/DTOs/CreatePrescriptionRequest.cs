using System;

namespace Application.DTOs
{
    public record CreatePrescriptionRequest(
        long PatientId,
        long DoctorId,
        int? EncounterId,
        string Diagnosis,
        string Medication,
        string Dosage,
        string Frequency,
        string Duration,
        string? AdditionalInstructions
    );
}



