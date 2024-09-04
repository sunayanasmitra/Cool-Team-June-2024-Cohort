using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.LifestyleRecord
{
    public class DrugHabitsDto
    {
        public int Id { get; set; }
        public int LifestyleRecordID { get; set; }
        public int DosesPerWeek { get; set; }
        public int DosesPerMonth { get; set; }
        public int DrugDirectoryId { get; set; }
    }
}
