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
        }

        public DbSet<User> Users { get; set; }
    }
}
