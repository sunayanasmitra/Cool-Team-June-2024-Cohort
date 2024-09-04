using System.ComponentModel.DataAnnotations;
using HealthCareApp.Shared.Dto.Vaccine;

namespace HealthCareApp.Server.Models
{
    public class VaccineRelation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; } = null!;
        public int VaccineDirectoryId { get; set; }
        public DateTime? DateAdministered { get; set; }

        public string FilePath { get; set; } = null!;

        // Navigation Properties
        public ApplicationUser ApplicationUser { get; set; } = null!;

        // Constructors

        public VaccineRelation() { }

        public VaccineRelation(VaccineRelationDto dto)
        {
            ApplicationUserId = dto.ApplicationUserId;
            VaccineDirectoryId = dto.VaccineDirectoryId;
            DateAdministered = dto.DateAdministered;
            FilePath = dto.FilePath;
        }

        public VaccineRelationDto ToDto()
        {

            return new VaccineRelationDto()
            {
                ApplicationUserId = this.ApplicationUserId,
                VaccineDirectoryId = this.VaccineDirectoryId,
                DateAdministered = this.DateAdministered,
                FilePath = this.FilePath
            };
        }
    }
}
