using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpcomingController : ControllerBase
    {
        private readonly IUpcomingService _upcomingService;
        public UpcomingController(IUpcomingService upcomingService)
        {
            _upcomingService = upcomingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<UpcomingResponse> upcomings = await _upcomingService.GetAllUpcomings();
                
                if (upcomings.Count == 0)
                {
                    return NoContent();
                }
                return Ok(upcomings);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUpcoming([FromBody] UpcomingRequest newUpcoming)
        {
            try
            {
                UpcomingResponse upcomingResponse = await _upcomingService.CreateUpcoming(newUpcoming);

                if (upcomingResponse == null)
                {
                    return Problem("Is null");
                }
                return Ok(upcomingResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{upcomingId}")]
        public async Task<IActionResult> FindEventById([FromRoute] int upcomingId)
        {
            try
            {
                var upcomingRespone = await _upcomingService.FindByUpcomingId(upcomingId);

                if (upcomingRespone == null)
                {
                    return NotFound();
                }
                return Ok(upcomingRespone);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{upcomingId}")]
        public async Task<IActionResult> UpdateUpcomingById([FromRoute] int upcomingId, [FromBody] UpcomingRequest updateUpcoming)
        {
            try
            {
                var upcomingRespone = await _upcomingService.UpdateUpcomingById(upcomingId, updateUpcoming);

                if (upcomingRespone == null)
                {
                    return NotFound();
                }
                return Ok(upcomingRespone);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{upcomingId}")]
        public async Task<IActionResult> DeleteUpcomingById([FromRoute] int upcomingId)
        {
            try
            {
                var upcomingResponse = await _upcomingService.DeleteUpcomingById(upcomingId);

                if (upcomingResponse == null)
                {
                    return NotFound();
                }
                return Ok(upcomingResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
