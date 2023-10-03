using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Service.Services
{
    public class ProductService : IProductService
    {
        // Repository til dataadgang
        public IProductRepository _productRepository { get; set; }

        // Konstruktør, der tager et IProductRepository som parameter
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Metode til at hente alle produkter
        public async Task<List<ProductModel>> GetAllProduct()
        {
            // Kalder GetAllProduct-metoden i det underliggende repository for at hente produkter
            return await _productRepository.GetAllProduct();
        }

        // Metode til at hente et produkt efter ID
        public async Task<ProductModel> GetProductById(int id)
        {
            // Kalder GetProductById-metoden i det underliggende repository for at hente et produkt efter ID
            return await _productRepository.GetProductById(id);
        }

        // Metode til at slette et produkt efter ID
        public async Task<ProductModel> DeleteProductById(int id)
        {
            // Kalder DeleteProductById-metoden i det underliggende repository for at slette et produkt efter ID
            return await _productRepository.DeleteProductById(id);
        }

        // Metode til at oprette et nyt produkt
        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            // Kalder CreateProduct-metoden i det underliggende repository for at oprette et nyt produkt
            return await _productRepository.CreateProduct(product);
        }

        public async Task<ProductModel> UpdateProduct(ProductModel UpdateProduct)
        {
            var product = await _productRepository.UpdateProduct(UpdateProduct);

            if (product is null)
            {
                return null;
            }

            product.ProductPrice = UpdateProduct.ProductPrice;
            product.ProductName = UpdateProduct.ProductName;
            product.ProductDescription = UpdateProduct.ProductDescription;

            return await _productRepository.UpdateProduct(product);
        }
    }
}
