
using System.ComponentModel.DataAnnotations;

namespace Gherghel_Anamaria_Lab2.Models
{
    public class Authors
    {
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;
    }
}
