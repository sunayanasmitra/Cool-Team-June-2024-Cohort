using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApp.Server.Models;

public class ApplicationUser : IdentityUser
{

    //navigation property
    public BasicInformation BasicInformation { get; set; } = null!;
    public ICollection<LifestyleRecord> LifestyleRecords { get; set; } = null!;
    public ICollection<VaccineRelation> VaccineRelations { get; set; } = null!;
    public MedicalRecord MedicalRecords { get; set; } = null!;
    public ICollection<MedicalDocument> MedicalDocuments { get; set; } = null!;
    public HealthPlan HealthPlan { get; set; } = null!;

}
