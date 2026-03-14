using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public MedicalController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await _context.UserProfiles.OrderByDescending(u => u.Id).FirstOrDefaultAsync();
            if (profile == null) return NotFound("No profile found");

            string fullImagePath = string.IsNullOrEmpty(profile.ProfilePicPath) 
                ? "https://via.placeholder.com/150" 
                : $"http://localhost:5139/Uploads/{profile.ProfilePicPath}";

            return Ok(new {
                name = profile.FullName,
                email = profile.Email,
                phone = profile.PhoneNumber,
                gender = profile.Gender,
                profilePic = fullImagePath
            });
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadRecord([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string fileType)
        {
            if (file == null || file.Length == 0) return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var record = new MedicalRecord
            {
                FileName = fileName, 
                FileType = fileType, 
                FilePath = uniqueFileName 
            };

            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();

            return Ok(record);
        }

        [HttpGet("records")]
        public async Task<IActionResult> GetRecords()
        {
            return Ok(await _context.MedicalRecords.ToListAsync());
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _context.MedicalRecords.FindAsync(id);
            if (record == null) return NotFound();

            var filePath = Path.Combine(_env.ContentRootPath, "Uploads", record.FilePath ?? string.Empty);
            if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);

            _context.MedicalRecords.Remove(record);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted!" });
        }
    }
}