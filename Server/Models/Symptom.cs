using HealthCareApp.Shared.Dto.MedicalRecord;

namespace HealthCareApp.Server.Models
{
    public class Symptom
    {

        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public int SymptomDirectoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //navigation properties

        public SymptomDirectory symptomDirectory { get; set; } = null!;

        public SymptomDto toDto()
        {
            return new SymptomDto()
            {
                Id = Id,
                MedicalRecordId = MedicalRecordId,
                SymptomDirectoryId = SymptomDirectoryId,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }

    }
}
