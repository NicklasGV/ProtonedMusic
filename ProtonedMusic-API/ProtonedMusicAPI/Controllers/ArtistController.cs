using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Hosting.Internal;
using ProtonedMusicAPI.Interfaces.IArtist;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IArtistRepository _artistRepository;


        public ArtistController(IArtistService artistService, IArtistRepository artistRepository)
        {
            _artistService = artistService;
            _artistRepository = artistRepository;
        }

        [HttpGet]
        [Route("{artistId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int artistId)
        {
            try
            {
                ArtistResponse artistResponse = await _artistService.FindByIdAsync(artistId);

                if (artistResponse == null)
                {
                    return NotFound();
                }
                return Ok(artistResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{artistId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int artistId, [FromForm] ArtistRequest updateArtist)
        {
            try
            {
                var artistResponse = await _artistService.UpdateByIdAsync(artistId, updateArtist);

                if (updateArtist.PictureFile != null)
                {
                    ArtistResponse artistPicture = await _artistService.UploadPicture(artistResponse.Id, updateArtist.PictureFile);

                    if (artistPicture != null)
                    {
                        artistResponse = artistPicture;
                    }

                }

                if (artistResponse == null)
                {
                    return NotFound();
                }

                return Ok(artistResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{artistId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int artistId)
        {
            try
            {
                var artistResponse = await _artistService.DeleteByIdAsync(artistId);
                if (artistResponse == null)
                {
                    return NotFound();
                }
                return Ok(artistResponse);
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
                List<ArtistResponse> artists = await _artistService.GetAllAsync();

                if (artists == null)
                {
                    return Problem("A problem occured the team is fixing it as we speak");
                }

                if (artists.Count == 0)
                {
                    return NoContent();
                }
                return Ok(artists);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateAsync([FromForm] ArtistRequest newArtist)
        {
            try
            {
                ArtistResponse artistResponse = await _artistService.CreateAsync(newArtist);

                if (newArtist.PictureFile != null)
                {
                    ArtistResponse artistPicture = await _artistService.UploadPicture(artistResponse.Id, newArtist.PictureFile);

                    if (artistPicture != null)
                    {
                        artistResponse = artistPicture;
                    }

                }

                return Ok(artistResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("upload-picture/{artistId}")]
        public async Task<IActionResult> UploadPicture([FromRoute] int artistId, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            if (file != null)
            {
                ArtistResponse artist = await _artistService.UploadPicture(artistId, file);

                if (artist != null)
                {
                    return Ok(artist.PicturePath);
                }

            }

            return BadRequest("No file was uploaded.");
        }
    }
}
