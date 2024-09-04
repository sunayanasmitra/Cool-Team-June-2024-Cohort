using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.HealthPlan
{
    public class HealthRiskDto
    {
        public int Id { get; set; }

        public int HealthPlanID { get; set; }
        public string Risk { get; set; }
        public string Reason { get; set; }
    }
}
