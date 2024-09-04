using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class Reminders
    {
        [Key]
        public int Id { get; set; }
        public int HealthPlanID { get; set; }
        public string Reminder { get; set; } = null!;
        public bool IsChecked { get; set; }

    }
}
