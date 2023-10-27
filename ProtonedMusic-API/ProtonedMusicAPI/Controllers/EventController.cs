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
        public async Task<IActionResult> GetAll()
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

        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] EventRequest newEvent)
        {
            try
            {
                EventResponse eventResponse = await _eventService.CreateEvent(newEvent);

                if(eventResponse == null)
                {
                    return Problem("Is null");
                }
                return Ok(eventResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{eventId}")]
        public async Task<IActionResult> FindEventById([FromRoute] int eventId)
        {
            try
            {
                var eventResponse = await _eventService.FindByEventId(eventId);

                if(eventResponse == null)
                {
                    return NotFound();
                }
                return Ok(eventResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{eventId}")]
        public async Task<IActionResult> UpdateEventById([FromRoute] int eventId, [FromBody] EventRequest updateEvent)
        {
            try
            {
                var eventResponse = await _eventService.UpdateEventById(eventId, updateEvent);

                if( eventResponse == null)
                {
                    return NotFound();
                }
                return Ok(eventResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{eventId}")]
        public async Task<IActionResult> DeleteEventById([FromRoute] int eventId)
        {
            try
            {
                var eventResponse = await _eventService.DeleteEventById(eventId);

                if (eventResponse == null)
                {
                    return NotFound();
                }
                return Ok(eventResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
