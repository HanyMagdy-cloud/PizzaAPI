using System.ComponentModel.DataAnnotations;

namespace PizzaAPI.DTOs
{
    public class OrderItemCreateDto
    {
        [Required]
        public int DishId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal PriceAtOrderTime { get; set; }
    }
}
