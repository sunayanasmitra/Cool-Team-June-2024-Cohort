using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class MedicalRecordDto
{
    public string ApplicationUserId { get; set; } = null!;
    public DateTime RecordDate { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public Decimal BloodPressure { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public Decimal BloodGlucoseLevel { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public Decimal WeightInPounds { get; set; }

    [Column(TypeName = "decimal(4,2)")]
    public Decimal HeightInInches { get; set; }

}
