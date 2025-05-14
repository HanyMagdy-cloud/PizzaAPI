using PizzaAPI.Data.Entities;

namespace PizzaAPI.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task AddOrderAsync(Order order);
        Task<bool> SaveChangesAsync();

        Task<Order?> GetByIdAsync(int orderId);
        Task<bool> DeleteAsync(Order order);
        Task CreateAsync(Order order);
    }
}
