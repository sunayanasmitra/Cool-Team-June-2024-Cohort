using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApp.Server.Models
{
    public class DrugHabits
    {
        [Key]
        public int Id { get; set; }
        public int LifestyleRecordID { get; set; }
        public int DrugDirectoryId { get; set; }
        public int DosesPerWeek { get; set; }
        public int DosesPerMonth { get; set; }

        //navigation properties

        public LifestyleRecord LifestyleRecord { get; set; } = null!;
        public DrugDirectory DrugDirectory { get; set; } = null!;

        
    }
}
