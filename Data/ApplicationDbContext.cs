using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookStore.Models;

namespace OnlineBookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>().HasData(
                new Book { Id = 101, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 15.99m, Stock = 45 },
                new Book { Id = 102, Title = "1984", Author = "George Orwell", Price = 12.50m, Stock = 12 },
                new Book { Id = 103, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 14.00m, Stock = 30 },
                new Book { Id = 104, Title = "Moby Dick", Author = "Herman Melville", Price = 11.20m, Stock = 20 }
            );
        }
    }
}