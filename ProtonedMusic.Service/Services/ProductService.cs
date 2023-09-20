using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Service.Services
{
    public class ProductService : IProductService
    {
        public IProductRepository _productRepository { get; set; }

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductModel>> GetAllProduct()
        {
            return await _productRepository.GetAllProduct();
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<ProductModel> DeleteProductById(int id)
        {
            return await _productRepository.DeleteProductById(id);
        }
    }
}
