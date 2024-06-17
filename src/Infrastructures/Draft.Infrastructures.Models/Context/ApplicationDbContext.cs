using Draft.Infrastructures.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Draft.Infrastructures.Models.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Account> Account { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingInternal(modelBuilder);
    }

    internal static void OnModelCreatingInternal(ModelBuilder modelBuilder)
    {

    }
}
