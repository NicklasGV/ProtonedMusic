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
        public async Task<IActionResult> FindByIdAsync([FromRoute] int customerId)
        {
            try
            {
                var response = await _orderHistoryService.FindByIdAsync(customerId);

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

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderHistoryRequest newOrder)
        {
            try
            {
                OrderHistoryResponse response = await _orderHistoryService.CreateOrderAsync(newOrder);

                if (response == null)
                {
                    return Problem("Is null");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}