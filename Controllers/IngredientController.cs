using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.DTOs;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;

namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {

        private readonly IIngredientRepository _ingredientRepository;

        // Inject the IngredientRepository
        public IngredientController(IIngredientRepository ingredientRepository)
        {
            _ingredientRepository = ingredientRepository;
        }

        // GET: api/ingredient
        // Get all ingredients (optionally with dishes)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingredients = await _ingredientRepository.GetAllAsync();
            return Ok(ingredients);
        }

        // GET: api/ingredient/5
        // Get a single ingredient by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ingredient = await _ingredientRepository.GetByIdAsync(id);
            if (ingredient == null)
                return NotFound("Ingredient not found.");

            return Ok(ingredient);
        }

        // POST: api/ingredient
        // Add a new ingredient
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IngredientCreateDto dto)
        {
            var ingredient = new Ingredient
            {
                Name = dto.Name
            };

            await _ingredientRepository.AddIngredientAsync(ingredient);
            var result = await _ingredientRepository.SaveChangesAsync();

            if (!result)
                return StatusCode(500, "Failed to create ingredient.");

            return Ok("Ingredient created successfully.");
        }

    }
}
