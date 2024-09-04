using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApp.Server.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; } = null!;

        public DateTime RecordDate { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public Decimal BloodPressure { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public Decimal BloodGlucoseLevel { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public Decimal WeightInPounds { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public Decimal HeightInInches { get; set; }

        // Navigation Properties

        public ApplicationUser ApplicationUser { get; set; } = null!;

        public ICollection<Symptom> Symptom { get; set; } = new List<Symptom>();

        public ICollection<Diagnosis> Diagnosis { get; set; } = new List<Diagnosis>();

        public ICollection<Medication> Medication { get; set; } = new List<Medication>();

        public ICollection<Allergy> Allergy { get; set; } = new List<Allergy>();


    }
}
