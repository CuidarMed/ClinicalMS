using System;

namespace Application.DTOs
{
    public record PrescriptionResponse(
        int PrescriptionId,
        long PatientId,
        long DoctorId,
        int? EncounterId,
        string Diagnosis,
        string Medication,
        string Dosage,
        string Frequency,
        string Duration,
        string? AdditionalInstructions,
        DateTime PrescriptionDate,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt
    );
}




