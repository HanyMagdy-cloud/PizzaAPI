using PizzaAPI.Data.Entities;

namespace PizzaAPI.Data.Interfaces
{
    public interface IDishRepository
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<Dish?> GetByIdAsync(int id);
        Task AddDishAsync(Dish dish);
        Task UpdateDishAsync(Dish dish);
        Task DeleteDishAsync(Dish dish);
        Task<bool> SaveChangesAsync();
    }
}
