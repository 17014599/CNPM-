namespace AidimsApi.Models
{
    public class ScanRequest
    {
        public int Id { get; set; }
        public string PatientId { get; set; } // Ví dụ: "BN001"
        public string ScanType { get; set; }  // Ví dụ: "CT Scan"
        public string Note { get; set; }      // Ghi chú lý do chụp
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
