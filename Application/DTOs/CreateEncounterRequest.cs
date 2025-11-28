namespace Application.DTOs
{
    public record CreateEncounterRequest(
        long PatientId,
        long DoctorId,
        long AppointmentId,
        string Reasons,
        string Subjective,
        string Objetive,
        string Assessment,
        string Plan,
        string Notes,
        string Status,
        DateTime Date
        );
    
    
}
