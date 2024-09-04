using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareApp.Shared.Dto.BasicInformation;
using HealthCareApp.Shared.Dto.LifestyleRecord;
using HealthCareApp.Shared.Dto.MedicalRecord;

namespace HealthCareApp.Shared.Dto.MedicalDocument;

public class Medical360Dto
{
    public BasicInformationDto BasicInformation { get; set; }
    public ICollection<DiagnosisDto> Diagnoses { get; set; }
    public ICollection<AllergyDto> Allergies { get; set; }
    public ICollection<PhysicalActivitiesDto> PhysicalActivities { get; set; }
    public ICollection<MedicationDto> Medications { get; set; }
}

