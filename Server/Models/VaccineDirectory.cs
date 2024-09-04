using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class VaccineDirectory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VaccineName { get; set; } = null!;

        
    }
}
