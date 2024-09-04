using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/Diagnosis")]
public class DiagnosisController : Controller
{
	private readonly ApplicationDbContext _context;
	private UserManager<ApplicationUser> _userManager;

	public DiagnosisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	[HttpPost("/Create-Diagnosis")]
	public async Task<IActionResult> CreateDiagnosis([FromBody] DiagnosisDto Dto)
	{
		try
		{
			if (ModelState.IsValid)
			{
				//get the current user's Id
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

				Diagnosis diagnosis = new Diagnosis()
				{
					StartDate = Dto.StartDate,
					EndDate = Dto.EndDate,
					MedicalRecordId = medicalRecord.Id,
					DiagnosisDirectoryId = Dto.DiagnosisDirectoryId,
				};

				_context.Diagnosis.Add(diagnosis);
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

	[HttpPut("/Update-Diagnosis/{id}")]
	public async Task<IActionResult> UpdateDiagnosis(int id, [FromBody] DiagnosisDto Dto)
	{
		try
		{
			if (ModelState.IsValid)
			{
				//get the current user's Id
				var userId = _userManager.GetUserId(User);
				if (userId == null)
				{
					return Unauthorized("User not authenticated");
				}

				//get the diagnosis to update
				var diagnosis = await _context.Diagnosis.FindAsync(id);
				if (diagnosis == null)
				{
					return NotFound($"diagnosis with ID {id} not found.");
				}

				//get the medical record of current user
				var medicalRecord = await _context.MedicalRecord
					.FirstOrDefaultAsync(mr => mr.ApplicationUserId == userId && mr.Id == diagnosis.MedicalRecordId);

				if (medicalRecord == null)
				{
					return Unauthorized("You do not have permission to update this diagnosis.");
				}

				//update diagnosiss details
				diagnosis.StartDate = Dto.StartDate;
				diagnosis.EndDate = Dto.EndDate;

				_context.Diagnosis.Update(diagnosis);
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

	[HttpDelete("/Delete-Diagnosis/{id}")]
	public async Task<IActionResult> DeleteDiagnosis(int id)
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


				//get the diagnosis to delete
				var diagnosis = await _context.Diagnosis.FindAsync(id);
				if (diagnosis == null)
				{
					return NotFound($"Diagnosis with ID {id} not found.");
				}

				//get the medical record of current user
				var medicalRecord = _context.MedicalRecord
					.FirstOrDefault(mr => mr.Id == diagnosis.MedicalRecordId && mr.ApplicationUserId == userId);

				if (medicalRecord == null)
				{
					return NotFound("You do not have permission to delete this diagnosis.");
				}

				//Remove the diagnosis
				_context.Diagnosis.Remove(diagnosis);
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

	[HttpGet("/Get-Diagnosis")]
	public async Task<IActionResult> GetDiagnosis()
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

			// retrieve diagnosiss for the most recent medical record
			var diagnosis = await _context.Diagnosis.Where(s => s.MedicalRecordId == medicalRecord.Id)
				.ToListAsync();

			return Ok(diagnosis.Select(D => new DiagnosisDto()
			{
				Id = D.Id,
				StartDate = D.StartDate,
				EndDate = D.EndDate,
				DiagnosisDirectoryId = D.DiagnosisDirectoryId,
				//MedicalRecordId = medicalRecord.Id,
				DiagnosisDirectoryDto = new DiagnosisDirectoryDto()
				{
					Id = D.DiagnosisDirectoryId,
					DiagnosisName = _context.DiagnosisDirectory.Where(Dir => Dir.Id == D.DiagnosisDirectoryId).First().DiagnosisName,
				}
			}).ToList());
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}

	}

}
