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
                // Her kan du eventuelt validere customerId-format eller håndtere det efter behov
                var orderHistory = await _orderHistoryService.GetOrdersByCustomerIdAsync(customerId);
                return Ok(orderHistory);
            }
            catch (Exception ex)
            {
                // Håndter fejl og send en passende fejlmeddelelse
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
