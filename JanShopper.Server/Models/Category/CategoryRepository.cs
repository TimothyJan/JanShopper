using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly JanShopperDbContext _context;

        public CategoryRepository(JanShopperDbContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new CategoryDTO 
                { 
                    Id = c.Id, 
                    Name = c.Name 
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            categoryDTO.Id = category.Id;
            return categoryDTO;
        }

        public async Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = await _context.Categories.FindAsync(categoryDTO.Id);
            if (category == null)
            {
                return false;
            }

            category.Name = categoryDTO.Name;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
