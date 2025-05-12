using PizzaAPI.Entities;

namespace PizzaAPI.Interfaces
{
    public interface IIngredientRepository
    {
        Task<List<Ingredient>> GetAllAsync();
        Task<Ingredient?> GetByIdAsync(int id);
        Task AddIngredientAsync(Ingredient ingredient);
        Task<bool> SaveChangesAsync();
    }
}
