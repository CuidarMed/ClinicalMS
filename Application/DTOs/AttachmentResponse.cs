using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AttachmentResponse
    {
        public int AttachmentId { get; set; }
        public long PatientId { get; set; }
        public int EncounterId { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FileUrl { get; set; }
        public string? Notes { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    };
}
