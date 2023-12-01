using Microsoft.Extensions.Hosting.Internal;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontpagePostController : ControllerBase
    {
        private readonly IFrontpagePostService _frontpagePostService;
        private readonly IFrontpagePostRepository _frontpagePostRepository;
       

        public FrontpagePostController(IFrontpagePostService frontpagePostService, IFrontpagePostRepository frontpagePostRepository)
        {
            _frontpagePostService = frontpagePostService;
            _frontpagePostRepository = frontpagePostRepository;
        }

        [HttpGet]
        [Route("{frontpageId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int frontpageId)
        {
            try
            {
                FrontpagePostResponse frontpageResponse = await _frontpagePostService.FindByIdAsync(frontpageId);

                if (frontpageResponse == null)
                {
                    return NotFound();
                }
                return Ok(frontpageResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{frontpageId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int frontpageId, [FromForm] FrontpagePostRequest updateFrontpage)
        {
            try
            {
                var frontpageResponse = await _frontpagePostService.UpdateByIdAsync(frontpageId, updateFrontpage);

                if (updateFrontpage.PictureFile != null)
                {
                    FrontpagePostResponse frontpagePictureResponse = await _frontpagePostService.UploadFrontpagePicture(frontpageResponse.Id, updateFrontpage.PictureFile);

                    if (frontpagePictureResponse != null) { frontpageResponse = frontpagePictureResponse; }
                }

                if (frontpageResponse == null)
                {
                    return NotFound();
                }

                return Ok(frontpageResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{frontpageId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int frontpageId)
        {
            try
            {
                var frontpageResponse = await _frontpagePostService.DeleteByIdAsync(frontpageId);
                if (frontpageResponse == null)
                {
                    return NotFound();
                }
                return Ok(frontpageResponse);
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
                List<FrontpagePostResponse> frontpages = await _frontpagePostService.GetAllAsync();

                if (frontpages == null)
                {
                    return Problem("A problem occured the team is fixing it as we speak");
                }

                if (frontpages.Count == 0)
                {
                    return NoContent();
                }
                return Ok(frontpages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromForm] FrontpagePostRequest newFrontPage)
        {
            try
            {
                FrontpagePostResponse frontpageResponse = await _frontpagePostService.CreateAsync(newFrontPage);
                if (newFrontPage.PictureFile != null)
                {
                    FrontpagePostResponse frontpagePictureResponse = await _frontpagePostService.UploadFrontpagePicture(frontpageResponse.Id, newFrontPage.PictureFile);

                    if (frontpagePictureResponse != null) { frontpageResponse = frontpagePictureResponse; }
                }
                return Ok(frontpageResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("upload-frontpage-picture/{frontpageId}")]
        public async Task<IActionResult> UploadProfilePicture([FromRoute] int frontpageId, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            if (file != null)
            {
                FrontpagePostResponse frontpage = await _frontpagePostService.UploadFrontpagePicture(frontpageId, file);

                if (frontpage != null)
                {
                    return Ok(frontpage.FrontpagePicturePath);
                }
                
            }

            return BadRequest("No file was uploaded.");
        }
    }
}
