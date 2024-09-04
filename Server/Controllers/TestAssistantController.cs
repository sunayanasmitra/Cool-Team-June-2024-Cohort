using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Duende.IdentityServer.Extensions;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HealthCareApp.Server.Controllers
{
    [Route("api/Testing")]
    [Route("api/account")]
    public class TestAssistantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private IUserStore<ApplicationUser> _userStore;
        private SignInManager<ApplicationUser> _signinManager;

        public TestAssistantController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager
        )
        {
            _context = context;
            _userManager = userManager;
            _userStore = userStore;
            _signinManager = signInManager;
        }

        /// <summary>
        /// Method to Create a User for Testing.
        /// If User Already Exists will return UserId
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>string (userid)</returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpGet("/User/Register")]
        public async Task<IActionResult> RegisterTestUser([FromQuery] string email, string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user is not null)
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        await _signinManager.SignInAsync(user, false);
                        return Ok(user?.Id);
                    }
                    user = new ApplicationUser();
                    await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
                    await GetEmailStore().SetEmailAsync(user, email, CancellationToken.None);
                    var result = await _userManager.CreateAsync(user, password);
                    user = await _userManager.FindByEmailAsync(email);
                    if (user is not null)
                    {
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);
                        await _signinManager.SignInAsync(user, false);
                        return Ok(user?.Id);
                    }
                    return BadRequest(ModelState);
                }
                else
                    return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
        }

        [HttpGet("/User/Logout")]
        public async Task<IActionResult> RegisterLogoutUser()
        {
            await _signinManager.SignOutAsync();
            return Ok();
        }

        /// <summary>
        /// Delete all Data from Tables Within Database.
        /// </summary>
        /// <returns>HttpStatus Code 200</returns>
        [HttpPost("/Delete/All")]
        public async Task<IActionResult> DeleteAllData()
        {
            _context.ActivityDirectory.ExecuteDelete();
            _context.AlcoholHabits.ExecuteDelete();
            _context.Allergy.ExecuteDelete();
            _context.ApplicationUser.ExecuteDelete();
            _context.BasicInformation.ExecuteDelete();
            _context.Diagnosis.ExecuteDelete();
            _context.DiagnosisDirectory.ExecuteDelete();
            _context.DrugDirectory.ExecuteDelete();
            _context.DrugHabits.ExecuteDelete();
            _context.EatingHabits.ExecuteDelete();
            _context.LifestyleRecord.ExecuteDelete();
            _context.MedicalDocument.ExecuteDelete();
            _context.MedicalRecord.ExecuteDelete();
            _context.Medication.ExecuteDelete();
            _context.PhysicalActivities.ExecuteDelete();
            _context.Symptom.ExecuteDelete();
            _context.SymptomDirectory.ExecuteDelete();
            _context.VaccineRelation.ExecuteDelete();
            _context.VaccineDirectory.ExecuteDelete();
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetUsers")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        public IActionResult GetListOfUsers()
        {
            return Ok(
                _context
                    .ApplicationUser.Select(user => new UserDto()
                    {
                        email = user.Email,
                        password = user.PasswordHash
                    })
                    .ToList()
            );
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException(
                    "The default UI requires a user store with email support."
                );
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
