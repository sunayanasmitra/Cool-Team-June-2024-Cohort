using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.LifestyleRecord
{
    public class AlcoholHabitsDto
    {
        public int Id { get; set; }
        public int LifestyleRecordID { get; set; }
        public int DrinksPerWeek { get; set; }
        public int DrinksPerMonth { get; set; }
        public string PrimaryAlcoholType { get; set; }
    }
}
