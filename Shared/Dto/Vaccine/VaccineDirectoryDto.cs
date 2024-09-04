using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.Vaccine;

public class VaccineDirectoryDto
{
	public int Id { get; set; }
	public string VaccineName { get; set; } = null!;
}
