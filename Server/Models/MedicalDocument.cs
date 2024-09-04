using HealthCareApp.Shared.Dto.MedicalDocument;
using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class MedicalDocument
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        public DateTime RecordDate { get; set; }
        public DateTime UploadDate { get; set; }
        [Required]
        public string FilePath { get; set; } = null!;

        /*
         * If we want it to be stored in the database differently:
        [Required]
        public string FileName { get; set; }
        [Required]
        public byte[] FileData { get; set; }
        public string ContentType { get; set; }
        */

        //navigation properties
        ApplicationUser ApplicationUser { get; set; } = null!;

        //defining constructors
        public MedicalDocument() { }

        public MedicalDocument(MedicalDocumentDto dto)
        {
            ApplicationUserId = dto.ApplicationUserId;
            RecordDate = dto.RecordDate;
            UploadDate = dto.UploadDate;
            FilePath = dto.FilePath;
        }

        public MedicalDocumentDto toDto()
        {
            return new MedicalDocumentDto()
            {
                ApplicationUserId = this.ApplicationUserId,
                RecordDate = this.RecordDate,
                UploadDate = this.UploadDate,
                FilePath = this.FilePath
            };
        }
    }
}
