using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/DiagnosisDirectory")]
public class DiagnosisDirectoryController : Controller
{
	private readonly ApplicationDbContext _context;
	private UserManager<ApplicationUser> _userManager;

	public DiagnosisDirectoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	[HttpGet("/Get-DiagnosisDirectories")]
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

			var diagnosisDirectories = await _context.DiagnosisDirectory
				.Select(ad => new DiagnosisDirectoryDto
				{
					Id = ad.Id,
					DiagnosisName = ad.DiagnosisName,
				})
			.ToListAsync();

			return Ok(diagnosisDirectories);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}

	}
}
