﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.MedicalRecord;

public class SymptomDirectoryDto
{
    public int Id { get; set; }
    public string SymptomName { get; set; } = null!;
}
