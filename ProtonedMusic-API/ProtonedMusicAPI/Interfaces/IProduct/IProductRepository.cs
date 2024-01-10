namespace ProtonedMusicAPI.Interfaces.IProduct
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> CreateAsync(Product newProduct);
        Task<Product?> FindByIdAsync(int productId);
        Task<Product?> UpdateByIdAsync(int productId, Product updateProduct);
        Task<Product?> DeleteByIdAsync(int productId);
        Task<Product?> UploadProductPicture(int productId, IFormFile file);
    }
}
