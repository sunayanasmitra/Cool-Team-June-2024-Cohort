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
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/EatingHabits")]
    public class EatingHabitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EatingHabitsController> _logger;

        public EatingHabitsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<EatingHabitsController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("Create-EatingHabits")]
        public async Task<IActionResult> CreateEatingHabits([FromBody] EatingHabitsDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var lifestyleRecord = await _context.LifestyleRecord.FindAsync(Dto.LifestyleRecordID);
                if (lifestyleRecord == null)
                    return NotFound("Lifestyle record not found.");

                var eatingHabits = new EatingHabits
                {
                    LifestyleRecordID = Dto.LifestyleRecordID,
                    CaloriesPerDay = Dto.CaloriesPerDay,
                    SugarIntakePerDay = Dto.SugarIntakePerDay,
                    FatIntakePerDay = Dto.FatIntakePerDay,
                    ProteinIntakePerDay = Dto.ProteinIntakePerDay,
                    CholesterolIntakePerDay = Dto.CholesterolIntakePerDay,
                    CarbIntakePerDay = Dto.CarbIntakePerDay,
                    SodiumIntakePerDay = Dto.SodiumIntakePerDay,
                    VegetablePercentOfIntake = Dto.VegetablePercentOfIntake,
                    MeatPercentOfIntake = Dto.MeatPercentOfIntake,
                    CerealsPercentofIntake = Dto.CerealsPercentofIntake,
                    FoodRestriction = Dto.FoodRestriction,
                };

                lifestyleRecord.EatingHabits.Add(eatingHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating eating habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpPut("Update-EatingHabits")]
        public async Task<IActionResult> UpdateEatingHabits([FromQuery] int id, [FromBody] EatingHabitsDto Dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingEatingHabits = await _context.EatingHabits
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (existingEatingHabits == null)
                    return NotFound("Eating habits not found.");

                existingEatingHabits.CaloriesPerDay = Dto.CaloriesPerDay;
                existingEatingHabits.SugarIntakePerDay = Dto.SugarIntakePerDay;
                existingEatingHabits.FatIntakePerDay = Dto.FatIntakePerDay;
                existingEatingHabits.ProteinIntakePerDay = Dto.ProteinIntakePerDay;
                existingEatingHabits.CholesterolIntakePerDay = Dto.CholesterolIntakePerDay;
                existingEatingHabits.CarbIntakePerDay = Dto.CarbIntakePerDay;
                existingEatingHabits.SodiumIntakePerDay = Dto.SodiumIntakePerDay;
                existingEatingHabits.VegetablePercentOfIntake = Dto.VegetablePercentOfIntake;
                existingEatingHabits.MeatPercentOfIntake = Dto.MeatPercentOfIntake;
                existingEatingHabits.CerealsPercentofIntake = Dto.CerealsPercentofIntake;
                existingEatingHabits.FoodRestriction = Dto.FoodRestriction;

                _context.EatingHabits.Update(existingEatingHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating eating habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-EatingHabits")]
        public async Task<IActionResult> GetEatingHabits([FromQuery] int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var eatingHabits = await _context.EatingHabits
                    .FirstOrDefaultAsync(e => e.Id == id);

                if (eatingHabits == null)
                    return NotFound("Eating habits not found.");

                return Ok(eatingHabits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving eating habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }

        [HttpGet("/Get-EatingHabits-By-LifestyleRecord")]
        public async Task<IActionResult> GetEatingHabitsByLifestyleRecord([FromQuery] int lifestyleRecordId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                    return Unauthorized();

                var eatingHabitsList = await _context.EatingHabits
                    .Where(e => e.LifestyleRecordID == lifestyleRecordId)
                    .ToListAsync();

                if (eatingHabitsList == null || !eatingHabitsList.Any())
                    return NotFound("No eating habits found for the specified lifestyle record.");

                return Ok(eatingHabitsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving eating habits for the specified lifestyle record.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }


        [HttpDelete("/Delete-EatingHabits")]
        public async Task<IActionResult> DeleteEatingHabits([FromQuery] int id)
        {
            try
            {
                var eatingHabits = await _context.EatingHabits.FindAsync(id);
                if (eatingHabits == null)
                    return NotFound("Eating habits not found.");

                _context.EatingHabits.Remove(eatingHabits);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting eating habits.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occurred.");
            }
        }
    }
}
