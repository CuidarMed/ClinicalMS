using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record AntecedentRequest
    {
        public required string Category { get; set; }
        public required string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndTime { get; set; }
        public required string Status { get; set; }
    }
}
