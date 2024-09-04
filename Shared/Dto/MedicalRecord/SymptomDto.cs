using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class SymptomDto
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public int SymptomDirectoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SymptomDirectoryDto SymptomDirectoryDto { get; set; } = new SymptomDirectoryDto();

}
