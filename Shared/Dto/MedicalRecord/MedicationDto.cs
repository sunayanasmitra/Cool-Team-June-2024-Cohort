using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class MedicationDto
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public int DrugDirectoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DrugDirectoryDto DrugDirectoryDto { get; set; } = new DrugDirectoryDto();

}
