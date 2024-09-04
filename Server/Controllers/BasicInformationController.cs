using Duende.IdentityServer.Extensions;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.BasicInformation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/BasicInformation")]
    public class BasicInformationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public BasicInformationController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("/Create-BasicInformation")]
        public async Task<IActionResult> CreateBasicInformation([FromBody] BasicInformationDto Dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BasicInformation basicInformation = new BasicInformation(Dto);
                    basicInformation.ApplicationUserId = _userManager.GetUserId(this.User)!;
                    await _context.BasicInformation.AddAsync(basicInformation);
                    _context.SaveChanges();
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

        [HttpPost("/Update-BasicInformation")]
        public IActionResult UpdateBasicInformation([FromBody] BasicInformationDto Dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BasicInformation basicInformation = new BasicInformation(Dto);
                    basicInformation.ApplicationUserId = _userManager.GetUserId(this.User)!;
                    _context.BasicInformation.Update(basicInformation);
                    _context.SaveChanges();
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

        [HttpGet("/Delete-BasicInformation")]
        public async Task<IActionResult> DeleteBasicInformation()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BasicInformation basicInformation = _context
                        .BasicInformation.Where(b =>
                            b.ApplicationUserId == _userManager.GetUserId(this.User)!
                        )
                        .FirstOrDefault()!;
                    _context.BasicInformation.Remove(basicInformation);
                    _context.SaveChanges();
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

        [HttpGet("/Get-BasicInformation")]
        public async Task<IActionResult> GetBasicInformation()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = (await _userManager.GetUserAsync(this.User))!;
                    var dto = _context
                        .BasicInformation.Where(b => b.ApplicationUserId == user.Id)
                        .First()
                        .toDto();
                    return Ok(dto);
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
