using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;

namespace PizzaAPI.Repos
{
    public class UserRepository : IUserRepository
    {
        // Injects the database context (for accessing the database)
        private readonly ApplicationDbContext _context;

        // Constructor injection to provide the DbContext instance
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves a user from the database by username, or returns null if not found
        public async Task<User?> GetByUsernameAsync(string username)
        {
            // Searches the Users table for the first match with the given username
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        // Adds a new user to the Users table asynchronously (but does not save yet)
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        // Saves all changes made in the context to the database
        // Returns true if at least one row was affected (i.e., data saved)
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

    }
}
