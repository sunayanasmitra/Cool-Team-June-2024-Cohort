using Duende.IdentityServer.Extensions;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/PhysicalActivities")]
    public class PhysicalActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<PhysicalActivitiesController> _logger;

        public PhysicalActivitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<PhysicalActivitiesController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Create-PhysicalActivities")]
        public async Task<IActionResult> CreatePhysicalActivities([FromBody] PhysicalActivitiesDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var physicalActivities = new PhysicalActivities
                {
                    LifestyleRecordID = Dto.LifestyleRecordID,
                    TimesPerWeek = Dto.TimesPerWeek,
                    ActivityDirectoryId = Dto.ActivityDirectoryId,
                };

                _context.PhysicalActivities.Add(physicalActivities);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating physical activities.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("Update-PhysicalActivities")]
        public async Task<IActionResult> UpdatePhysicalActivities([FromQuery] int id, [FromBody] PhysicalActivitiesDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingPhysicalActivities = await _context.PhysicalActivities
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingPhysicalActivities == null)
                    return NotFound("Physical activities not found.");

                existingPhysicalActivities.LifestyleRecordID = Dto.LifestyleRecordID;
                existingPhysicalActivities.TimesPerWeek = Dto.TimesPerWeek;
                existingPhysicalActivities.ActivityDirectoryId = Dto.ActivityDirectoryId;

                _context.PhysicalActivities.Update(existingPhysicalActivities);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating alcohol habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-PhysicalActivities")]
        public async Task<IActionResult> GetPhysicalActivities([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var physicalActivities = await _context.PhysicalActivities
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (physicalActivities == null)
                    return NotFound("Physical activities not found.");

                return Ok(physicalActivities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving physical activities.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-PhysicalActivities-By-LifestyleRecord")]
        public async Task<IActionResult> GetPhysicalActivitiesByLifestyleRecord([FromQuery] int lifestyleRecordId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var physicalActivitiesList = await _context.PhysicalActivities
                    .Where(e => e.LifestyleRecordID == lifestyleRecordId)
                    .ToListAsync();

                if (physicalActivitiesList == null || !physicalActivitiesList.Any())
                    return NotFound("No physical activities found for the specified lifestyle record.");

                return Ok(physicalActivitiesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving physical activities for the specified lifestyle record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpDelete("/Delete-PhysicalActivities")]
        public async Task<IActionResult> DeletePhysicalActivities([FromQuery] int id)
        {
            try
            {
                var physicalActivities = await _context.PhysicalActivities.FindAsync(id);
                if (physicalActivities == null)
                    return NotFound("Physical activities not found.");

                _context.PhysicalActivities.Remove(physicalActivities);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting Physical activities.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
