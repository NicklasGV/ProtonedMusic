using ProtonedMusicAPI.Database.Entities;
using ProtonedMusicAPI.Interfaces.ICategory;

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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CategoryResponse> category = await _categoryService.GetAll();

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
        public async Task<IActionResult> Create([FromBody] CategoryRequest newCategory)
        {
            try
            {
                CategoryResponse categoryResponse = await _categoryService.Create(newCategory);

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
        public async Task<IActionResult> FindById([FromRoute] int categoryId)
        {
            try
            {
                var categoryResponse = await _categoryService.FindById(categoryId);

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
        public async Task<IActionResult> UpdateById([FromRoute] int categoryId, [FromBody] CategoryRequest updateCategory)
        {
            try
            {
                var categoryResponse = await _categoryService.UpdateById(categoryId, updateCategory);

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

        [HttpDelete]
        [Route("{categoryId}")]
        public async Task<IActionResult> DeleteById([FromRoute] int categoryId)
        {
            try
            {
                var categoryResponse = await _categoryService.DeleteById(categoryId);
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
