using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using HealthCareApp.Server.Models;
using System.Reflection.Emit;

namespace HealthCareApp.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<VaccineDirectory>().HasData(
            new VaccineDirectory() { Id = 1, VaccineName = "Covid Pfizer" },
            new VaccineDirectory() { Id = 2, VaccineName = "Moderna Covid" },
            new VaccineDirectory() { Id = 3, VaccineName = "Johnson and Johnson Covid" }
        );

        base.OnModelCreating(builder);
        builder.Entity<ActivityDirectory>().HasData(
            new ActivityDirectory() { Id = 1, ActivityName = "Running" },
            new ActivityDirectory() { Id = 2, ActivityName = "Walking" },
            new ActivityDirectory() { Id = 3, ActivityName = "Yoga" },
            new ActivityDirectory() { Id = 4, ActivityName = "Cycling" },
            new ActivityDirectory() { Id = 5, ActivityName = "Swimming" },
            new ActivityDirectory() { Id = 6, ActivityName = "Hiking" },
            new ActivityDirectory() { Id = 7, ActivityName = "Pilates" },
            new ActivityDirectory() { Id = 8, ActivityName = "Rowing" },
            new ActivityDirectory() { Id = 9, ActivityName = "Elliptical" },
            new ActivityDirectory() { Id = 10, ActivityName = "Stair Stepper" },
            new ActivityDirectory() { Id = 11, ActivityName = "HIIT (High Intensity Interval Training" },
            new ActivityDirectory() { Id = 12, ActivityName = "Strength Training" },
            new ActivityDirectory() { Id = 13, ActivityName = "Core Training" },
            new ActivityDirectory() { Id = 14, ActivityName = "Dance" },
            new ActivityDirectory() { Id = 15, ActivityName = "Water Fitness" },
            new ActivityDirectory() { Id = 16, ActivityName = "CrossFit" },
            new ActivityDirectory() { Id = 17, ActivityName = "Functional Training" },
            new ActivityDirectory() { Id = 18, ActivityName = "Skiing/Snowboarding" },
            new ActivityDirectory() { Id = 19, ActivityName = "Golf" },
            new ActivityDirectory() { Id = 20, ActivityName = "Tennis" },
            new ActivityDirectory() { Id = 21, ActivityName = "Basketball" },
            new ActivityDirectory() { Id = 22, ActivityName = "Soccer" },
            new ActivityDirectory() { Id = 23, ActivityName = "Table Tennis" },
            new ActivityDirectory() { Id = 24, ActivityName = "Badminton" },
            new ActivityDirectory() { Id = 25, ActivityName = "Volleyball" },
            new ActivityDirectory() { Id = 26, ActivityName = "Baseball/Softball" },
            new ActivityDirectory() { Id = 27, ActivityName = "Rock Climbing" },
            new ActivityDirectory() { Id = 28, ActivityName = "Martial Arts" },
            new ActivityDirectory() { Id = 29, ActivityName = "Tai Chi" },
            new ActivityDirectory() { Id = 30, ActivityName = "Mixed Cardio" },
            new ActivityDirectory() { Id = 31, ActivityName = "Other" }
        );

        builder.Entity<AllergyDirectory>().HasData(
            new AllergyDirectory { Id = 1, AllergyName = "Peanuts" },
            new AllergyDirectory { Id = 2, AllergyName = "Shellfish" },
            new AllergyDirectory { Id = 3, AllergyName = "Tree Nuts" },
            new AllergyDirectory { Id = 4, AllergyName = "Milk" },
            new AllergyDirectory { Id = 5, AllergyName = "Eggs" },
            new AllergyDirectory { Id = 6, AllergyName = "Soy" },
            new AllergyDirectory { Id = 7, AllergyName = "Wheat" },
            new AllergyDirectory { Id = 8, AllergyName = "Fish" },
            new AllergyDirectory { Id = 9, AllergyName = "Sesame" },
            new AllergyDirectory { Id = 10, AllergyName = "Mustard" },
            new AllergyDirectory { Id = 11, AllergyName = "Sulfites" },
            new AllergyDirectory { Id = 12, AllergyName = "Latex" },
            new AllergyDirectory { Id = 13, AllergyName = "Pollen" },
            new AllergyDirectory { Id = 14, AllergyName = "Dust Mites" },
            new AllergyDirectory { Id = 15, AllergyName = "Mold" },
            new AllergyDirectory { Id = 16, AllergyName = "Pet Dander" },
            new AllergyDirectory { Id = 17, AllergyName = "Insect Stings" },
            new AllergyDirectory { Id = 18, AllergyName = "Medications" },
            new AllergyDirectory { Id = 19, AllergyName = "Fragrances" },
            new AllergyDirectory { Id = 20, AllergyName = "Nickel" }
        );

        builder.Entity<SymptomDirectory>().HasData(
            new SymptomDirectory { Id = 1, SymptomName = "Fever" },
            new SymptomDirectory { Id = 2, SymptomName = "Cough" },
            new SymptomDirectory { Id = 3, SymptomName = "Shortness of Breath" },
            new SymptomDirectory { Id = 4, SymptomName = "Fatigue" },
            new SymptomDirectory { Id = 5, SymptomName = "Headache" },
            new SymptomDirectory { Id = 6, SymptomName = "Sore Throat" },
            new SymptomDirectory { Id = 7, SymptomName = "Runny Nose" },
            new SymptomDirectory { Id = 8, SymptomName = "Muscle Aches" },
            new SymptomDirectory { Id = 9, SymptomName = "Chills" },
            new SymptomDirectory { Id = 10, SymptomName = "Nausea" },
            new SymptomDirectory { Id = 11, SymptomName = "Vomiting" },
            new SymptomDirectory { Id = 12, SymptomName = "Diarrhea" },
            new SymptomDirectory { Id = 13, SymptomName = "Chest Pain" },
            new SymptomDirectory { Id = 14, SymptomName = "Abdominal Pain" },
            new SymptomDirectory { Id = 15, SymptomName = "Rash" },
            new SymptomDirectory { Id = 16, SymptomName = "Dizziness" },
            new SymptomDirectory { Id = 17, SymptomName = "Swelling" },
            new SymptomDirectory { Id = 18, SymptomName = "Joint Pain" },
            new SymptomDirectory { Id = 19, SymptomName = "Confusion" },
            new SymptomDirectory { Id = 20, SymptomName = "Loss of Appetite" },
            new SymptomDirectory { Id = 21, SymptomName = "Coughing Up Blood" },
            new SymptomDirectory { Id = 22, SymptomName = "Wheezing" },
            new SymptomDirectory { Id = 23, SymptomName = "Difficulty Swallowing" },
            new SymptomDirectory { Id = 24, SymptomName = "Loss of Taste" },
            new SymptomDirectory { Id = 25, SymptomName = "Loss of Smell" },
            new SymptomDirectory { Id = 26, SymptomName = "Frequent Urination" },
            new SymptomDirectory { Id = 27, SymptomName = "Bloody Urine" },
            new SymptomDirectory { Id = 28, SymptomName = "Yellowing of Skin" },
            new SymptomDirectory { Id = 29, SymptomName = "Difficulty Breathing" },
            new SymptomDirectory { Id = 30, SymptomName = "Chest Tightness" }
        );

        builder.Entity<DiagnosisDirectory>().HasData(
            new DiagnosisDirectory { Id = 1, DiagnosisName = "Diabetes Mellitus" },
            new DiagnosisDirectory { Id = 2, DiagnosisName = "Hypertension" },
            new DiagnosisDirectory { Id = 3, DiagnosisName = "Asthma" },
            new DiagnosisDirectory { Id = 4, DiagnosisName = "Chronic Obstructive Pulmonary Disease (COPD)" },
            new DiagnosisDirectory { Id = 5, DiagnosisName = "Coronary Artery Disease" },
            new DiagnosisDirectory { Id = 6, DiagnosisName = "Stroke" },
            new DiagnosisDirectory { Id = 7, DiagnosisName = "Parkinson's Disease" },
            new DiagnosisDirectory { Id = 8, DiagnosisName = "Rheumatoid Arthritis" },
            new DiagnosisDirectory { Id = 9, DiagnosisName = "Multiple Sclerosis" },
            new DiagnosisDirectory { Id = 10, DiagnosisName = "Alzheimer's Disease" },
            new DiagnosisDirectory { Id = 11, DiagnosisName = "Epilepsy" },
            new DiagnosisDirectory { Id = 12, DiagnosisName = "Cancer" },
            new DiagnosisDirectory { Id = 13, DiagnosisName = "Chronic Kidney Disease" },
            new DiagnosisDirectory { Id = 14, DiagnosisName = "Gastroesophageal Reflux Disease (GERD)" },
            new DiagnosisDirectory { Id = 15, DiagnosisName = "Peptic Ulcer Disease" },
            new DiagnosisDirectory { Id = 16, DiagnosisName = "Psoriasis" },
            new DiagnosisDirectory { Id = 17, DiagnosisName = "Hypothyroidism" },
            new DiagnosisDirectory { Id = 18, DiagnosisName = "Hyperthyroidism" },
            new DiagnosisDirectory { Id = 19, DiagnosisName = "Obesity" },
            new DiagnosisDirectory { Id = 20, DiagnosisName = "Sleep Apnea" },
            new DiagnosisDirectory { Id = 21, DiagnosisName = "Chronic Sinusitis" },
            new DiagnosisDirectory { Id = 22, DiagnosisName = "Bronchitis" },
            new DiagnosisDirectory { Id = 23, DiagnosisName = "Anemia" },
            new DiagnosisDirectory { Id = 24, DiagnosisName = "Tuberculosis" },
            new DiagnosisDirectory { Id = 25, DiagnosisName = "Hepatitis B" },
            new DiagnosisDirectory { Id = 26, DiagnosisName = "Hepatitis C" },
            new DiagnosisDirectory { Id = 27, DiagnosisName = "Scleroderma" },
            new DiagnosisDirectory { Id = 28, DiagnosisName = "Lupus" },
            new DiagnosisDirectory { Id = 29, DiagnosisName = "Chronic Fatigue Syndrome" },
            new DiagnosisDirectory { Id = 30, DiagnosisName = "Cystic Fibrosis" }
        );

        builder.Entity<DrugDirectory>().HasData(
            new DrugDirectory { Id = 1, DrugName = "Aspirin" },
            new DrugDirectory { Id = 2, DrugName = "Ibuprofen" },
            new DrugDirectory { Id = 3, DrugName = "Acetaminophen" },
            new DrugDirectory { Id = 4, DrugName = "Amoxicillin" },
            new DrugDirectory { Id = 5, DrugName = "Metformin" },
            new DrugDirectory { Id = 6, DrugName = "Lisinopril" },
            new DrugDirectory { Id = 7, DrugName = "Atorvastatin" },
            new DrugDirectory { Id = 8, DrugName = "Simvastatin" },
            new DrugDirectory { Id = 9, DrugName = "Hydrochlorothiazide" },
            new DrugDirectory { Id = 10, DrugName = "Losartan" },
            new DrugDirectory { Id = 11, DrugName = "Omeprazole" },
            new DrugDirectory { Id = 12, DrugName = "Esomeprazole" },
            new DrugDirectory { Id = 13, DrugName = "Gabapentin" },
            new DrugDirectory { Id = 14, DrugName = "Hydrocodone" },
            new DrugDirectory { Id = 15, DrugName = "Oxycodone" },
            new DrugDirectory { Id = 16, DrugName = "Sertraline" },
            new DrugDirectory { Id = 17, DrugName = "Fluoxetine" },
            new DrugDirectory { Id = 18, DrugName = "Alprazolam" },
            new DrugDirectory { Id = 19, DrugName = "Clonazepam" },
            new DrugDirectory { Id = 20, DrugName = "Levothyroxine" },
            new DrugDirectory { Id = 21, DrugName = "Metoprolol" },
            new DrugDirectory { Id = 22, DrugName = "Amlodipine" },
            new DrugDirectory { Id = 23, DrugName = "Furosemide" },
            new DrugDirectory { Id = 24, DrugName = "Azithromycin" },
            new DrugDirectory { Id = 25, DrugName = "Ciprofloxacin" },
            new DrugDirectory { Id = 26, DrugName = "Doxycycline" },
            new DrugDirectory { Id = 27, DrugName = "Prednisone" },
            new DrugDirectory { Id = 28, DrugName = "Methylprednisolone" },
            new DrugDirectory { Id = 29, DrugName = "Insulin" },
            new DrugDirectory { Id = 30, DrugName = "Ceftriaxone" }
        );

        builder.Entity<LifestyleRecord>()
            .HasMany(lr => lr.DrugHabits)
            .WithOne(dh => dh.LifestyleRecord)
            .HasForeignKey(dh => dh.LifestyleRecordID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LifestyleRecord>()
            .HasMany(lr => lr.EatingHabits)
            .WithOne(eh => eh.LifestyleRecord)
            .HasForeignKey(eh => eh.LifestyleRecordID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LifestyleRecord>()
            .HasMany(lr => lr.AlcoholHabits)
            .WithOne(ah => ah.LifestyleRecord)
            .HasForeignKey(ah => ah.LifestyleRecordID)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LifestyleRecord>()
            .HasMany(lr => lr.PhysicalActivities)
            .WithOne(pa => pa.LifestyleRecord)
            .HasForeignKey(pa => pa.LifestyleRecordID)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<BasicInformation> BasicInformation { get; set; }
    public DbSet<LifestyleRecord> LifestyleRecord { get; set; }
    public DbSet<EatingHabits> EatingHabits { get; set; }
    public DbSet<AlcoholHabits> AlcoholHabits { get; set; }
    public DbSet<DrugHabits> DrugHabits { get; set; }
    public DbSet<PhysicalActivities> PhysicalActivities { get; set; }
    public DbSet<ActivityDirectory> ActivityDirectory { get; set; }
    public DbSet<VaccineRelation> VaccineRelation { get; set; }
    public DbSet<VaccineDirectory> VaccineDirectory { get; set; }
    public DbSet<MedicalRecord> MedicalRecord { get; set; }
    public DbSet<MedicalDocument> MedicalDocument { get; set; }
    public DbSet<Symptom> Symptom { get; set; }
    public DbSet<SymptomDirectory> SymptomDirectory { get; set; }
    public DbSet<Diagnosis> Diagnosis { get; set; }
    public DbSet<DiagnosisDirectory> DiagnosisDirectory { get; set; }
    public DbSet<Medication> Medication { get; set; }
    public DbSet<DrugDirectory> DrugDirectory { get; set; }
    public DbSet<Allergy> Allergy { get; set; }
    public DbSet<AllergyDirectory> AllergyDirectory { get; set; }
    public DbSet<HealthPlan> HealthPlan { get; set; }
    public DbSet<HealthRisk> HealthRisk { get; set; }
    public DbSet<DietPlan> DietPlan { get; set; }
    public DbSet<WorkoutPlan> WorkoutPlan { get; set; }
    public DbSet<Reminders> Reminders { get; set; }
}
