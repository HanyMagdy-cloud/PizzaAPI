using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.DTOs
{
    public class IngredientCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
