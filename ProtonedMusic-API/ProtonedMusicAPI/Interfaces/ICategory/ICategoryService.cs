namespace ProtonedMusicAPI.Interfaces.ICategory
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAll();
        Task<CategoryResponse> Create(CategoryRequest newCategory);
        Task<CategoryResponse?> FindById(int categoryId);
        Task<CategoryResponse?> UpdateById(int categoryId, CategoryRequest updateCategory);
        Task<CategoryResponse?> DeleteById(int categoryId);
    }
}
