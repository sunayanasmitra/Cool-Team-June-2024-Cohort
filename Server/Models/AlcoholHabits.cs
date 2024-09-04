using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class AlcoholHabits
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LifestyleRecordID { get; set; }
        public int DrinksPerWeek { get; set; }
        public int DrinksPerMonth { get; set; }
        public string PrimaryAlcoholType { get; set; }

        //navigation properties
        public LifestyleRecord LifestyleRecord { get; set; } = null!;
    }
}
