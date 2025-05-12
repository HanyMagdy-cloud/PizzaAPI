namespace PizzaAPI.DTOs
{
    public class DishReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Description { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public List<string>? IngredientNames { get; set; }


    }
}
