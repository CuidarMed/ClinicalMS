using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record AntecedentUpdate
    (
        string Category,
        string Description,
        DateTime StartDate,
        DateTime? EndDate,
        string Status
    );
}
