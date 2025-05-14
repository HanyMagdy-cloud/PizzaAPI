using Microsoft.EntityFrameworkCore;
using PizzaAPI.Data.Entities;
using System.Collections.Generic;

namespace PizzaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }

        // Add other DbSet<T> here as you create more entities


        // DbSet for Orders
        public DbSet<Order> Orders { get; set; }

        // DbSet for OrderItems (join between Orders and Dishes)
        public DbSet<OrderItem> OrderItems { get; set; }

        // DbSet for Dishes
        public DbSet<Dish> Dishes { get; set; }

        // DbSet for Categories
        public DbSet<Category> Categories { get; set; }

        // DbSet for Ingredients
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
