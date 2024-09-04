namespace HealthCareApp.Server.Models
{
    public class Diagnosis
    {

        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public int DiagnosisDirectoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation Properties

        public MedicalRecord MedicalRecord { get; set; } = null!;
    }
}
