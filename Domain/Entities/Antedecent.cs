using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Antedecent
    {
        [Key]
        public int AntedecentId { get; set; }
       
        [Required]
        public long PatientId { get; set; }     //Referencia al patient en DirectoryMS
        
        [Required]
        [MaxLength(200)]
        public string Category { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime StartDate { get; set; }
        
        public DateTime? EndTime { get; set; }
        
        [Required]
        public string Status { get; set; }
        
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
