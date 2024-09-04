using HealthCareApp.Shared.Dto.BasicInformation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareApp.Server.Models
{
    public class BasicInformation
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        [MaxLength(13)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        //navigation properties
        ApplicationUser ApplicationUser { get; set; } = null!;

        public BasicInformation() { }
        public BasicInformation(BasicInformationDto dto)
        {
            ApplicationUserId = dto.ApplicationUserId;
            Gender = dto.Gender;
            PhoneNumber = dto.PhoneNumber;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            DOB = dto.DOB;
            Id = dto.Id;
        }

        public BasicInformationDto toDto()
        {
            return new BasicInformationDto()
            {
                ApplicationUserId = this.ApplicationUserId,
                Gender = this.Gender,
                DOB = this.DOB,
                PhoneNumber = this.PhoneNumber,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Id = this.Id
            };
        }

    }
}
