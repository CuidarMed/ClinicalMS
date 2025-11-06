using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record EncounterResponce
    (
        int EncounterId,
        long PatientId,
        long DoctorId,
        long AppoinmentId,
        string Reasons,
        string Subjetive,
        string Objetive,
        string Assessment,
        string Plan,
        string Notes,
        string Status,
        DateTime Date,
        DateTimeOffset createdAt,
        DateTimeOffset UpdatedAt
    );
}
