
namespace Gherghel_Anamaria_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public decimal Price { get; set; }

        // Legatura cu autorul
        public int AuthorsID { get; set; }

        public Authors? Authors { get; set; }

        // Legatura cu editura
        public int? PublisherID { get; set; }

        public Publisher? Publisher { get; set; }
    }
}
