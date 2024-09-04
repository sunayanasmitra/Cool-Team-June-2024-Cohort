using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.LifestyleRecord
{
    public class EatingHabitsDto
    {
        public int Id { get; set; }
        public int LifestyleRecordID { get; set; }
        public int CaloriesPerDay { get; set; }
        public int SugarIntakePerDay { get; set; }
        public int FatIntakePerDay { get; set; }
        public int ProteinIntakePerDay { get; set; }
        public int CholesterolIntakePerDay { get; set; }
        public int CarbIntakePerDay { get; set; }
        public int SodiumIntakePerDay { get; set; }
        [Column(TypeName = "decimal(2,2)")]
        public float VegetablePercentOfIntake { get; set; }
        [Column(TypeName = "decimal(2,2)")]
        public float MeatPercentOfIntake { get; set; }
        [Column(TypeName = "decimal(2,2)")]
        public float CerealsPercentofIntake { get; set; }
        [MaxLength(250)]
        public string? FoodRestriction { get; set; }
    }
}
