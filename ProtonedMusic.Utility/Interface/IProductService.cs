using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Utility.Interface
{
    public interface IProductService
    {
        public Task<List<ProductModel>> GetAllProduct();
    }
}
