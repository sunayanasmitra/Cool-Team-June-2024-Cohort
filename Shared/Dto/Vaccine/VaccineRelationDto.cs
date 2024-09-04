using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.Vaccine;

public class VaccineRelationDto
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public int VaccineDirectoryId { get; set; }
    public DateTime? DateAdministered { get; set; }
    public string FilePath { get; set; } = null!;
}
