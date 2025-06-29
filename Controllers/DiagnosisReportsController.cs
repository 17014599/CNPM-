using Microsoft.AspNetCore.Mvc;
using AidimsApi.Models;
using AidimsApi.Data;

namespace AidimsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosisReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiagnosisReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<DiagnosisReport>> Create(DiagnosisReport report)
        {
            _context.DiagnosisReports.Add(report);
            await _context.SaveChangesAsync();
            return Ok(report);
        }

        [HttpGet]
        public ActionResult<IEnumerable<DiagnosisReport>> GetAll()
        {
            return Ok(_context.DiagnosisReports.ToList());
        }
    }
}
