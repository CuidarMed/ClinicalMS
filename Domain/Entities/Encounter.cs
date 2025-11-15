using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Encounter
    {
        [Key]
        public int EncounterId { get; set; }

        // Relacion con Patient en DirectoryMS
        [Required]
        public long PatientId { get; set; }
        
        // Realacion con Doctor en DirectoryMS
        [Required]
        public long DoctorId { get; set; }
        
        // Relacion con Appointment en ScheudlingMS
        [Required]
        public long AppointmentId { get; set; }
        
        [Required]
        [MaxLength(225)]
        public string Reasons { get; set; }

        // Modelo SOAP
        [Required]
        [MaxLength(2000)]
        public string Subjective { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Objetive { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Assessment { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Plan { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Notes { get; set; }

        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        public ICollection<Attachment> attachments { get; set; } = new List<Attachment>();
    }
}
