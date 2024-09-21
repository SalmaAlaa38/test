using Microsoft.EntityFrameworkCore;
using Domain.Entities;
namespace Infrastructure.Persistence
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(u =>
            {
                u.OwnsMany(u => u.RefreshTokens);
                u.HasIndex(u => u.Email).IsUnique();
            });

        }

        public DbSet<User> Users { get; set; }
        public DbSet<VerificationToken> verificationTokens { get; set; }
    }
}
