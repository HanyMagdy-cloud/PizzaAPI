using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation property: list of dishes in this category
        public ICollection<Dish>? Dishes { get; set; }
    }
}
