using Duende.IdentityServer.Extensions;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.Vaccine;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/Vaccine")]
    public class VaccineController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public VaccineController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("/Create-Vaccine")]
        public async Task<IActionResult> CreateVaccine([FromBody] VaccineRelationDto Dto)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var userId = _userManager.GetUserId(User);
                    if (userId == null)
                    {
                        return Unauthorized("User not authenticated");
                    }

                    VaccineRelation vaccineRelation = new VaccineRelation(Dto);
                    vaccineRelation.ApplicationUserId = userId;
                    _context.VaccineRelation.Add(vaccineRelation);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("/Update-Vaccine")]
        public async Task<IActionResult> UpdateVaccine(int id, [FromBody] VaccineRelationDto Dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    var userId = _userManager.GetUserId(User);
                    if (userId == null)
                    {
                        return Unauthorized("User not authenticated");
                    }

                    var vaccineRelation = await _context.VaccineRelation
                        .Where(vr => vr.Id == id && vr.ApplicationUserId == userId)
                        .FirstOrDefaultAsync();

                    if (vaccineRelation == null)
                    {
                        return Conflict("No vaccine relation found for this user.");
                    }

                    vaccineRelation.VaccineDirectoryId = Dto.VaccineDirectoryId;
                    vaccineRelation.DateAdministered = Dto.DateAdministered;

                    _context.VaccineRelation.Update(vaccineRelation);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/Delete-Vaccine/{id}")]
        public async Task<IActionResult> DeleteVaccine(int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                var vaccineRelation = await _context.VaccineRelation
                    .Where(vr => vr.ApplicationUserId == userId && vr.Id == id).FirstOrDefaultAsync();

                if (vaccineRelation == null)
                {
                    return Conflict("No vaccine relation found for this user.");
                }
                _context.VaccineRelation.Remove(vaccineRelation);
                await _context.SaveChangesAsync();
                return Ok();
              
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/Get-Vaccine")]
        public async Task<ActionResult<List<VaccineRelationDto>>> GetVaccine()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                var vaccineRelation = await _context.VaccineRelation
                    .Where(u => u.ApplicationUserId == userId).ToListAsync();

                return Ok(vaccineRelation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }

        [HttpGet("/Get-Directory")]
        public async Task<ActionResult<List<VaccineDirectoryDto>>> GetVaccineDirectory()
        {
            var directory = await _context.VaccineDirectory.OrderBy(vaccine => vaccine.VaccineName).ToListAsync();
            return Ok(directory);
        }

        [HttpPost("/upload-file")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
         
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");

            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var filePath = Path.Combine(uploadsDir, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { FilePath = $"/uploads/{file.FileName}" });
        }
    }
}
