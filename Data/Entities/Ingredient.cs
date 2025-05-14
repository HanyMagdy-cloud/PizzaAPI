using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        // Navigation property: list of dishes that use this ingredient (many-to-many)
        public ICollection<Dish>? Dishes { get; set; }
    }
}
