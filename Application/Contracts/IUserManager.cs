using Domain.Entities;


namespace Application.Contracts
{
    public interface  IUserManager
    {
        public Task<User> GetUserByEmailAsync(string Email);
        public Task<User> InsertUserAsync(User user);
        public bool CheckPasswordAsync(string password, string hashedPassword);
        public string HashPasswordAsync(string password);
    }
}
