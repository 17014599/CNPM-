using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AidimsApi.Data;
using AidimsApi.Models;

namespace AidimsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/patients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            return await _context.Patients.ToListAsync();
        }

        // POST: api/patients
        [HttpPost]
        public async Task<ActionResult<Patient>> Create(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return Ok(patient);
        }

        // ✅ POST: api/patients/bulk
        [HttpPost("bulk")]
        public async Task<ActionResult<IEnumerable<Patient>>> CreateMultiple([FromBody] List<Patient> patients)
        {
            if (patients == null || !patients.Any())
            {
                return BadRequest("Danh sách bệnh nhân rỗng.");
            }

            _context.Patients.AddRange(patients);
            await _context.SaveChangesAsync();

            return Ok(patients);
        }
    }
}
