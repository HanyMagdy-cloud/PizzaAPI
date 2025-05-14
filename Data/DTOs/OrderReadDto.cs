namespace PizzaAPI.Data.DTOs
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemReadDto> Items { get; set; } = new();
    }
}
