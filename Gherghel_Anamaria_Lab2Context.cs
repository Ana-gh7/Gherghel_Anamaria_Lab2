using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gherghel_Anamaria_Lab2.Data;

namespace Gherghel_Anamaria_Lab2
{
    public class Gherghel_Anamaria_Lab2Context(
        DbContextOptions<Gherghel_Anamaria_Lab2Context> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Gherghel_Anamaria_Lab2.Models.Category> Category { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Publisher> Publisher { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Authors> Authors { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Member> Member { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}