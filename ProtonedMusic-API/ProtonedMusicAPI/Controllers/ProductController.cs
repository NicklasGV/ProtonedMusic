namespace ProtonedMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                List<ProductResponse> products = await _productService.GetAllAsync();

                if (products.Count == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] ProductRequest newProduct)
        {
            try
            {
                ProductResponse productResponse = await _productService.CreateAsync(newProduct);

                if (productResponse == null)
                {
                    return Problem("Is null");
                }
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("{productId}")]
        public async Task<IActionResult> FindByIdAsync([FromRoute] int productId)
        {
            try
            {
                var productResponse = await _productService.FindByIdAsync(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{productId}")]
        public async Task<IActionResult> UpdateByIdAsync([FromRoute] int productId, [FromBody] ProductRequest updateProduct)
        {
            try
            {
                var productResponse = await _productService.UpdateByIdAsync(productId, updateProduct);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] int productId)
        {
            try
            {
                var productResponse = await _productService.DeleteByIdAsync(productId);

                if (productResponse == null)
                {
                    return NotFound();
                }
                return Ok(productResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("upload-product-picture/{productId}")]
        public async Task<IActionResult> UploadProductPicture([FromRoute] int productId, IFormFile file)
        {

            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            if (file != null)
            {
                ProductResponse product = await _productService.UploadProductPicture(productId, file);

                if (product != null)
                {
                    return Ok(product.ProductPicturePath);
                }

            }

            return BadRequest("No file was uploaded.");
        }
    }
}
