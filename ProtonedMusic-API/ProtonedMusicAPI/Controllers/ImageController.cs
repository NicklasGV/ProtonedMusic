namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateImage([FromForm] ImageRequest imageRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var imageResponse = await _imageService.Create(imageRequest);
                return Ok(imageResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{ImageId}")]
        public async Task<IActionResult> GetImage(Guid ImageId)
        {
            var imageResponse = await _imageService.FindById(ImageId);

            if (imageResponse == null)
            {
                return NotFound(); // Billede blev ikke fundet
            }

            return Ok(imageResponse);
        }

        // Du kan tilføje flere endpoints efter behov, f.eks. til at slette billeder eller hente en liste af billeder.
    }

}

