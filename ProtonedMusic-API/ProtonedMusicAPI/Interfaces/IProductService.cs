namespace ProtonedMusicAPI.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse> CreateAsync(ProductRequest newProduct);
        Task<ProductResponse?> FindByIdAsync(int productId);
        Task<ProductResponse?> UpdateByIdAsync(int productId, ProductRequest updateProduct);
        Task<ProductResponse?> DeleteByIdAsync(int productId);
        Task<ProductResponse> UploadProductPicture(int productId, IFormFile file);
    }
}
