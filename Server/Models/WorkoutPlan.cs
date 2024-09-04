using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class WorkoutPlan
    {
        [Key]
        public int Id { get; set; }
        public int HealthPlanID { get; set; }
        public string WorkoutGoal { get; set; } = null!;
        public bool IsChecked { get; set; }
    }
}
