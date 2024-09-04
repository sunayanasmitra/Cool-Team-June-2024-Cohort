using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.HealthPlan
{
    public class DietPlanDto
    {
        public int Id { get; set; }
        public int HealthPlanID { get; set; }
        public string DietGoal { get; set; }
        public bool IsChecked { get; set; }
    }
}
