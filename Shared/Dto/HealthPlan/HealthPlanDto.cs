using HealthCareApp.Shared.Dto.LifestyleRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.HealthPlan
{
    public class HealthPlanDto
    {
        public int Id { get; set; }
        public DateTime DateOfPlan { get; set; }
        public List<HealthRiskDto>? HealthRisk { get; set; }
        public List<DietPlanDto>? DietPlan { get; set; }
        public List<WorkoutPlanDto>? WorkoutPlan { get; set; }
        public List<RemindersDto>? Reminders { get; set; }


    }
}
