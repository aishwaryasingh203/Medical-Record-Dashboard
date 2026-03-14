using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserProfile model)
        {
            try
            {
                if (await _context.UserProfiles.AnyAsync(u => u.Email == model.Email))
                {
                    return BadRequest("Email already registered!");
                }

                _context.UserProfiles.Add(model);
                await _context.SaveChangesAsync();
                return Ok(new { message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("SIGNUP ERROR: " + ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, "Internal Server Error: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}