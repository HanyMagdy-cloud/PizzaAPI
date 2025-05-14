using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.Data.DTOs
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
