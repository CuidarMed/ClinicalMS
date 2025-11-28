namespace Application.DTOs
{
    public class UpdateEncounterRequest
    {
        public string? Reasons { get; set; }
        public string? Subjective { get; set; }
        public string? Objetive { get; set; }
        public string? Assessment { get; set; }
        public string? Plan { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
    }
}
