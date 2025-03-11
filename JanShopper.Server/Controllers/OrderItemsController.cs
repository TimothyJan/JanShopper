using JanShopper.Server.Models;
using JanShopper.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JanShopper.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IOrderItemsRepository _orderItemsRepository;

        public OrderItemsController(IOrderItemsRepository orderItemsRepository)
        {
            _orderItemsRepository = orderItemsRepository;
        }

        // GET: api/OrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItemsDTO>>> GetAllOrderItems()
        {
            var orderItems = await _orderItemsRepository.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        // GET: api/OrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItemsDTO>> GetOrderItemById(int id)
        {
            var orderItem = await _orderItemsRepository.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        // POST: api/OrderItems
        [HttpPost]
        public async Task<ActionResult<OrderItemsDTO>> CreateOrderItem(OrderItemsDTO orderItemDTO)
        {
            var createdOrderItem = await _orderItemsRepository.CreateOrderItemAsync(orderItemDTO);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = createdOrderItem.Id }, createdOrderItem);
        }

        // PUT: api/OrderItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItemsDTO orderItemDTO)
        {
            if (id != orderItemDTO.Id)
            {
                return BadRequest("ID in the URL does not match the ID in the request body.");
            }

            var result = await _orderItemsRepository.UpdateOrderItemAsync(orderItemDTO);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/OrderItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemsRepository.DeleteOrderItemAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}