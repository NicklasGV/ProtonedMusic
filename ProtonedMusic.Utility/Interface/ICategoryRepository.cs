namespace ProtonedMusic.Utility.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryModel>> GetAllCategory();

        public Task<CategoryModel> GetCategoryById(int id);

        public Task<CategoryModel> DeleteCategoryById(int id);

        public Task<CategoryModel> CreateCategory(CategoryModel category);

        public Task<CategoryModel> UpdateCategory(int categoryId, CategoryModel updateCategory);
    }
}
