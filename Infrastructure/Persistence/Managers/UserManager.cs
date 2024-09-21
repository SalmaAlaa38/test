using Application.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Managers
{
    public sealed class UserManager
        (
        ApplicationDbContext context
        ) : IUserManager
    {
        public bool CheckPasswordAsync(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<User?> GetUserByEmailAsync(string Email)
        {
            return await context.Users
                .FirstOrDefaultAsync(x => x.Email == Email);
        }

        public string HashPasswordAsync(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<User> InsertUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }
        public async Task<User?> GetUserByIdAsync(string Id)
        {
            return await context.Users.FindAsync(Id);
        }
        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
