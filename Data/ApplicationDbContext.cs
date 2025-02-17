using BasicWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BasicWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "Aspekt" },
                new Company { CompanyId = 2, CompanyName = "Axapta Masters" },
                new Company { CompanyId = 3, CompanyName = "Codeit Solution" });
            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 123, CountryName = "North Macedonia" },
                new Country { CountryId = 234, CountryName = "Serbia" },
                new Country { CountryId = 345, CountryName = "France" });
            modelBuilder.Entity<Contact>().HasData(
                new Contact { ContactId = 1, ContactName = "Klementina", CompanyId = 1, CountryId = 123 },
                new Contact { ContactId = 2, ContactName = "Kirila", CompanyId = 2, CountryId = 234 },
                new Contact { ContactId = 3, ContactName = "Petar", CompanyId = 3, CountryId = 345 }
                );
        }

    }
}
