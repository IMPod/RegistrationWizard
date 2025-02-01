using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL.Models;

namespace RegistrationWizard.DAL;

public class RegistrationContext : DbContext
{
    public RegistrationContext(DbContextOptions<RegistrationContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Province> Provinces => Set<Province>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Country>().HasData(new Country { Id = 1, Name = "Country A" });
        modelBuilder.Entity<Province>().HasData(
            new Province { Id = 1, Name = "Province A1", CountryId = 1 },
            new Province { Id = 2, Name = "Province A2", CountryId = 1 }
        );

        modelBuilder.Entity<Country>().HasData(new Country { Id = 2, Name = "Country B" });
        modelBuilder.Entity<Province>().HasData(
            new Province { Id = 3, Name = "Province B1", CountryId = 2 },
            new Province { Id = 4, Name = "Province B2", CountryId = 2 }
        );
    }
}

