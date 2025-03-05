using JanShopper.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace JanShopper.Server.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly JanShopperDbContext _context;

        public OrderRepository(JanShopperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status
                })
                .ToListAsync();
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    UserId = o.UserId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status
                })
                .FirstOrDefaultAsync();

            return order;
        }

        public async Task<OrderDTO> CreateOrderAsync(OrderDTO orderDTO)
        {
            var order = new Order
            {
                UserId = orderDTO.UserId,
                OrderDate = orderDTO.OrderDate,
                TotalAmount = orderDTO.TotalAmount,
                Status = orderDTO.Status
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            orderDTO.Id = order.Id; // Set the generated ID
            return orderDTO;
        }

        public async Task<bool> UpdateOrderAsync(OrderDTO orderDTO)
        {
            var order = await _context.Orders.FindAsync(orderDTO.Id);
            if (order == null)
            {
                return false;
            }

            order.UserId = orderDTO.UserId;
            order.OrderDate = orderDTO.OrderDate;
            order.TotalAmount = orderDTO.TotalAmount;
            order.Status = orderDTO.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}