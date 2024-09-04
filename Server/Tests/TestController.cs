using HealthCareApp.Server.Controllers;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HealthCareApp.Server.Tests
{

    [Route("Test/Controller")]
    public partial class TestController : ControllerBase
    {
        TestAssistantController TestAssistantController;
        MedicalRecordController MedicalRecordController;
        public TestController(
            TestAssistantController testAssistantController,
            MedicalRecordController medicalRecordController)
        {
            TestAssistantController = testAssistantController;
            MedicalRecordController = medicalRecordController;
        }

        [HttpGet("Run-All-Test")]
        public async Task<IActionResult> RunAllTests()
        {
            try
            {
                await TestRegisterUser();
                await TestMedicalRecord();
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.ToString());
            }
            return Ok();
        }



    }
}
