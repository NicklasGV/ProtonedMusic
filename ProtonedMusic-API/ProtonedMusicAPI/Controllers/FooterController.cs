namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterController : ControllerBase
    {
        private readonly IFooterService _footerService;
        public FooterController(IFooterService footerService)
        {
            _footerService = footerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<FooterResponse> posts = await _footerService.GetAllAsync();

                if (posts.Count == 0)
                {
                    return NoContent();
                }
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FooterRequest newFooter)
        {
            try
            {
                FooterResponse response = await _footerService.CreateAsync(newFooter);

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

        [HttpGet]
        [Route("{footerId}")]
        public async Task<IActionResult> FindById([FromRoute] int footerId)
        {
            try
            {
                var response = await _footerService.FindByIdAsync(footerId);

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
        [Route("{footerId}")]
        public async Task<IActionResult> UpdateById([FromRoute] int footerId, [FromBody] FooterRequest updateFooter)
        {
            try
            {
                var response = await _footerService.UpdateByIdAsync(footerId, updateFooter);

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
        [Route("{footerId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int footerId)
        {
            try
            {
                var response = await _footerService.DeleteByIdAsync(footerId);

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
    }
}
