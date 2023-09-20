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
            // Hent alle produkter fra ProductService
            return await _productService.GetAllProduct();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> DeleteProductById(int id)
        {
            // Hent produktet med det angivne ID fra ProductService
            var product = await _productService.GetProductById(id);

            // Hvis produktet ikke findes, returner en 404 Not Found-response
            if (product == null)
            {
                return NotFound(); // Returnerer 404 hvis produktet ikke findes..
            }

            // Slet produktet med det angivne ID fra ProductService
            await _productService.DeleteProductById(id);

            // Returner en OK-response for at bekræfte, at sletningen er udført
            return Ok();
        }

        [HttpGet("id")]
        public async Task<ProductModel> GetProductById(int id)
        {
            // Hent produktet med det angivne ID fra ProductService
            return await _productService.GetProductById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> CreateProduct(ProductModel product)
        {
            // Valider inputmodellen ved at tjekke ModelState for valideringsfejl
            if (!ModelState.IsValid)
            {
                // Returner en BadRequest-response med valideringsfejlene, hvis der er nogen
                return BadRequest(ModelState);
            }

            // Opret produktet ved hjælp af ProductService
            await _productService.CreateProduct(product);

            // Returner et CreatedAtActionResult som svar for at bekræfte oprettelsen
            // inkluderer URL'en til den nye ressource og det oprettede produkt
            return CreatedAtAction(nameof(GetProductById), new {id = product.Id}, product);
        }

    }
}
