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
    [Route("api/WorkoutPlan")]
    public class WorkoutPlanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<WorkoutPlanController> _logger;

        public WorkoutPlanController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<WorkoutPlanController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("/Create-WorkoutPlan")]
        public async Task<IActionResult> CreateWorkoutPlan([FromBody] WorkoutPlanDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var workoutPlan = new WorkoutPlan
                {
                    HealthPlanID = Dto.HealthPlanID,
                    WorkoutGoal = Dto.WorkoutGoal,
                };

                _context.WorkoutPlan.Add(workoutPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating workout plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("/Update-WorkoutPlan")]
        public async Task<IActionResult> UpdateWorkoutPlan([FromBody] WorkoutPlanDto Dto, [FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingWorkoutPlan = await _context.WorkoutPlan
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (existingWorkoutPlan == null)
                    return NotFound("Workout plan not found.");

                existingWorkoutPlan.WorkoutGoal = Dto.WorkoutGoal;
                existingWorkoutPlan.IsChecked = Dto.IsChecked;

                _context.WorkoutPlan.Update(existingWorkoutPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating workout plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }


        [HttpGet("/Get-WorkoutPlan")]
        public async Task<IActionResult> GetWorkoutPlan([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var workoutPlan = await _context.WorkoutPlan
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (workoutPlan == null)
                    return NotFound("Workout plan not found.");

                return Ok(workoutPlan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving workout plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

		[HttpGet("/Get-WorkoutPlanByPlanId")]
		public async Task<IActionResult> GetWorkoutPlanByPlanId([FromQuery] int healthPlanId)
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return Unauthorized();

				var workoutPlans = await _context.WorkoutPlan
					.Where(dp => dp.HealthPlanID == healthPlanId)
					.ToListAsync();

				if (workoutPlans == null || !workoutPlans.Any())
					return NotFound("No workout plans found for the given health plan ID.");

				return Ok(workoutPlans);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving workout plans by health plan ID.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
			}
		}

		[HttpDelete("/Delete-WorkoutPlan")]
        public async Task<IActionResult> DeleteWorkoutPlan([FromQuery] int id)
        {
            try
            {
                var workoutPlan = await _context.WorkoutPlan.FindAsync(id);
                if (workoutPlan == null)
                    return NotFound("Workout plan not found.");

                _context.WorkoutPlan.Remove(workoutPlan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting workout plan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
