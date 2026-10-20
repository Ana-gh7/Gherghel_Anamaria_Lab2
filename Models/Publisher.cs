
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gherghel_Anamaria_Lab2.Models
{
    public class Publisher
    {
        public int ID { get; set; }

        [Required]
        public string PublisherName { get; set; } = string.Empty;

        public ICollection<Book>? Books { get; set; }
    }
}
