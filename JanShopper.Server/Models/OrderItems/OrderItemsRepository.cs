using JanShopper.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace JanShopper.Server.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly JanShopperDbContext _context;

        public OrderItemsRepository(JanShopperDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItemsDTO>> GetAllOrderItemsAsync()
        {
            var orderItems = await _context.OrderItems
                                           .Include(oi => oi.Order)
                                           .Include(oi => oi.Product)
                                           .ToListAsync();

            return orderItems.Select(oi => new OrderItemsDTO
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Price = oi.Price
            }).ToList();
        }

        public async Task<OrderItemsDTO?> GetOrderItemByIdAsync(int id)
        {
            var orderItem = await _context.OrderItems
                                          .Include(oi => oi.Order)
                                          .Include(oi => oi.Product)
                                          .FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItem == null)
            {
                return null;
            }

            return new OrderItemsDTO
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price
            };
        }

        public async Task<OrderItemsDTO> CreateOrderItemAsync(OrderItemsDTO orderItemDTO)
        {
            var orderItem = new OrderItems
            {
                OrderId = orderItemDTO.OrderId,
                ProductId = orderItemDTO.ProductId,
                Quantity = orderItemDTO.Quantity,
                Price = orderItemDTO.Price
            };

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            orderItemDTO.Id = orderItem.Id; // Set the generated ID
            return orderItemDTO;
        }

        public async Task<bool> UpdateOrderItemAsync(OrderItemsDTO orderItemDTO)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemDTO.Id);
            if (orderItem == null)
            {
                return false;
            }

            orderItem.OrderId = orderItemDTO.OrderId;
            orderItem.ProductId = orderItemDTO.ProductId;
            orderItem.Quantity = orderItemDTO.Quantity;
            orderItem.Price = orderItemDTO.Price;

            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return false;
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> OrderItemExistsAsync(int id)
        {
            return await _context.OrderItems.AnyAsync(oi => oi.Id == id);
        }
    }
}