using JanShopper.Server.Models;

namespace JanShopper.Server.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO);
        Task<bool> UpdateOrderAsync(OrderDTO orderDTO);
        Task<bool> DeleteOrderAsync(int id);
    }
}