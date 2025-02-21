using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(int Id);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDTO);
        Task<bool> UpdateCategoryAsync(CategoryDTO categoryDTO);
        Task<bool> DeleteCategoryAsync(int Id);
    }
}