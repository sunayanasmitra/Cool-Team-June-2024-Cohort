using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/ActivityDirectory")]
public class ActivityDirectoriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ActivityDirectoriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("Get-All-ActivityDirectories")]
    public async Task<ActionResult<List<ActivityDirectoryDto>>> GetAllActivityDirectories()
    {
        try
        {
            var activityDirectories = await _context.ActivityDirectory
                .Select(a => new ActivityDirectoryDto
                {
                    Id = a.Id,
                    ActivityName = a.ActivityName
                })
                .ToListAsync();

            if (activityDirectories == null || !activityDirectories.Any())
                return NotFound("No activity directories found.");

            return Ok(activityDirectories);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("Get-Activity/{id}")]
    public async Task<ActionResult<ActivityDirectoryDto>> GetActivityDirectoryById(int id)
    {
        try
        {
            var activityDirectory = await _context.ActivityDirectory
                .Where(d => d.Id == id)
                .Select(d => new ActivityDirectoryDto
                {
                    Id = d.Id,
                    ActivityName = d.ActivityName
                })
                .FirstOrDefaultAsync();

            if (activityDirectory == null)
                return NotFound($"Activity directory with ID {id} not found.");

            return Ok(activityDirectory);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
