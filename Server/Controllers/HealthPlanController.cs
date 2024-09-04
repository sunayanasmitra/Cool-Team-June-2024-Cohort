using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HealthCareApp.Shared.Dto.HealthPlan;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Humanizer;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/HealthPlan")]
    public class HealthPlanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HealthPlanController> _logger;


        public HealthPlanController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<HealthPlanController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost("/Create-HealthPlan")]
        public async Task<IActionResult> CreateHealthPlan([FromBody] HealthPlanDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var healthPlan = new HealthPlan
                {
                    DateOfPlan = Dto.DateOfPlan,
                    ApplicationUserId = userId
                };

                _context.HealthPlan.Add(healthPlan);
                await _context.SaveChangesAsync();

                return Ok(healthPlan.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating health plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the health plan.");
            }
        }


        [HttpPut("/Update-HealthPlan")]
        public async Task<IActionResult> UpdateHealthPlan([FromQuery] int id, [FromBody] HealthPlanDto Dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var healthPlan = await _context.HealthPlan
                    .FirstOrDefaultAsync(lr => lr.Id == id && lr.ApplicationUserId == userId);

                if (healthPlan == null)
                    return NotFound("Health Plan not found.");

                healthPlan.DateOfPlan = Dto.DateOfPlan;
                _context.HealthPlan.Update(healthPlan);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating health plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the health plan.");
            }
        }

        [HttpDelete("/Delete-HealthPlan")]
        public async Task<IActionResult> DeleteHealthPlan([FromQuery] int id)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var healthPlan = await _context.HealthPlan
                    .FirstOrDefaultAsync(lr => lr.Id == id && lr.ApplicationUserId == userId);

                if (healthPlan == null)
                    return NotFound("Health plan not found.");

                _context.HealthPlan.Remove(healthPlan);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting health plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the health plan.");
            }
        }

        [HttpGet("/Get-HealthPlan")]
        public async Task<IActionResult> GetHealthPlans()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                    return Unauthorized();

                var healthPlans = await _context.HealthPlan
                    .Where(hp => hp.ApplicationUserId == userId)
                    .ToListAsync();

                if (healthPlans == null || !healthPlans.Any())
                    return NotFound("No health plans found for the current user.");

                return Ok(healthPlans);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving health plans.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving health plans.");
            }
        }
    }
}
