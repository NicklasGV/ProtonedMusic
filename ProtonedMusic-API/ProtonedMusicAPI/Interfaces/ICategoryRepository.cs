namespace ProtonedMusicAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> CreateAsync(Category newCategory);
        Task<Category?> FindByIdAsync(int categoryId);
        Task<Category?> UpdateByIdAsync(int categoryId, Category updateCategory);
        Task<Category?> DeleteByIdAsync(int categoryId);
    }
}
