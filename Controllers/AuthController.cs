using Microsoft.AspNetCore.Mvc;
using AidimsApi.Models;
using AidimsApi.Data;

namespace AidimsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == login.Username &&
                u.Password == login.Password &&
                u.Role == login.Role);

            if (user == null)
                return Unauthorized(new { message = "Sai thông tin đăng nhập" });

            return Ok(new { username = user.Username, role = user.Role });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest(new { message = "Tài khoản đã tồn tại" });

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }
    }
}
