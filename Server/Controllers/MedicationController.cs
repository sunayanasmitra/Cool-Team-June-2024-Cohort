using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/Medication")]
public class MedicationController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public MedicationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("/Create-Medication")]
    public async Task<IActionResult> CreateMedication([FromBody] MedicationDto Dto)
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

                //get the medical record of current user
                var medicalRecord = await _context.MedicalRecord
                    .FirstOrDefaultAsync(mr => mr.ApplicationUserId == userId);

                if (medicalRecord == null)
                {
                    return NotFound("MedicalRecord not found for the user");
                }

                Medication medication = new Medication()
                {
                    StartDate = Dto.StartDate,
                    EndDate = Dto.EndDate,
                    MedicalRecordId = medicalRecord.Id,
                    DrugDirectoryId = Dto.DrugDirectoryId,
                };

                _context.Medication.Add(medication);
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
            throw new BadHttpRequestException(ex.Message);
        }
    }

    [HttpPut("/Update-Medication/{id}")]
    public async Task<IActionResult> UpdateMedication(int id, [FromBody] MedicationDto Dto)
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

                // get the medication to update
                var medication = await _context.Medication.FindAsync(id);
                if (medication == null)
                {
                    return NotFound($"Medication with ID {id} not found.");
                }

                //get the medical record of current user
                var medicalRecord = await _context.MedicalRecord
                    .FirstOrDefaultAsync(mr => mr.Id == medication.MedicalRecordId && mr.ApplicationUserId == userId);

                if (medicalRecord == null)
                {
                    return Unauthorized("You do not have permission to update this medication.");
                }

                //update medications details
                medication.StartDate = Dto.StartDate;
                medication.EndDate = Dto.EndDate;

                _context.Medication.Update(medication);
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

    [HttpDelete("/Delete-Medication/{id}")]
    public async Task<IActionResult> DeleteMedication(int id)
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


                //get the medication to delete
                var medication = await _context.Medication.FindAsync(id);
                if (medication == null)
                {
                    return NotFound($"Medication with ID {id} not found.");
                }

                //get the medical record of current user
                var medicalRecord = _context.MedicalRecord
                    .FirstOrDefault(mr => mr.Id == medication.MedicalRecordId && mr.ApplicationUserId == userId);

                if (medicalRecord == null)
                {
                    return NotFound("You do not have permission to delete this medication.");
                }

                //Remove the medication
                _context.Medication.Remove(medication);
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

    [HttpGet("/Get-Medication")]
    public async Task<IActionResult> GetMedication()
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
                return NotFound("MedicalRecord not found for the user.");
            }

            // retrieve medications for the most recent medical record
            var medication = _context.Medication.Where(s => s.MedicalRecordId == medicalRecord.Id)
                .ToList().Select(y => { MedicationDto temp = y.toDto();
                    temp.DrugDirectoryDto.DrugName = _context
                    .DrugDirectory.First(dir => dir.Id == y.DrugDirectoryId).DrugName;
                    return temp;
                }).ToList();

            return Ok(medication);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
}
