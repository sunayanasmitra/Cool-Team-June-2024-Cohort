using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/Allergy")]
public class AllergyController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public AllergyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("/Create-Allergy")]
    public async Task<IActionResult> Createallergy([FromBody] AllergyDto Dto)
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
                var medicalRecord = await _context.MedicalRecord.FirstOrDefaultAsync(mr =>
                    mr.ApplicationUserId == userId
                );

                if (medicalRecord == null)
                {
                    return NotFound("MedicalRecord not found for the user");
                }

                Allergy allergy = new Allergy()
                {
                    StartDate = Dto.StartDate,
                    EndDate = Dto.EndDate,
                    MedicalRecordId = medicalRecord.Id,
                    AllergyDirectoryId = Dto.AllergyDirectoryId,
                };

                _context.Allergy.Add(allergy);
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

    [HttpPut("/Update-Allergy/{id}")]
    public async Task<IActionResult> UpdateAllergy(int id, [FromBody] AllergyDto Dto)
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

                // get the allergy to update
                var allergy = await _context.Allergy.FindAsync(id);
                if (allergy == null)
                {
                    return NotFound($"allergy with ID {id} not found.");
                }

                //get the medical record of current user
                var medicalRecord = await _context.MedicalRecord.FirstOrDefaultAsync(mr =>
                    mr.Id == allergy.MedicalRecordId && mr.ApplicationUserId == userId
                );

                if (medicalRecord == null)
                {
                    return Unauthorized("You do not have permission to update this allergy.");
                }

                //update allergys details
                allergy.StartDate = Dto.StartDate;
                allergy.EndDate = Dto.EndDate;

                _context.Allergy.Update(allergy);
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

    [HttpDelete("/Delete-Allergy/{id}")]
    public async Task<IActionResult> DeleteAllergy(int id)
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

                //get the allergy to delete
                var allergy = await _context.Allergy.FindAsync(id);
                if (allergy == null)
                {
                    return NotFound($"Allergy with ID {id} not found.");
                }

                //get the medical record of current user
                var medicalRecord = _context.MedicalRecord.FirstOrDefault(mr =>
                    mr.Id == allergy.MedicalRecordId && mr.ApplicationUserId == userId
                );

                if (medicalRecord == null)
                {
                    return NotFound("You do not have permission to delete this allergy.");
                }

                //Remove the allergy
                _context.Allergy.Remove(allergy);
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

    [HttpGet("/Get-Allergy")]
    public async Task<IActionResult> GetAllergy()
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
            var medicalRecord = await _context.MedicalRecord.FirstOrDefaultAsync(mr =>
                mr.ApplicationUserId == userId
            );

            if (medicalRecord == null)
            {
                return NotFound("MedicalRecord not found for the user.");
            }

            // retrieve allergys for the most recent medical record
            var allergy = _context
                .Allergy
                .Where(s => 
                    s.MedicalRecordId == medicalRecord.Id)
                .ToList()
                .Select(y =>
                {
                    AllergyDto temp = y.toDto();
                    temp.AllergyDirectoryDto.AllergyName = _context
                        .AllergyDirectory
                        .First(dir => dir.Id == y.AllergyDirectoryId)
                        .AllergyName;
                    return temp;
                })
                .ToList();

            return Ok(allergy);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
