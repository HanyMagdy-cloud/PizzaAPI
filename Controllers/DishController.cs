using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.DTOs;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;
using PizzaAPI.Repos;

namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {

        private readonly IDishRepository _dishRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;

        // Inject the DishRepository
        public DishController(IDishRepository dishRepository, ICategoryRepository categoryRepository,
            IIngredientRepository ingredientRepository)

        {
            _dishRepository = dishRepository;
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/dish
        // Returns all dishes with their categories and ingredients
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dishes = await _dishRepository.GetAllAsync();

            var result = dishes.Select(d => new DishReadDto
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                Description = d.Description,
                CategoryName = d.Category?.Name ?? "Unknown",
                IngredientNames = d.Ingredients?.Select(i => i.Name).ToList()
            }).ToList();

            return Ok(result);
        }




        // POST: api/dish
        // Creates a new dish
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DishCreateDto dto)
        {
            // Get category by name
            var category = await _categoryRepository.GetByNameAsync(dto.CategoryName);
            if (category == null)
                return BadRequest($"Category '{dto.CategoryName}' does not exist.");

            // Get ingredient entities from names
            var allIngredients = await _ingredientRepository.GetAllAsync();
            var ingredients = allIngredients
                .Where(i => dto.IngredientNames.Select(n => n.Trim().ToLower()).Contains(i.Name.Trim().ToLower()))
                .ToList();

            if (ingredients.Count != dto.IngredientNames.Count)
                return BadRequest("Some ingredients were not found in the database.");

            var dish = new Dish
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = category.Id,
                Ingredients = ingredients
            };

            await _dishRepository.AddDishAsync(dish);
            var success = await _dishRepository.SaveChangesAsync();

            if (!success)
                return StatusCode(500, "Failed to create dish.");

            return Ok("Dish created successfully.");
        }



        // PUT: api/dish/
        // Updates an existing dish
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DishUpdateDto dto)
        {
            var dish = await _dishRepository.GetByIdAsync(id);
            if (dish == null)
                return NotFound("Dish not found.");

            // Find the category by name
            var category = await _categoryRepository.GetByNameAsync(dto.CategoryName);
            if (category == null)
                return BadRequest($"Category '{dto.CategoryName}' not found.");

            // Find ingredients by name
            var allIngredients = await _ingredientRepository.GetAllAsync();
            var matchingIngredients = allIngredients
                .Where(i => dto.IngredientNames.Contains(i.Name))
                .ToList();

            if (matchingIngredients.Count != dto.IngredientNames.Count)
                return BadRequest("Some ingredients were not found in the database.");

            // Update dish details
            dish.Name = dto.Name;
            dish.Price = dto.Price;
            dish.Description = dto.Description;
            dish.CategoryId = category.Id;

            // Replace the ingredients collection (EF Core will update the many-to-many relationship table)
            dish.Ingredients = matchingIngredients;

            await _dishRepository.UpdateDishAsync(dish);
            var result = await _dishRepository.SaveChangesAsync();

            if (!result)
                return StatusCode(500, "Failed to update dish.");

            return Ok("Dish updated successfully.");
        }



       
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dish = await _dishRepository.GetByIdAsync(id);
            if (dish == null)
                return NotFound("Dish not found.");

            await _dishRepository.DeleteDishAsync(dish);
            var result = await _dishRepository.SaveChangesAsync();

            if (!result)
                return StatusCode(500, "Failed to delete dish.");

            return Ok("Dish deleted successfully.");
        }
    }
}
