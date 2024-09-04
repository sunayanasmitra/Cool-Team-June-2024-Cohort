using HealthCareApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Authorization;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.EntityFrameworkCore;


namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/Symptom")]
public class SymptomController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public SymptomController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("/Create-Symptom")]
    public async Task<IActionResult> CreateSymptom([FromBody] SymptomDto Dto)
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

                if(medicalRecord == null)
                {
                    return NotFound("MedicalRecord not found for the user");    
                }

                Symptom symptom = new Symptom()
                {
                    StartDate = Dto.StartDate,
                    EndDate = Dto.EndDate,
                    MedicalRecordId = medicalRecord.Id,
                    SymptomDirectoryId = Dto.SymptomDirectoryId,
                };

                _context.Symptom.Add(symptom);
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

    [HttpPut("/Update-Symptom/{id}")]
    public async Task<IActionResult> UpdateSymptom(int id, [FromBody] SymptomDto Dto)
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

                // get the symptom to update
                var symptom = await _context.Symptom.FindAsync(id);
                if(symptom == null)
                {
                    return NotFound($"Symptom with ID {id} not found.");
                }

                //get the medical record of current user
                var medicalRecord = await _context.MedicalRecord
                    .FirstOrDefaultAsync(mr => mr.Id == symptom.MedicalRecordId && mr.ApplicationUserId == userId);

                if (medicalRecord == null)
                {
                    return Unauthorized("You do not have permission to update this symptom.");
                }

                //update symptoms details
                symptom.StartDate = Dto.StartDate;
                symptom.EndDate = Dto.EndDate;

                _context.Symptom.Update(symptom);
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

    [HttpDelete("/Delete-Symptom/{id}")]
    public async Task<IActionResult> DeleteSymptom(int id)
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


                //get the symptom to delete
                var symptom = await _context.Symptom.FindAsync(id);
                if(symptom == null)
                {
                    return NotFound($"Symptom with ID {id} not found.");
                }

                //get the medical record of current user
                var medicalRecord = _context.MedicalRecord
                    .FirstOrDefault(mr => mr.Id == symptom.MedicalRecordId && mr.ApplicationUserId == userId);

                if (medicalRecord == null)
                {
                    return NotFound("You do not have permission to delete this symptom.");
                }

                //Remove the symptom
                _context.Symptom.Remove(symptom);
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

    [HttpGet("/Get-Symptom")]
    public async Task<IActionResult> GetSymptom()
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

            // retrieve symptoms for the most recent medical record
            var symptom = _context
                .Symptom
                .Where(s =>
                    s.MedicalRecordId == medicalRecord.Id)
                .ToList()
                .Select(y =>
                {
                    SymptomDto temp = y.toDto();
                    temp.SymptomDirectoryDto.SymptomName = _context
                        .SymptomDirectory
                        .First(dir => dir.Id == y.SymptomDirectoryId)
                        .SymptomName;
                    return temp;
                })
                .ToList();

            return Ok(symptom);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}" );
        }

    }
}
