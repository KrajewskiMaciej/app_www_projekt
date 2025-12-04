using Microsoft.EntityFrameworkCore;

namespace Weterynaria.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Animals> Animals { get; set; }
        public DbSet<Visits> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
            });

            modelBuilder.Entity<Animals>()
                .HasOne(a => a.Owner)
                .WithMany(u => u.Animals)
                .HasForeignKey(a => a.OwnerId);

            modelBuilder.Entity<Visits>()
                .HasOne(v => v.Animal)
                .WithMany(a => a.Visits)
                .HasForeignKey(v => v.AnimalId);
        }
    }
}