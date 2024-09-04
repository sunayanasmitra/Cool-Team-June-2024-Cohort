using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class DiagnosisDirectoryDto
{
    public int Id { get; set; }
    public string DiagnosisName { get; set; } = null!;
}
