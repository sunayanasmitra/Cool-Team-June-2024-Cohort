using HealthCareApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Authorization;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/MedicalRecord")]
public class MedicalRecordController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public MedicalRecordController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("/Create-MedicalRecord")]
    public async Task<IActionResult> CreateMedicalRecord([FromBody] MedicalRecordDto Dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                //get the current user's id
                var userId = _userManager.GetUserId(User);
                if (userId == null) 
                {
                    return Unauthorized("User not authenticated");                
                }

                //check if a MedicalRecord already exists for this user
                var existingRecord = await _context.MedicalRecord
                    .FirstOrDefaultAsync(mr => mr.ApplicationUserId == userId);

                if (existingRecord != null)
                {
                    return Conflict("A medicalRecord already exists for this user.");
                }

                MedicalRecord medicalRecord = new MedicalRecord()
                {
                    RecordDate = Dto.RecordDate,
                    BloodPressure = Dto.BloodPressure,
                    BloodGlucoseLevel = Dto.BloodGlucoseLevel,
                    WeightInPounds = Dto.WeightInPounds,
                    HeightInInches = Dto.HeightInInches,
                    ApplicationUserId = userId
                };

                _context.MedicalRecord.Add(medicalRecord);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("/Update-MedicalRecord")]
    public async Task<IActionResult> UpdateMedicalRecord([FromBody] MedicalRecordDto Dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                //get the current user's ID
                var userId = _userManager.GetUserId(User);
                if (userId == null)
                {
                    return Unauthorized("User not authenticated");
                }

                //find the existing medical record
                var medicalRecord = await _context.MedicalRecord
                    .FirstOrDefaultAsync(mr => mr.ApplicationUserId == userId);

                if (medicalRecord == null)
                {
                    return Conflict("No medical record found for this user.");
                }

                medicalRecord.RecordDate = Dto.RecordDate;
                medicalRecord.BloodPressure = Dto.BloodPressure;
                medicalRecord.BloodGlucoseLevel = Dto.BloodGlucoseLevel;
                medicalRecord.WeightInPounds = Dto.WeightInPounds;
                medicalRecord.HeightInInches = Dto.HeightInInches;

                _context.MedicalRecord.Update(medicalRecord);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("/Delete-MedicalRecord")]
    public async Task<IActionResult> DeleteMedicalRecord()
    {
        try
        {
            //get the current user's ID
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            //find the existing medical record
            var medicalRecord = await _context.MedicalRecord
                .FirstOrDefaultAsync(mr => mr.ApplicationUserId == userId);

            if (medicalRecord == null)
            {
                return Conflict("No medical record found for this user.");
            }

            //Remove the medical record
            _context.MedicalRecord.Remove(medicalRecord);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("/Get-MedicalRecord")]
    public async Task<IActionResult> GetMedicalRecord()
    {
        try
        {
            //get the current user's ID
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            //find the latest medical record for the user
            var medicalRecord = await _context.MedicalRecord
                .FirstOrDefaultAsync(mr => mr.ApplicationUserId == userId);

            if (medicalRecord == null)
            {
                return BadRequest("MedicalRecord not found for the user.");
            }

            return Ok(medicalRecord);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
}
