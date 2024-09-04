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
    [Route("api/DietPlan")]
    public class DietPlanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DietPlanController> _logger;

        public DietPlanController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<DietPlanController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("/Create-DietPlan")]
        public async Task<IActionResult> CreateDietPlan([FromBody] DietPlanDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var dietPlan = new DietPlan
                {
                    HealthPlanID = Dto.HealthPlanID,
                    DietGoal = Dto.DietGoal,
                };

                _context.DietPlan.Add(dietPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating diet plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("/Update-DietPlan")]
        public async Task<IActionResult> UpdateDietPlan([FromBody] DietPlanDto Dto, [FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingDietPlan = await _context.DietPlan
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (existingDietPlan == null)
                    return NotFound("Diet plan not found.");

                existingDietPlan.DietGoal = Dto.DietGoal;
                existingDietPlan.IsChecked = Dto.IsChecked;

                _context.DietPlan.Update(existingDietPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating diet plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-DietPlan")]
        public async Task<IActionResult> GetDietPlan([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var dietPlan = await _context.DietPlan
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (dietPlan == null)
                    return NotFound("Diet plan not found.");

                return Ok(dietPlan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving diet plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

		[HttpGet("/Get-DietPlanByPlanId")]
		public async Task<IActionResult> GetDietPlanByPlanId([FromQuery] int healthPlanId)
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return Unauthorized();

				var dietPlans = await _context.DietPlan
					.Where(dp => dp.HealthPlanID == healthPlanId)
					.ToListAsync();

				if (dietPlans == null || !dietPlans.Any())
					return NotFound("No diet plans found for the given health plan ID.");

				return Ok(dietPlans);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving diet plans by health plan ID.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
			}
		}

		[HttpDelete("/Delete-DietPlan")]
        public async Task<IActionResult> DeleteDietPlan([FromQuery] int id)
        {
            try
            {
                var dietPlan = await _context.DietPlan.FindAsync(id);
                if (dietPlan == null)
                    return NotFound("Diet plan not found.");

                _context.DietPlan.Remove(dietPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting diet plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
