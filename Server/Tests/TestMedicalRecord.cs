using HealthCareApp.Server.Controllers;
using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.MedicalRecord;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HealthCareApp.Server.Tests
{

    public partial class TestController
    {
        private async Task TestMedicalRecord()
        {
            TestCreateMedicalRecord();
        }

        private async Task TestCreateMedicalRecord()
        {
            ObjectResult res = (ObjectResult)await MedicalRecordController.CreateMedicalRecord(new MedicalRecordDto() { });
            Assert.IsTrue(res.StatusCode == 200);
        }

    }
}
