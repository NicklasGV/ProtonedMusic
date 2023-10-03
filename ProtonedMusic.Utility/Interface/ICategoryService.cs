namespace ProtonedMusic.Utility.Interface
{
    public interface ICategoryService
    {
        public Task<List<CategoryModel>> GetAllCategory();

        // Metode til at hente et produkt efter ID
        public Task<CategoryModel> GetCategoryById(int id);

        // Metode til at slette et produkt efter ID
        public Task<CategoryModel> DeleteCategoryById(int id);

        // Metode til at oprette et nyt produkt
        public Task<CategoryModel> CreateCategory(CategoryModel category);

        public Task<CategoryModel> UpdateCategory(CategoryModel UpdateCategory);
    }
}
