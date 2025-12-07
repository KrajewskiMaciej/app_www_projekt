using Microsoft.EntityFrameworkCore;

namespace Weterynaria.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Animals> Animals { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<ServicesTypes> ServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServicesTypes>().HasData(
                new ServicesTypes { Id = 1, Name = "Konsultacja ogólna", Price = 100 },
                new ServicesTypes { Id = 2, Name = "Szczepienie", Price = 50 },
                new ServicesTypes { Id = 3, Name = "USG", Price = 150 }
            );
        }
    }
}