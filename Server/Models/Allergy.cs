using HealthCareApp.Shared.Dto.MedicalRecord;

namespace HealthCareApp.Server.Models
{
    public class Allergy
    {
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public int AllergyDirectoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        //Navigation properties
        public AllergyDirectory AllergyDirectory { get; set; } = null!;

        public AllergyDto toDto()
        {
            return new AllergyDto()
            {
                Id = Id,
                MedicalRecordId = MedicalRecordId,
                AllergyDirectoryId = AllergyDirectoryId,
                StartDate = StartDate,
                EndDate = EndDate
            };
        }

    }
}
