using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class HealthPlan
    {
        [Key]
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public DateTime DateOfPlan { get; set; }

        //Navigation
        public ICollection<HealthRisk> HealthRisk { get; set; } = null!;
        public ICollection<DietPlan> DietPlan { get; set; } = null!;
        public ICollection<WorkoutPlan> WorkoutPlan { get; set; } = null!;
        public ICollection<Reminders> Reminders { get; set; } = null!;
    }
}
