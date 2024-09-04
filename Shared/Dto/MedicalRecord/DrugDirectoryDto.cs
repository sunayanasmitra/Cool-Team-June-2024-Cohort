using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class DrugDirectoryDto
{
    public int Id { get; set; }
    public string DrugName { get; set; } = null!;
}
