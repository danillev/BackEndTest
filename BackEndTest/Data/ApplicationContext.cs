using BackEndTest.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace BackEndTest.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<trainsCar>()
                .HasKey(tc => new { tc.traintNumber, tc.carNumber });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Car> cars { get; set; }
        public DbSet<trainsCar> trainsCars { get; set; }
    }
}
