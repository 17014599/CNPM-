using Microsoft.EntityFrameworkCore;
using AidimsApi.Models;

namespace AidimsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }

        // ✅ Thêm dòng này để tạo bảng ScanRequests
        public DbSet<ScanRequest> ScanRequests { get; set; }

        public DbSet<DiagnosisReport> DiagnosisReports { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
