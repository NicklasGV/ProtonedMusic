using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProtonedMusicAPI.DTO.IOrderHistoryDTO;
using ProtonedMusicAPI.Interfaces.IOrderHistory;

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
        public async Task<ActionResult<OrderHistoryResponse>> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                OrderHistoryResponse response = await _orderHistoryService.GetOrdersByCustomerIdAsync(customerId);

                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromForm] OrderHistoryRequest newOrder)
        {
            try
            {
                OrderHistoryResponse response = await _orderHistoryService.CreateOrderAsync(newOrder);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
