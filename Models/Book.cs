
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gherghel_Anamaria_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Titlul cartii este obligatoriu")]
        [StringLength(150, MinimumLength = 3,
            ErrorMessage = "Titlul trebuie sa aiba intre 3 si 150 de caractere")]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 500)]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        // Legatura cu autorul
        public int AuthorsID { get; set; }

        public Authors? Authors { get; set; }

        // Legatura cu editura
        public int? PublisherID { get; set; }

        public Publisher? Publisher { get; set; }

        public ICollection<BookCategory> BookCategories { get; set; }
            = new List<BookCategory>();

        public ICollection<Borrowing> Borrowings { get; set; }
            = new List<Borrowing>();
    }
}
