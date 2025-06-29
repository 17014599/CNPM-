namespace AidimsApi.Models
{
    public class AIResult
    {
        public string Diagnosis { get; set; } = "";
        public double Confidence { get; set; }
        public string Filename { get; set; } = "";
    }
}
