using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.DTOs;
using PizzaAPI.Entities;
using PizzaAPI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = GetUserIdFromClaims();
            if (!userId.HasValue)
                return Unauthorized("Invalid user ID in token.");

            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId.Value);
            var result = orders.Select(o => new OrderReadDto
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                TotalPrice = o.TotalPrice,
                Items = o.OrderItems?.Select(i => new OrderItemReadDto
                {
                    DishName = i.Dish?.Name ?? "Unknown",
                    Quantity = i.Quantity,
                    PriceAtOrderTime = i.PriceAtOrderTime
                }).ToList() ?? new()
            }).ToList();

            return Ok(new
            {
                data = result,
                statusCode = 200,
                message = "Orders retrieved"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemCreateDto> dto)
        {
            var userId = GetUserIdFromClaims();
            if (!userId.HasValue)
                return Unauthorized("Invalid user ID in token.");

            if (dto == null || !dto.Any())
                return BadRequest("Order must contain at least one item.");

            // Calculate total price
            var totalPrice = dto.Sum(item => item.PriceAtOrderTime * item.Quantity);

            // Create the order entity
            var order = new Order
            {
                UserId = userId.Value,
                CreatedAt = DateTime.UtcNow,
                TotalPrice = totalPrice,
                OrderItems = dto.Select(item => new OrderItem
                {
                    DishId = item.DishId,
                    Quantity = item.Quantity,
                    PriceAtOrderTime = item.PriceAtOrderTime
                }).ToList()
            };

            await _orderRepository.CreateAsync(order);
            var result = await _orderRepository.SaveChangesAsync();

            if (!result)
                return StatusCode(500, "Failed to create order.");

            return Ok(new
            {
                statusCode = 200,
                message = "Order created successfully.",
                orderId = order.Id
            });
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMyOrder(int id)
        {
            var userId = GetUserIdFromClaims();
            if (!userId.HasValue)
                return Unauthorized("Invalid user ID in token.");

            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound("Order not found");

            if (order.UserId != userId.Value)
                return Forbid("You are not allowed to delete this order");

            var success = await _orderRepository.DeleteAsync(order);
            if (!success)
                return StatusCode(500, "Failed to delete order");

            return Ok(new
            {
                statusCode = 200,
                message = $"Order with ID {id} has been deleted"
            });
        }





        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                              User.FindFirst(JwtRegisteredClaimNames.Sub);

            return int.TryParse(userIdClaim?.Value, out var userId) ? userId : null;
        }
    }
}
