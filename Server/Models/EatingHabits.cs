using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApp.Server.Models;

public class EatingHabits
{
    [Key]
    public int Id { get; set; }
    public int LifestyleRecordID { get; set; }
    public int CaloriesPerDay { get; set; }
    public int SugarIntakePerDay { get; set; }
    public int FatIntakePerDay { get; set; }
    public int ProteinIntakePerDay { get; set; }
    public int CholesterolIntakePerDay { get; set; }
    public int CarbIntakePerDay { get; set; }
    public int SodiumIntakePerDay { get; set; }
    [Column(TypeName ="decimal(2,2)")]
    public float VegetablePercentOfIntake { get; set; }
    [Column(TypeName = "decimal(2,2)")]
    public float MeatPercentOfIntake { get; set; }
    [Column(TypeName = "decimal(2,2)")]
    public float CerealsPercentofIntake { get; set; }
    [MaxLength(250)]
    public string? FoodRestriction { get; set; }

    //navigation properties

    public LifestyleRecord LifestyleRecord { get; set; }

}
