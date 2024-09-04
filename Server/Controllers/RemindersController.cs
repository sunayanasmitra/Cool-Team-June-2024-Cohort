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
    [Route("api/Reminders")]
    public class RemindersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RemindersController> _logger;

        public RemindersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<RemindersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("/Create-Reminder")]
        public async Task<IActionResult> CreateReminders([FromBody] RemindersDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var reminder = new Reminders
                {
                    HealthPlanID = Dto.HealthPlanID,
                    Reminder = Dto.Reminder,
                };

                _context.Reminders.Add(reminder);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating reminder.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("/Update-Reminder")]
        public async Task<IActionResult> UpdateReminders([FromBody] RemindersDto Dto, [FromQuery] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingReminder = await _context.Reminders
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (existingReminder == null)
                    return NotFound("Reminder not found.");

                existingReminder.Reminder = Dto.Reminder;
                existingReminder.IsChecked = Dto.IsChecked;

                _context.Reminders.Update(existingReminder);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating reminder.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-Reminder")]
        public async Task<IActionResult> GetReminders([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var reminder = await _context.Reminders
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (reminder == null)
                    return NotFound("Reminder not found.");

                return Ok(reminder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving reminder.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

		[HttpGet("/Get-RemindersByPlanId")]
		public async Task<IActionResult> GetRemindersByPlanId([FromQuery] int healthPlanId)
		{
			try
			{
				var user = await _userManager.GetUserAsync(User);
				if (user == null)
					return Unauthorized();

				var reminders = await _context.Reminders
					.Where(r => r.HealthPlanID == healthPlanId)
					.ToListAsync();

				if (reminders == null || !reminders.Any())
					return NotFound("No reminders found for the given health plan ID.");

				return Ok(reminders);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while retrieving reminders by health plan ID.");
				return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
			}
		}

		[HttpDelete("/Delete-Reminder")]
        public async Task<IActionResult> DeleteReminders([FromQuery] int id)
        {
            try
            {
                var reminder = await _context.Reminders.FindAsync(id);
                if (reminder == null)
                    return NotFound("Reminder not found.");

                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting reminder.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
