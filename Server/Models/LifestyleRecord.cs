using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApp.Server.Models;

public class LifestyleRecord
{
    [Key]
    public int Id { get; set; }

    public string ApplicationUserId { get; set; } = null!;

    public DateTime RecordDate { get; set; }

    //Navigation
    public ICollection<EatingHabits> EatingHabits { get; set; } = new List<EatingHabits>();
    public ICollection<AlcoholHabits> AlcoholHabits { get; set; } = new List<AlcoholHabits>();
    public ICollection<DrugHabits> DrugHabits { get; set; } = new List<DrugHabits>();
    public ICollection<PhysicalActivities> PhysicalActivities { get; set; } = new List<PhysicalActivities>();
}
