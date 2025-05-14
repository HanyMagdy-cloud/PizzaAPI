namespace PizzaAPI.Data.DTOs
{
    public class OrderItemReadDto
    {
        public string DishName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal PriceAtOrderTime { get; set; }
    }
}
