using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<EventResponse> events = await _eventService.GetAllEvents();

                if (events.Count == 0)
                {
                    return NoContent();
                }
                return Ok(events);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
