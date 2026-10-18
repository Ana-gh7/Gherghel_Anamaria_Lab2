using Microsoft.EntityFrameworkCore;

public class Gherghel_Anamaria_Lab2Context(DbContextOptions<Gherghel_Anamaria_Lab2Context> options) : DbContext(options)
{
    public DbSet<Gherghel_Anamaria_Lab2.Models.Book> Book { get; set; } = default!;
}
