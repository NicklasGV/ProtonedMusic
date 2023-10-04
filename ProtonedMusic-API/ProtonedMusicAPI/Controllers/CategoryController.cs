namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<CategoryResponse> category = await _categoryService.GetAllAsync();

                if (category.Count == 0)
                {
                    return NoContent();
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CategoryRequest newCategory)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.CreateAsync(newCategory);

                if (categoryResponse == null)
                {
                    return Problem("Is null");
                }
                return Ok(newCategory);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int categoryId)
        {
            try
            {
                var categoryResponse = await _categoryService.FindByIdAsync(categoryId);

                if (categoryResponse == null)
                {
                    return NotFound();
                }
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }

        [HttpPut]
        [Route("{categoryId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int categoryId, [FromBody] CategoryRequest updateCategory)
        {
            try
            {
                var categoryResponse = await _categoryService.UpdateByIdAsync(categoryId, updateCategory);

                if (categoryResponse == null)
                {
                    return NotFound();
                }
                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
