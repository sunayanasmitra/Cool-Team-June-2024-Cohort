using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.BasicInformation;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using HealthCareApp.Shared.Dto.MedicalDocument;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareApp.Server.Controllers
{
    public class Medical360Controller : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public Medical360Controller(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("/Get-Medical360")]
        public async Task<IActionResult> Get360MedicalProfile()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Medical360Dto medical360Dto = new Medical360Dto();
                    var physicalActivities = _context.PhysicalActivities.Find(await _userManager.GetUserAsync(this.User));

                    var dto = new PhysicalActivitiesDto()
                    {
                        TimesPerWeek = physicalActivities.TimesPerWeek
                    };
                    medical360Dto.PhysicalActivities.Add(dto);
                    return Ok();
                }
                else
                    return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }
    }
}

