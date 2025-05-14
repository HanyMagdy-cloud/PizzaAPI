using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.Entities
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        // Foreign key to the Category 
        public int CategoryId { get; set; }

        // Navigation property to the category
        public Category? Category { get; set; }

        // Navigation property: list of order items that include this dish
        public ICollection<OrderItem>? OrderItems { get; set; }

        // Navigation property: list of ingredients for the dish (many-to-many)
        public ICollection<Ingredient>? Ingredients { get; set; }
    }
}
