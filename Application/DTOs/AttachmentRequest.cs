using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record AttachmentRequest
    (
        int EncounterId, // Opcional
        string FileName,
        string FileType,
        string FileUrl,
        string Notes
    );
}
