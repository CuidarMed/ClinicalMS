using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EncounterRequest
    {
        public long AppointmentId {get; set; }
        public string Reasons { get; set; }
        public string Subjective { get; set; }
        public string Objetive { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public string Notes { get; set; }
    }
}
