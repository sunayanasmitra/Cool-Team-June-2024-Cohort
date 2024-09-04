using Duende.IdentityServer.Extensions;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/DrugHabits")]
    public class DrugHabitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DrugHabitsController> _logger;

        public DrugHabitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<DrugHabitsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Create-DrugHabits")]
        public async Task<IActionResult> CreateDrugHabits([FromBody] DrugHabitsDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var drugHabits = new DrugHabits
                {
                    LifestyleRecordID = Dto.LifestyleRecordID,
                    DosesPerWeek = Dto.DosesPerWeek,
                    DosesPerMonth = Dto.DosesPerMonth,
                    DrugDirectoryId = Dto.DrugDirectoryId,
                };

                _context.DrugHabits.Add(drugHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating c habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("Update-DrugHabits")]
        public async Task<IActionResult> UpdateDrugHabits([FromQuery] int id, [FromBody] DrugHabitsDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingDrugHabits = await _context.DrugHabits
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (existingDrugHabits == null)
                    return NotFound("Drug habits not found.");

                existingDrugHabits.DosesPerWeek = Dto.DosesPerWeek;
                existingDrugHabits.DosesPerMonth = Dto.DosesPerMonth;
                existingDrugHabits.DrugDirectoryId = Dto.DrugDirectoryId;

                _context.DrugHabits.Update(existingDrugHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating drug habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-DrugHabits")]
        public async Task<IActionResult> GetDrugHabits([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var drugHabits = await _context.DrugHabits
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (drugHabits == null)
                    return NotFound("Drug habits not found.");

                return Ok(drugHabits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving drug habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-DrugHabits-By-LifestyleRecord")]
        public async Task<IActionResult> GetDrugHabitsByLifestyleRecord([FromQuery] int lifestyleRecordId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var drugHabitsList = await _context.DrugHabits
                    .Where(e => e.LifestyleRecordID == lifestyleRecordId)
                    .ToListAsync();

                if (drugHabitsList == null || !drugHabitsList.Any())
                    return NotFound("No drug habits found for the specified lifestyle record.");

                return Ok(drugHabitsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving drug habits for the specified lifestyle record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpDelete("/Delete-DrugHabits")]
        public async Task<IActionResult> DeleteDrugHabits([FromQuery] int id)
        {
            try
            {
                var drugHabits = await _context.DrugHabits.FindAsync(id);
                if (drugHabits == null)
                    return NotFound("Drug habits not found.");

                _context.DrugHabits.Remove(drugHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting drug habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}

