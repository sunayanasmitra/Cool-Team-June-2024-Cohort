using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class DietPlan
    {
        [Key]
        public int Id { get; set; }
        public int HealthPlanID { get; set; }
        public string DietGoal { get; set; } = null!;
        public bool IsChecked { get; set; }
    }
}
