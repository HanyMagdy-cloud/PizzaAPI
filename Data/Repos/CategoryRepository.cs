using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Data.Entities;
using PizzaAPI.Data.Interfaces;

namespace PizzaAPI.Data.Repos
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        // Inject the database context into the repository
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieve all categories
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Dishes) // Optional: include related dishes
                .ToListAsync();
        }

        // Retrieve a single category by ID
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Dishes) // Optional
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // Add a new category
        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        // Save changes to the database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
