namespace ProtonedMusic_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        IProductService _productService {  get; set; }

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<ProductModel>> GetAllProducts()
        {
            return await _productService.GetAllProduct();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound(); // Returnerer 404 hvis produktet ikke findes.
            }

            await _productService.DeleteProductById(id);

            return Ok();
        }

        [HttpGet("id")]
        public async Task<ProductModel> GetProductById(int id)
        {
            return await _productService.GetProductById(id);
        }

    }
}
