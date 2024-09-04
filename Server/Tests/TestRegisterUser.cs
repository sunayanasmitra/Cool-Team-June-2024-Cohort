using HealthCareApp.Server.Controllers;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HealthCareApp.Server.Tests
{

    public partial class TestController
    {
        private async Task TestRegisterUser()
        {
            ObjectResult res = (ObjectResult)await TestAssistantController.RegisterTestUser("Asdf@example.com", "Asdfasdf1!");
            Assert.IsTrue(res.StatusCode == 200);
        }

    }
}
