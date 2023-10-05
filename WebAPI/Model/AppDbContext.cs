using Microsoft.EntityFrameworkCore;

namespace WebAPI.Model;

public class AppDbContext : DbContext
{
    public DbSet<Person> persons { get; set; }
    public DbSet<Skills> skills { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

}