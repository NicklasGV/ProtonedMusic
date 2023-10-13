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



        [HttpPost]
        public async Task<IActionResult> CreateImage([FromForm] ImageRequest imageRequest, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".PNG", ".GIF", }; // Definer de tilladte filtyper
                var fileExtension = Path.GetExtension(imageFile.FileName);

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file type. Allowed file types: jpg, jpeg, png, gif");
                }

                var uploadedImage = await _imageService.UploadImage(imageRequest, imageFile);

                return Ok(uploadedImage);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
