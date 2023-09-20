using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> GetAllProduct();
    }
}
