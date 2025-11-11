using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record PatientSummaryDTO
    (
        BasicPatientDTO BasicPatient, // DTO interno con los datos de Patient
        List<Encounter> RecentEncounters, // Ultimas 3 consultas
        List<Antedecent> Antedecents, 
        List<Attachment> RecentAttachment // ultimos archivos
    );

}
