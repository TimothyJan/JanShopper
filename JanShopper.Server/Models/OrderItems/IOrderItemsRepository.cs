using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<IEnumerable<OrderItemsDTO>> GetAllOrderItemsAsync();
        Task<OrderItemsDTO?> GetOrderItemByIdAsync(int id);
        Task<OrderItemsDTO> CreateOrderItemAsync(OrderItemsDTO orderItem);
        Task<bool> UpdateOrderItemAsync(OrderItemsDTO orderItem);
        Task<bool> DeleteOrderItemAsync(int id);
        Task<bool> OrderItemExistsAsync(int id);
    }
}