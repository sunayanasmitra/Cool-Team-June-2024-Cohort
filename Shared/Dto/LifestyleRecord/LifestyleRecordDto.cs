using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.LifestyleRecord
{
    public class LifestyleRecordDto
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }
        public List<EatingHabitsDto>? EatingHabits { get; set; }
        public List<AlcoholHabitsDto>? AlcoholHabits { get; set; }
        public List<DrugHabitsDto>? DrugHabits { get; set; }
        public List<PhysicalActivitiesDto>? PhysicalActivities { get; set; }
    }
}