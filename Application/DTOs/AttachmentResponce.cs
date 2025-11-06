using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record AttachmentResponce
    (
        int AttachmentId,
        long PatientId,
        int EncounterId,
        string FileName,
        string FileType,
        string FileUrl,
        string Notes,
        DateTimeOffset CreatedAt
    );
}
