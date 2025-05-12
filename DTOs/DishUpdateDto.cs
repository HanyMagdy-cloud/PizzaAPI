using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.DTOs
{
    public class DishUpdateDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";

        public string CategoryName { get; set; } = "";
        public List<string> IngredientNames { get; set; } = new();
    }
}
