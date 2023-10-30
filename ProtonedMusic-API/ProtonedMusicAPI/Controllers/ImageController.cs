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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetImageById(int id)
        {
            var img = await _imageService.GetImageById(id);

            if (img is null)
            {
                return NotFound($"No picture found with ID = {id}");
            }

            return Ok(img);
        }

        [HttpPost]
        public async Task<IActionResult> AddPicture()
        {
            var file = Request.Form.Files[0];

            if (file is null)
            {
                return BadRequest("Something done gone wrong, I tell you hwat");
            }
            var img = new Image();
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);

                if (ms.Length < 2097152)
                {
                    //img.ImageData = ms.ToArray();
                    img.ImageName = file.FileName;
                }
                else
                {
                    return BadRequest("This picture is too dang big, make sure it's under 2MB in size");
                }
            }
            await _imageService.AddImage(img);
            return Ok(img);
        }

    }

}

