using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record BasicPatientDTO
    (
        long PatientId,
        string Name,
        string LastName,
        DateOnly BirthDate
    );
}
