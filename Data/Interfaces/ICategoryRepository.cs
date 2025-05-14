using PizzaAPI.Data.Entities;

namespace PizzaAPI.Data.Interfaces
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
