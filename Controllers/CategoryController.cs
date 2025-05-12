using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.DTOs;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;

namespace PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;

        // Inject the CategoryRepository
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/category
        // Get all categories (optionally includes dishes)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var result = categories.Select(c => new CategoryReadDto
            {
                Id = c.Id,
                Name = c.Name,
                DishNames = c.Dishes?.Select(d => d.Name).ToList()
            }).ToList();

            return Ok(result);
        }


        // GET: api/category/5
        // Get a category by its ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return NotFound("Category not found.");

            return Ok(category);
        }

        // POST: api/category
        // Create a new category
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            await _categoryRepository.AddCategoryAsync(category);
            var result = await _categoryRepository.SaveChangesAsync();

            if (!result)
                return StatusCode(500, "Failed to create category.");

            return Ok("Category created successfully.");
        }

    }
}
