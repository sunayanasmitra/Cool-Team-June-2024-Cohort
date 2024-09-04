using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class HealthRisk
    {
        [Key]
        public int Id { get; set; }
        public int HealthPlanID { get; set; }
        public string Risk { get; set; }
        public string Reason { get; set; }

    }
}
