using Duende.IdentityServer.Extensions;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.HealthPlan;
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
    [Route("api/HealthRisk")]
    public class HealthRiskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HealthRiskController> _logger;

        public HealthRiskController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<HealthRiskController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("/Create-HealthRisk")]
        public async Task<IActionResult> CreateHealthRisk([FromBody] HealthRiskDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var healthRisk = new HealthRisk
                {   
                    HealthPlanID = Dto.HealthPlanID,
                    Risk = Dto.Risk,
                    Reason = Dto.Reason,
                };

                _context.HealthRisk.Add(healthRisk);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating health risk.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("/Update-HealthRisk")]
        public async Task<IActionResult> UpdateHealthRisk([FromBody] HealthRiskDto Dto, [FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingHealthRisk = await _context.HealthRisk
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (existingHealthRisk == null)
                    return NotFound("Health risk not found.");

                existingHealthRisk.Risk = Dto.Risk;
                existingHealthRisk.Reason = Dto.Reason;

                _context.HealthRisk.Update(existingHealthRisk);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating health risk.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-HealthRisk")]
        public async Task<IActionResult> GetHealthRisk([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var healthRisk = await _context.HealthRisk
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (healthRisk == null)
                    return NotFound("Health risk not found.");

                return Ok(healthRisk);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving health risk.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

		[HttpGet("/Get-HealthRiskByPlanId")]
		public async Task<IActionResult> GetHealthRiskByPlanId([FromQuery] int healthPlanId)
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return Unauthorized();

				var healthRisk = await _context.HealthRisk
					.Where(dp => dp.HealthPlanID == healthPlanId)
					.ToListAsync();

				if (healthRisk == null || !healthRisk.Any())
					return NotFound("No health risks found for the given health plan ID.");

				return Ok(healthRisk);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving health risks by health plan ID.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
			}
		}

		[HttpDelete("/Delete-HealthRisk")]
        public async Task<IActionResult> DeleteHealthRisk([FromQuery] int id)
        {
            try
            {
                var healthRisk = await _context.HealthRisk.FindAsync(id);
                if (healthRisk == null)
                    return NotFound("Health risk not found.");

                _context.HealthRisk.Remove(healthRisk);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting health risk.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
