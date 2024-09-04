using System.ComponentModel.DataAnnotations;

namespace HealthCareApp.Server.Models
{
    public class DrugDirectory
    {
        public int Id { get; set; }
        public string DrugName { get; set; } = null!;

    }
}
