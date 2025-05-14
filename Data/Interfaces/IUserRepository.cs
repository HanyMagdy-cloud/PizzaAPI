using PizzaAPI.Data.Entities;

namespace PizzaAPI.Data.Interfaces
{
    // Interface for user-related database operations
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<bool> SaveChangesAsync();
    }

}
