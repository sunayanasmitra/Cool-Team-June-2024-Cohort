using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalDocument;

public class MedicalDocumentDto
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public DateTime RecordDate { get; set; }
    public DateTime UploadDate { get; set; }
    [Required]
    public string FilePath { get; set; } = null!;
}
