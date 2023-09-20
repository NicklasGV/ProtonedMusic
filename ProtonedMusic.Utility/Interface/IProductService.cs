using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    public interface IProductService
    {
        public Task<List<ProductModel>> GetAllProduct();
        public Task<ProductModel> GetProductById(int id);
        public Task<ProductModel> DeleteProductById(int id);
    }
}
