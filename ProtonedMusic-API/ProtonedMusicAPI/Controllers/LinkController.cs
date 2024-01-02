using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Hosting.Internal;
using ProtonedMusicAPI.Interfaces.IArtist;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly ILinkRepository _linkRepository;


        public LinkController(ILinkService linkService, ILinkRepository linkRepository)
        {
            _linkService = linkService;
            _linkRepository = linkRepository;
        }

        [HttpGet]
        [Route("{linkId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int linkId)
        {
            try
            {
                LinkResponse linkResponse = await _linkService.FindByIdAsync(linkId);

                if (linkResponse == null)
                {
                    return NotFound();
                }
                return Ok(linkResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{linkId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int linkId, [FromForm] LinkRequest updateLink)
        {
            try
            {
                var linkResponse = await _linkService.UpdateByIdAsync(linkId, updateLink);

                if (linkResponse == null)
                {
                    return NotFound();
                }

                return Ok(linkResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{linkId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int linkId)
        {
            try
            {
                var linkResponse = await _linkService.DeleteByIdAsync(linkId);
                if (linkResponse == null)
                {
                    return NotFound();
                }
                return Ok(linkResponse);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<LinkResponse> links = await _linkService.GetAllAsync();

                if (links == null)
                {
                    return Problem("A problem occured the team is fixing it as we speak");
                }

                if (links.Count == 0)
                {
                    return NoContent();
                }
                return Ok(links);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] LinkRequest newLink)
        {
            try
            {
                LinkResponse linkResponse = await _linkService.CreateAsync(newLink);

                return Ok(linkResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
