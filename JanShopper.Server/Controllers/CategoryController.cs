using Microsoft.AspNetCore.Mvc;
using JanShopper.Server.Models;
using JanShopper.Server.Repositories;

namespace JanShopper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound(); // 404 Not Found
            }

            return Ok(category); // 200 OK with category data
        }

        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request with validation errors
            }

            var createdCategory = await _categoryRepository.CreateCategoryAsync(categoryDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.Id }, createdCategory); // 201 Created
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest("ID mismatch"); // 400 Bad Request
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request with validation errors
            }

            var result = await _categoryRepository.UpdateCategoryAsync(categoryDTO);

            if (!result)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategoryAsync(id);

            if (!result)
            {
                return NotFound(); // 404 Not Found
            }

            return NoContent(); // 204 No Content
        }
    }
}