using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        // Foreign key reference to the User who placed the order
        [Required]
        public int UserId { get; set; }

        // Navigation property to access the related User entity
        public User? User { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Total price for the order
        [Required]
        [Column(TypeName = "decimal(10, 2)")] // Define precision for money value
        public decimal TotalPrice { get; set; }

        // Navigation property to hold a collection of OrderItems (not implemented yet)
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
