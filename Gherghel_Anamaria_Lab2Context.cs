
using Microsoft.EntityFrameworkCore;

namespace Gherghel_Anamaria_Lab2
{
    public class Gherghel_Anamaria_Lab2Context(
        DbContextOptions<Gherghel_Anamaria_Lab2Context> options)
        : DbContext(options)
    {
        public DbSet<Gherghel_Anamaria_Lab2.Models.Book> Book { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Publisher> Publisher { get; set; } = default!;

        public DbSet<Gherghel_Anamaria_Lab2.Models.Authors> Authors { get; set; } = default!;
    }
}
