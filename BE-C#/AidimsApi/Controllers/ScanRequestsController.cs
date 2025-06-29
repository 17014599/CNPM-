using Microsoft.AspNetCore.Mvc;
using AidimsApi.Models;
using AidimsApi.Data;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace AidimsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public ScanRequestsController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // API 1️⃣: Tạo yêu cầu chụp mới
        [HttpPost]
        public async Task<ActionResult<ScanRequest>> Create(ScanRequest request)
        {
            request.CreatedAt = DateTime.Now;
            _context.ScanRequests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        // API 2️⃣: Lấy tất cả yêu cầu chụp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScanRequest>>> GetAll()
        {
            return await _context.ScanRequests.ToListAsync();
        }

        // API 3️⃣: Gửi ảnh DICOM đến AI Flask để phân tích
        [HttpPost("analyze")]
        public async Task<IActionResult> AnalyzeImage(IFormFile dicom)
        {
            if (dicom == null || dicom.Length == 0)
                return BadRequest("No file uploaded.");

            var client = _httpClientFactory.CreateClient();

            using var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(dicom.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, "file", dicom.FileName);

            try
            {
                var response = await client.PostAsync("http://127.0.0.1:5001/analyze", content);
                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi gửi ảnh tới AI service: {ex.Message}");
            }
        }
    }
}
