namespace ProtonedMusic_API.Controllers
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
        public async Task<List<CategoryModel>> GetAllCategories()
        {
            return await _categoryService.GetAllCategory();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteCategoryById(int id)
        {
            var product = await _categoryService.GetCategoryById(id);

            if (product == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryById(id);

            return Ok();
        }

        [HttpGet("id")]
        public async Task<CategoryModel> GetCategoryById(int id)
        {
            return await _categoryService.GetCategoryById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> CreateCategory(CategoryModel category)
        {
            // Valider inputmodellen ved at tjekke ModelState for valideringsfejl
            if (!ModelState.IsValid)
            {
                // Returner en BadRequest-response med valideringsfejlene, hvis der er nogen
                return BadRequest(ModelState);
            }

            // Opret produktet ved hjælp af ProductService
            await _categoryService.CreateCategory(category);

            // Returner et CreatedAtActionResult som svar for at bekræfte oprettelsen
            // inkluderer URL'en til den nye ressource og det oprettede produkt
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductModel>> UpdateProduct(int id, CategoryModel UpdateCategory)
        {
            var product = await _categoryService.UpdateCategory(id, UpdateCategory);

            if (product is null)
            {
                return NotFound($"Unable to find user with ID = {UpdateCategory.Id}");
            }

            return Ok(product);
        }
    }
}
