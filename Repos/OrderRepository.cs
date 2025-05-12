using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;

namespace PizzaAPI.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        // Inject the ApplicationDbContext via constructor
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all orders placed by a specific user (includes OrderItems and Dishes)
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Dish)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        // Get a single order by its ID (with related data)
        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Dish)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        // Add a new order to the context (not saved yet)
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        // Save all changes to the database
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<bool> DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            return await SaveChangesAsync();
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

    }


}
