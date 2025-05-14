using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        // Foreign key to the associated Order
        [Required]
        public int OrderId { get; set; }

        // Navigation property to the related Order
        public Order? Order { get; set; }

        // Foreign key to the ordered Dish
        [Required]
        public int DishId { get; set; }

        // Navigation property to the related Dish
        public Dish? Dish { get; set; }

        // Quantity of the dish in this order
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        // Price of the dish at the time of the order
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PriceAtOrderTime { get; set; }
    }
}
