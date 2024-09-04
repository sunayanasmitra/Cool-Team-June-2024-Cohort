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
    [Route("api/AlcoholHabits")]
    public class AlcoholHabitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AlcoholHabitsController> _logger;

        public AlcoholHabitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<AlcoholHabitsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Create-AlcoholHabits")]
        public async Task<IActionResult> CreateAlcoholHabits([FromBody] AlcoholHabitsDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var alcoholHabits = new AlcoholHabits
                {
                    LifestyleRecordID = Dto.LifestyleRecordID,
                    DrinksPerWeek = Dto.DrinksPerWeek,
                    DrinksPerMonth = Dto.DrinksPerMonth,
                    PrimaryAlcoholType = Dto.PrimaryAlcoholType,
                };

                _context.AlcoholHabits.Add(alcoholHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating alcohol habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("Update-AlcoholHabits")]
        public async Task<IActionResult> UpdateAlcoholHabits([FromQuery] int id, [FromBody] AlcoholHabitsDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingAlcoholHabits = await _context.AlcoholHabits
                    .FirstOrDefaultAsync(a => a.Id == id);

                if (existingAlcoholHabits == null)
                    return NotFound("Alcohol habits not found.");

                existingAlcoholHabits.DrinksPerWeek = Dto.DrinksPerWeek;
                existingAlcoholHabits.DrinksPerMonth = Dto.DrinksPerMonth;
                existingAlcoholHabits.PrimaryAlcoholType = Dto.PrimaryAlcoholType;

                _context.AlcoholHabits.Update(existingAlcoholHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating alcohol habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-AlcoholHabits")]
        public async Task<IActionResult> GetAlcoholHabits([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var alcoholHabits = await _context.AlcoholHabits
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (alcoholHabits == null)
                    return NotFound("Alcohol habits not found.");

                return Ok(alcoholHabits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving alcohol habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-AlcoholHabits-By-LifestyleRecord")]
        public async Task<IActionResult> GeAlcoholHabitsByLifestyleRecord([FromQuery] int lifestyleRecordId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var alcoholHabitsList = await _context.AlcoholHabits
                    .Where(e => e.LifestyleRecordID == lifestyleRecordId)
                    .ToListAsync();

                if (alcoholHabitsList == null || !alcoholHabitsList.Any())
                    return NotFound("No alcohol habits found for the specified lifestyle record.");

                return Ok(alcoholHabitsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving alcohol habits for the specified lifestyle record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpDelete("/Delete-AlcoholHabits")]
        public async Task<IActionResult> DeleteAlcoholHabits([FromQuery] int id)
        {
            try
            {
                var alcoholHabits = await _context.AlcoholHabits.FindAsync(id);
                if (alcoholHabits == null)
                    return NotFound("Alcohol habits not found.");

                _context.AlcoholHabits.Remove(alcoholHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting alcohol habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
