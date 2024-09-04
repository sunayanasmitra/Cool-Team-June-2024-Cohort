using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Shared.Dto.BasicInformation
{
    public class BasicInformationDto
    {
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        [MaxLength(13)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public string ApplicationUserId { get; set; }

        public int Id { get; set; }

    }
}
