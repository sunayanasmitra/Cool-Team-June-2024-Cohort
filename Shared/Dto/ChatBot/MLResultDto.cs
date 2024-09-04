using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareApp.Shared.Dto.ChatBot
{
    public class MlResultDto
    {
        public List<string> diagnosis { get; set; }
        public List<string> symptoms { get; set; }
    }
}
