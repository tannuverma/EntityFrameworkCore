using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsEFCore.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {            
        }

        //--seeding data in Db table--
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1, Title = "INR", Description = "INR" },
                new Currency() { Id = 2, Title = "Dollar", Description = "Dollar" },
                new Currency() { Id = 3, Title = "Euro", Description = "Euro" },
                new Currency() { Id = 4, Title = "Dinar", Description = "Dinar" }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { Id = 1, Title = "Hindi", Description = "Hindi" },
                new Language() { Id = 2, Title = "English", Description = "English" },
                new Language() { Id = 3, Title = "Urdu", Description = "Urdu" },
                new Language() { Id = 4, Title = "Punjabi", Description = "Punjabi" }
                );
        }

        //--mapping entities to  their respective tables--
        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookPrice> BookPrices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
