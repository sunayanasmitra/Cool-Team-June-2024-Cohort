namespace HealthCareApp.Server.Models
{
    public class PhysicalActivities
    {
        public int Id { get; set; }
        public int LifestyleRecordID { get; set; }
        public int ActivityDirectoryId { get; set; }
        public int TimesPerWeek { get; set; }
        //Navigation
        public ActivityDirectory ActivityDirectory { get; set; } = null!;
        public LifestyleRecord LifestyleRecord { get; set; } = null!;
    }
}