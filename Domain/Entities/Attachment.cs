using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }

        // Paciente al que pertenece el archivo 
        [Required]
        public long PatientId { get; set; }

        [Required]
        public int EncounterId { get; set; }

        public Encounter encounter { get; set; }

        [MaxLength(80)]
        public string FileName { get; set; }

        [MaxLength(50)]
        public string FileType { get; set; }

        public string FileUrl { get; set; }

        public string Notes { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }
    }
}
