using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }

        // Relación con Patient en DirectoryMS
        [Required]
        public long PatientId { get; set; }

        // Relación con Doctor en DirectoryMS
        [Required]
        public long DoctorId { get; set; }

        // Relación opcional con Encounter
        public int? EncounterId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Diagnosis { get; set; }

        [Required]
        [MaxLength(200)]
        public string Medication { get; set; }

        [Required]
        [MaxLength(100)]
        public string Dosage { get; set; }

        [Required]
        [MaxLength(100)]
        public string Frequency { get; set; }

        [Required]
        [MaxLength(100)]
        public string Duration { get; set; }

        [MaxLength(1000)]
        public string? AdditionalInstructions { get; set; }

        [Required]
        public DateTime PrescriptionDate { get; set; } = DateTime.UtcNow;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}




