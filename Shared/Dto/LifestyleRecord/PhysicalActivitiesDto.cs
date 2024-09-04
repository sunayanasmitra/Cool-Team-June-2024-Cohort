using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.LifestyleRecord
{
    public class PhysicalActivitiesDto
    {
        public int Id { get; set; }
        public int LifestyleRecordID { get; set; }
        public int TimesPerWeek { get; set; }
        public int ActivityDirectoryId { get; set; }
    }
}
