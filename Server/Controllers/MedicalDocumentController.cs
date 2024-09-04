using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace HealthCareApp.Server.Controllers;

[Authorize]
[Route("api/MedicalDocument")]
public class MedicalDocumentController : Controller
{
    private readonly ApplicationDbContext _context;
    private UserManager<ApplicationUser> _userManager;
    private readonly string _uploadPath;

    public MedicalDocumentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
        _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MedicalDocuments").Replace(@"\", "/");

        if (!Directory.Exists(_uploadPath))
        {
            Directory.CreateDirectory(_uploadPath);
        }
    }

    //upload a file
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var fileName = Path.GetFileName(file.FileName);
        var filePath = Path.Combine(_uploadPath, fileName).Replace("\\", "/");

        // Check if the file already exists in the database
        // Step 1: Retrieve all existing file names from the database
        var existingFileNames = await _context.MedicalDocument
            .Select(doc => Path.GetFileName(doc.FilePath))
            .ToListAsync();

        // Step 2: Check if the incoming file name already exists
        if (existingFileNames.Any(existingName => existingName.Equals(fileName, StringComparison.OrdinalIgnoreCase)))
        {
            return Conflict("A file with the same name already exists.");
        }

        // Save the file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Create the DTO for saving in the database
        var dto = new MedicalDocumentDto
        {
            ApplicationUserId = "",
            RecordDate = DateTime.UtcNow,
            UploadDate = DateTime.UtcNow,
            FilePath = Path.Combine("MedicalDocuments", fileName)
        };

        var createResult = await CreateMedicalDocument(dto);

        return Ok("File uploaded successfully.");
    }

    [HttpPost("/Create-MedicalDocument")]
    public async Task<IActionResult> CreateMedicalDocument([FromBody] MedicalDocumentDto Dto)
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
                //initializing medical document with data contained in Dto
                MedicalDocument medicalDocument = new MedicalDocument(Dto);
                medicalDocument.ApplicationUserId = userId;
                _context.MedicalDocument.Add(medicalDocument);
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

    [HttpPut("/Update-MedicalDocument/{id}")]
    public async Task<IActionResult> UpdateMedicalDocument(int id, [FromBody] MedicalDocumentDto Dto)
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

                // get the medicalDocument to update
                var medicalDocument = await _context.MedicalDocument.Where(d =>  d.Id == id && d.ApplicationUserId == userId).FirstOrDefaultAsync();

                if(medicalDocument == null)
                {
                    return NotFound($"Document with ID {id} not found.");
                }

                medicalDocument.RecordDate = Dto.RecordDate;
                medicalDocument.UploadDate = Dto.UploadDate;
                medicalDocument.FilePath = Dto.FilePath;

                _context.MedicalDocument.Update(medicalDocument);
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

    [HttpDelete("/Delete-MedicalDocument/{id}")]
    public async Task<IActionResult> DeleteMedicalDocument(int id)
    {
        try
        {
            //get the current user's ID
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            //find the existing medical document
            var medicalDocument = await _context.MedicalDocument.Where(d => d.Id == id && d.ApplicationUserId == userId).FirstOrDefaultAsync();

            if (medicalDocument == null)
            {
                return NotFound($"Document with ID {id} not found.");
            }

            // Remove the file from the local storage
            var filePath = Path.Combine("wwwroot/MedicalDocuments", Path.GetFileName(medicalDocument.FilePath));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            //Remove the medical record from database
            _context.MedicalDocument.Remove(medicalDocument);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("/Get-MedicalDocument")]
    public async Task<IActionResult> GetMedicalDocument()
    {
        try
        {
            //get the current user's ID
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            var medicalDocument = await _context.MedicalDocument
                .Where(mr => mr.ApplicationUserId == userId).ToListAsync();

            if (medicalDocument == null)
            {
                return NotFound("medicalDocument not found for the user.");
            }

            foreach (var document in medicalDocument)
            {
                document.FilePath = document.FilePath.Replace("\\", "/");
            }

            return Ok(medicalDocument);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
}
