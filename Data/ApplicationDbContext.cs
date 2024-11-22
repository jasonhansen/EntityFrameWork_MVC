using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models;

namespace MyWebApplication.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // adding configurations : whats options will be passed on DBContext

        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //default entity framework
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Play", DisplayOrder = 1 },
                new Category { CategoryId = 2, Name = "SciFi", DisplayOrder = 5 },
                new Category { CategoryId = 3, Name = "Romantic", DisplayOrder = 6 }
            );
        }
    }
}
