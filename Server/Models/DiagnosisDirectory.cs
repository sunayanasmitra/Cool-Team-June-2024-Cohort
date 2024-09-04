namespace HealthCareApp.Server.Models
{
    public class DiagnosisDirectory
    {
        public int Id { get; set; }
        public string DiagnosisName { get; set; }

        //navigation properties
        public ICollection<Diagnosis> Diagnosis { get; set; } = null!;

    }
}
