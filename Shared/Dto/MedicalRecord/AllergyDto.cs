using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class AllergyDto
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
	public int AllergyDirectoryId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public AllergyDirectoryDto AllergyDirectoryDto { get; set; } = new AllergyDirectoryDto();
}
