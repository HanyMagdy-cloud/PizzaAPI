namespace PizzaAPI.DTOs
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<string>? DishNames { get; set; }
    }
}
