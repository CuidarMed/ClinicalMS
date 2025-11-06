using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record EncounterRequest
    (
        long AppointmentId, // Opcional
        string Reasons,
        string Subjective,
        string Objetive,
        string Assessment,
        string Plan,
        string Notes
    );
}
