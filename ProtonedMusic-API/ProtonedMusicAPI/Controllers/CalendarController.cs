using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using ProtonedMusicAPI.DTO.CalendarDTO;
using ProtonedMusicAPI.Interfaces.ICalendar;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CalendarResponse> contents = await _calendarService.GetAllAsync();

                if (contents.Count == 0)
                {
                    return NoContent();
                }
                return Ok(contents);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContent([FromForm] CalendarRequest newContent)
        {
            try
            {
                CalendarResponse response = await _calendarService.CreateAsync(newContent);

                if (response == null)
                {
                    return Problem("Is Null");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{calendarId}")]
        public async Task<IActionResult> FindCalendarById([FromRoute] int calendarId)
        {
            try
            {
                var response = await _calendarService.FindByIdAsync(calendarId);

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

        [HttpPut]
        [Route("{calendarId}")]
        public async Task<IActionResult> UpdateCalendarById([FromRoute] int calendarId, [FromBody] CalendarRequest updateContent)
        {
            try
            {
                var response = await _calendarService.UpdateByIdAsync(calendarId, updateContent);

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

        [HttpDelete]
        [Route("{calendarId}")]
        public async Task<IActionResult> DeleteCalendarById([FromRoute] int calendarId)
        {
            try
            {
                var response = await _calendarService.DeleteByIdAsync(calendarId);

                if(response == null)
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
    }
}
