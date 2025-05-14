using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.DTOs
{
    public class DishCreateDto
    {
        [Required]
        public string Name { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; } = "";

        [Required]
        public string CategoryName { get; set; } = "";

        [Required]
        public List<string> IngredientNames { get; set; } = new();
    }
}
