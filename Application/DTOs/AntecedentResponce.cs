using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record AntecedentResponce
    (
        int AntecedentId,
        long PatientId,
        string Category,
        string Description,
        DateTime StratDate,
        DateTime? EndDate,
        string Status,
        DateTimeOffset CreatedAt,
        DateTimeOffset UpdatedAt
    );
}
