using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsService newsService, INewsRepository newsRepository)
        {
            _newsService = newsService;
            _newsRepository = newsRepository;
        }

        [HttpGet]
        [Route("{newsId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int newsId)
        {
            try
            {
                NewsResponse newsResponse = await _newsService.FindByIdAsync(newsId);

                if (newsResponse == null)
                {
                    return NotFound();
                }
                return Ok(newsResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{newsId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int newsId, [FromBody] NewsRequest updateNews)
        {
            try
            {
                var newsResponse = await _newsService.UpdateByIdAsync(newsId, updateNews);

                if (newsResponse == null)
                {
                    return NotFound();
                }

                return Ok(newsResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpDelete]
        [Route("{newsId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int newsId)
        {
            try
            {
                var newsResponse = await _newsService.DeleteByIdAsync(newsId);
                if (newsResponse == null)
                {
                    return NotFound();
                }
                return Ok(newsResponse);
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
                List<NewsResponse> users = await _newsService.GetAllAsync();

                if (users == null)
                {
                    return Problem("A problem occured the team is fixing it as we speak");
                }

                if (users.Count == 0)
                {
                    return NoContent();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnnonymous]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] NewsRequest newNews)
        {
            try
            {

                if (DateTime.TryParse(newNews.DateTime, out DateTime result))
                {
                    // Parsing was successful, 'result' contains the DateTime value
                    Console.WriteLine(result);
                }
                NewsResponse newsResponse = await _newsService.CreateAsync(newNews);
                return Ok(newsResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
