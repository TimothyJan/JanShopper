using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO?> GetProductByIdAsync(int id);
        Task<ProductDTO> CreateProductAsync(ProductDTO productDTO);
        Task<bool> UpdateProductAsync(ProductDTO productDTO);
        Task<bool> DeleteProductAsync(int id);
    }
}
