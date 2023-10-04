namespace ProtonedMusicAPI.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync();
        Task<CategoryResponse> CreateAsync(CategoryRequest newCategory);
        Task<CategoryResponse?> FindByIdAsync(int categoryId);
        Task<CategoryResponse?> UpdateByIdAsync(int categoryId, CategoryRequest updateCategory);
        Task<CategoryResponse?> DeleteByIdAsync(int categoryId);
    }
}
