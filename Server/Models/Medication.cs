using HealthCareApp.Shared.Dto.MedicalRecord;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public int DrugDirectoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //navigation properties
        public DrugDirectory DrugDirectory { get; set; } = null!;

        public MedicationDto toDto()
        {
            return new MedicationDto()
            {
                Id = Id,
                MedicalRecordId = MedicalRecordId,
                DrugDirectoryId = DrugDirectoryId,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }
    }
}