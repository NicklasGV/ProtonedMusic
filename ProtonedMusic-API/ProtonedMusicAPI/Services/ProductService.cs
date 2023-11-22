namespace ProtonedMusicAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        private static ProductResponse MapProductToProductResponse(Product product)
        {
            ProductResponse repsonse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ProductPicturePath = product.ProductPicturePath,

            };
            if (product.ProductCategories.Count > 0)
            {
                repsonse.Categories = product.ProductCategories.Select(x => new ProductCategoryResponse
                {
                    Id = x.Category.Id,
                    Name = x.Category.Name,
                }).ToList();
            }
            return repsonse;
        }
        private static Product MapProductRequestToProduct(ProductRequest productRequest)
        {
            return new Product
            {
                Name = productRequest.Name,
                Price = productRequest.Price,
                Description = productRequest.Description,
                ProductPicturePath = productRequest.ProductPicturePath,
                ProductCategories = productRequest.CategoryIds.Select(c => new ProductCategory
                {
                    CategoryId = c
                }).ToList()
            };
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            List<Product> products = await _productRepository.GetAllAsync();
            if (products == null)
            {
                throw new ArgumentNullException();
            }
            return products.Select(MapProductToProductResponse).ToList();
        }

        public async Task<ProductResponse> CreateAsync(ProductRequest newProduct)
        {
            var product = await _productRepository.CreateAsync(MapProductRequestToProduct(newProduct));

            if (product == null)
            {
                throw new ArgumentNullException();
            }
            return MapProductToProductResponse(product);
        }

        public async Task<ProductResponse?> FindByIdAsync(int productId)
        {
            var product = await _productRepository.FindByIdAsync(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }
            return null;
        }

        public async Task<ProductResponse?> UpdateByIdAsync(int productId, ProductRequest updateProduct)
        {
            var product = await _productRepository.UpdateByIdAsync(productId, MapProductRequestToProduct(updateProduct));

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }
            return null;
        }

        public async Task<ProductResponse?> DeleteByIdAsync(int productId)
        {
            var product = await _productRepository.DeleteByIdAsync(productId);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }
            return null;
        }

        public async Task<ProductResponse> UploadProductPicture(int productId, IFormFile file)
        {
            Product product = await _productRepository.UploadProductPicture(productId, file);

            if (product != null)
            {
                return MapProductToProductResponse(product);
            }

            return null;

        }
    }
}
