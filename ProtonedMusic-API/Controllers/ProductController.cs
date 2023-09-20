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
    }
}
