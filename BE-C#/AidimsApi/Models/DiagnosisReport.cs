namespace AidimsApi.Models
{
    public class DiagnosisReport
    {
        public int Id { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
