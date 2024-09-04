using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Humanizer;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/LifestyleRecords")]
    public class LifestyleRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LifestyleRecordsController> _logger;


        public LifestyleRecordsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<LifestyleRecordsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost("/Create-LifestyleRecords")]
        public async Task<IActionResult> CreateLifestyleRecord([FromBody] LifestyleRecordDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var lifestyleRecord = new LifestyleRecord
                {
                    RecordDate = dto.RecordDate,
                    ApplicationUserId = userId
                };

                _context.LifestyleRecord.Add(lifestyleRecord);
                await _context.SaveChangesAsync();

                return Ok(lifestyleRecord.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating lifestyle record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the lifestyle record.");
            }
        }


        [HttpPut("/Update-LifestyleRecords")]
        public async Task<IActionResult> UpdateLifestyleRecord([FromQuery] int id, [FromBody] LifestyleRecordDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var lifestyleRecord = await _context.LifestyleRecord
                    .FirstOrDefaultAsync(lr => lr.Id == id && lr.ApplicationUserId == userId);

                if (lifestyleRecord == null)
                    return NotFound("Lifestyle record not found.");

                lifestyleRecord.RecordDate = Dto.RecordDate;
                _context.LifestyleRecord.Update(lifestyleRecord);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating lifestyle record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the lifestyle record.");
            }
        }

        [HttpDelete("/Delete-LifestyleRecord")]
    public async Task<IActionResult> DeleteLifestyleRecord([FromQuery] int id)
    {
        try
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            var lifestyleRecord = await _context.LifestyleRecord
                .Include(lr => lr.DrugHabits)
                .Include(lr => lr.EatingHabits)
                .Include(lr => lr.AlcoholHabits)
                .Include(lr => lr.PhysicalActivities)
                .FirstOrDefaultAsync(lr => lr.Id == id && lr.ApplicationUserId == userId);

            if (lifestyleRecord == null)
                return NotFound("Lifestyle record not found.");

            _context.DrugHabits.RemoveRange(lifestyleRecord.DrugHabits);
            _context.EatingHabits.RemoveRange(lifestyleRecord.EatingHabits);
            _context.AlcoholHabits.RemoveRange(lifestyleRecord.AlcoholHabits);
            _context.PhysicalActivities.RemoveRange(lifestyleRecord.PhysicalActivities);

            _context.LifestyleRecord.Remove(lifestyleRecord);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting lifestyle record.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the lifestyle record.");
        }
    }


        [HttpGet("/Get-LifestyleRecords")]
        public async Task<IActionResult> GetLifestyleRecords()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var lifestyleRecords = await _context.LifestyleRecord
                    .Where(lr => lr.ApplicationUserId == userId)
                    .ToListAsync();

                if (!lifestyleRecords.Any())
                    return NotFound("No lifestyle records found for this user.");

                return Ok(lifestyleRecords);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting lifestyle records.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the lifestyle records.");
            }
        }

    }
}
