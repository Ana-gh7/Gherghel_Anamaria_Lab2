
using System.ComponentModel.DataAnnotations;

namespace Gherghel_Anamaria_Lab2.Models
{
    public class Member
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$",
            ErrorMessage = "Prenumele trebuie sa inceapa cu litera mare si sa contina doar litere mici sau spatii.")]
        [StringLength(30, MinimumLength = 3)]
        public string? FirstName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-z\s]*$",
            ErrorMessage = "Numele trebuie sa inceapa cu litera mare si sa contina doar litere mici sau spatii.")]
        [StringLength(30, MinimumLength = 3)]
        public string? LastName { get; set; }

        [StringLength(70)]
        public string? Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^0[0-9]{3}[-. ]?[0-9]{3}[-. ]?[0-9]{3}$",
            ErrorMessage = "Telefonul trebuie sa inceapa cu 0 si sa fie de forma 0722-123-123, 0722.123.123 sau 0722 123 123")]
        public string? Phone { get; set; }

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public ICollection<Borrowing>? Borrowings { get; set; }
    }
}
