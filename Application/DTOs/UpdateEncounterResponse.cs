namespace Application.DTOs
{
    public class UpdateEncounterResponse
    {
        public long EncounterId { get; set; }
        public long AppointmentId { get; set; }
        public string? Status { get; set; }
    }
}
