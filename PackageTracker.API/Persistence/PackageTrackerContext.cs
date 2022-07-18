using Microsoft.EntityFrameworkCore;
using PackageTracker.API.Entities;

namespace PackageTracker.API.Persistence
{
    public class PackageTrackerContext : DbContext
    {
        public PackageTrackerContext(DbContextOptions<PackageTrackerContext> options)
        : base(options)
        {
            // Packages = new List<Package>(); 1
        }
        void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Package>(e => {
                e.HasKey(p => p.Id);
                e
                .HasMany(p => p.Updates)
                .WithOne()
                .HasForeignKey(pu => pu.PackageId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<PackageUpdate>(e => {
                e.HasKey(p => p.Id);
            });
        }
        // public List<Package> Packages { get; set; } 1
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageUpdate> Updates {get; set;}
    }
}