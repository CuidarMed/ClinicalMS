using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EncounterResponse
    {
        public int EncounterId { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long AppoinmentId { get; set; }
        public string Reasons { get; set; }
        public string Subjetive { get; set; }
        public string Objetive { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public DateTimeOffset createdAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
