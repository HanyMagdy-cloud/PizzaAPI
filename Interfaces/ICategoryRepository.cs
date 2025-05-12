using PizzaAPI.Entities;

namespace PizzaAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task<bool> SaveChangesAsync();
        Task<Category?> GetByNameAsync(string name);

    }
}
