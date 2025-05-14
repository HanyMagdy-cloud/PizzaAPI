using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Data.Entities;
using PizzaAPI.Data.Interfaces;

namespace PizzaAPI.Data.Repos
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        // Inject the database context
        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all ingredients
        //public async Task<IEnumerable<Ingredient>> GetAllAsync()
        //{
        //    return await _context.Ingredients
        //        .Include(i => i.Dishes) // Optional: show which dishes use this ingredient
        //        .ToListAsync();
        //}

        // Get a specific ingredient by ID
        public async Task<Ingredient?> GetByIdAsync(int id)
        {
            return await _context.Ingredients
                .Include(i => i.Dishes)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Add a new ingredient
        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            await _context.Ingredients.AddAsync(ingredient);
        }

        // Save all pending changes to the database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }
    }
}
