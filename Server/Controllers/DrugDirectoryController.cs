using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/DrugDirectory")]
public class DrugDirectoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;

    public DrugDirectoryController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet("/Get-DrugDirectories")]
    public async Task<IActionResult> GetDrug()
    {
        try
        {
            //get the current user's ID
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            var drugDirectories = await _context.DrugDirectory
                .Select(dd => new DrugDirectoryDto
                {
                    Id = dd.Id,
                    DrugName = dd.DrugName,
                })
            .ToListAsync();

            return Ok(drugDirectories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }

    [HttpGet("Get-Drug/{id}")]
    public async Task<ActionResult<DrugDirectoryDto>> GetDrugDirectoryById(int id)
    {
        try
        {
            var drugDirectory = await _context.DrugDirectory
                .Where(d => d.Id == id)
                .Select(d => new DrugDirectoryDto
                {
                    Id = d.Id,
                    DrugName = d.DrugName
                })
                .FirstOrDefaultAsync();

            if (drugDirectory == null)
                return NotFound($"Drug directory with ID {id} not found.");

            return Ok(drugDirectory);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
