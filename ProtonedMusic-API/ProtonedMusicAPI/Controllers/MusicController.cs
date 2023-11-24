namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private readonly IMusicService _musicService;
        private readonly IMusicRepository _musicRepository;
       

        public MusicController(IMusicService musicService, IMusicRepository musicRepository)
        {
            _musicService = musicService;
            _musicRepository = musicRepository;
        }



        [HttpGet]
        [Route("{musicId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int musicId)
        {
            try
            {
                MusicResponse musicResponse = await _musicService.FindByIdAsync(musicId);

                if (musicResponse == null)
                {
                    return NotFound();
                }
                return Ok(musicResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{musicId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int musicId, [FromBody] MusicRequest updateMusic, IFormFile song, IFormFile file)
        {
            try
            {
                if (song != null)
                {
                    MusicResponse musicSong = await _musicService.UploadSong(musicId, song);
                }
                if (file != null)
                {
                    MusicResponse musicPicture = await _musicService.UploadSongPicture(musicId, file);

                }

                var musicResponse = await _musicService.UpdateByIdAsync(musicId, updateMusic);

                if (musicResponse == null)
                {
                    return NotFound();
                }

                return Ok(musicResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{musicId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int musicId)
        {
            try
            {
                var musicResponse = await _musicService.DeleteByIdAsync(musicId);
                if (musicResponse == null)
                {
                    return NotFound();
                }
                return Ok(musicResponse);
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
                List<MusicResponse> musics = await _musicService.GetAllAsync();

                if (musics == null)
                {
                    return Problem("A problem occured the team is fixing it as we speak");
                }

                if (musics.Count == 0)
                {
                    return NoContent();
                }
                return Ok(musics);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] MusicRequest newMusic)
        {
            try
            {
                MusicResponse musicResponse = await _musicService.CreateAsync(newMusic);
                if (newMusic.SongFile != null)
                {
                    MusicResponse musicSong = await _musicService.UploadSong(musicResponse.Id, newMusic.SongFile);

                    if (musicSong != null)
                    {
                        musicResponse = musicSong;
                    }

                }
                if (newMusic.PictureFile != null)
                {
                    MusicResponse musicPicture = await _musicService.UploadSongPicture(musicResponse.Id, newMusic.PictureFile);

                    if (musicPicture != null)
                    {
                        musicResponse = musicPicture;
                    }

                }
                return Ok(musicResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("upload-song/{musicId}")]
        public async Task<IActionResult> UploadSong([FromRoute] int musicId, IFormFile song)
        {

            if (song == null || song.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            if (song != null)
            {
                MusicResponse music = await _musicService.UploadSong(musicId, song);

                if (music != null)
                {
                    return Ok(music.SongPicturePath);
                }

            }

            return BadRequest("No file was uploaded.");
        }

        [HttpPost]
        [Route("upload-song-picture/{musicId}")]
        public async Task<IActionResult> UploadSongPicture([FromRoute] int musicId, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            if (file != null)
            {
                MusicResponse music = await _musicService.UploadSongPicture(musicId, file);

                if (music != null)
                {
                    return Ok(music.SongPicturePath);
                }
                
            }

            return BadRequest("No file was uploaded.");
        }
    }
}
