using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.HealthPlan
{
    public class WorkoutPlanDto
    {
        public int Id { get; set; }
        public int HealthPlanID { get; set; }
        public string WorkoutGoal { get; set; }
        public bool IsChecked { get; set; }
    }
}
