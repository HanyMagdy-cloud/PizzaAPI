using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
