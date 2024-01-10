namespace ProtonedMusicAPI.Interfaces.ICategory
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> Create(Category newCategory);
        Task<Category?> FindById(int categoryId);
        Task<Category?> UpdateById(int categoryId, Category updateCategory);
        Task<Category?> DeleteById(int categoryId);
    }
}
