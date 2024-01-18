using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtonedMusicAPI.DTO.IOrderHistoryDTO;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IOrderHistoryService _orderHistoryService;

        public OrderHistoryController(IOrderHistoryService orderHistoryService)
        {
            _orderHistoryService = orderHistoryService;
        }

        [HttpGet("{customerId}")]
        public async Task<ActionResult<OrderHistoryResponse>> GetOrdersByCustomerId(string customerId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerId))
                {
                    return BadRequest("Invalid customerId");
                }

                var orderHistory = await _orderHistoryService.GetOrdersByCustomerIdAsync(customerId);
                return Ok(orderHistory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderHistoryRequest orderRequest)
        {
            try
            {
                var orderResponse = await _orderHistoryService.CreateOrderAsync(orderRequest);

                return Ok(orderResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}
