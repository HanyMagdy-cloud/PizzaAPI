using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;

namespace PizzaAPI.Repos
{
    public class DishRepository : IDishRepository
    {
        private readonly ApplicationDbContext _context;

        // Inject the database context into the repository
        public DishRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all dishes including their categories and ingredients
        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            return await _context.Dishes
                .Include(d => d.Category)
                .Include(d => d.Ingredients)
                .ToListAsync();
        }

        // Get a single dish by its ID
        public async Task<Dish?> GetByIdAsync(int id)
        {
            return await _context.Dishes
        .Include(d => d.Category)
        .Include(d => d.Ingredients)
        .FirstOrDefaultAsync(d => d.Id == id);
        }

        // Add a new dish
        public async Task AddDishAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
        }

        // Update an existing dish
        public Task UpdateDishAsync(Dish dish)
        {
            // EF Core tracks the object, so we just mark it as modified
            _context.Dishes.Update(dish);
            return Task.CompletedTask;
        }

        // Delete a dish
        public Task DeleteDishAsync(Dish dish)
        {
            _context.Dishes.Remove(dish);
            return Task.CompletedTask;
        }

        // Commit changes to the database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
