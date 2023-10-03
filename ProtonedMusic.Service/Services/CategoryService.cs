namespace ProtonedMusic.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private CategoryModel MapCategoryToCategoryModel(CategoryModel category)
        {
            CategoryModel cModel = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
            };
            if (category.ProductCategories.Count > 0)
            {
                cModel.Products = category.ProductCategories.Select(x => new Categoryproduct
                {
                    Id = x.Product.Id,
                    Name = x.Product.ProductName,
                    Price = x.Product.ProductPrice,
                    Description = x.Product.ProductDescription,
                }).ToList();
            }
            return cModel;
        }

        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            var newcategory = await _categoryRepository.CreateCategory(MapCategoryToCategoryModel(category));
            return MapCategoryToCategoryModel(newcategory);
        }

        public async Task<CategoryModel> DeleteCategoryById(int id)
        {
            return await _categoryRepository.DeleteCategoryById(id);
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            List<CategoryModel> categories = await _categoryRepository.GetAllCategory();
            if (categories is null)
            {
                throw new ArgumentNullException();
            }

            return categories.Select(MapCategoryToCategoryModel).ToList();
        }

        public async Task<CategoryModel> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<CategoryModel> UpdateCategory(int categoryId,CategoryModel updateCategory)
        {
            var category = await _categoryRepository.UpdateCategory(categoryId, MapCategoryToCategoryModel(updateCategory));

            return MapCategoryToCategoryModel(updateCategory);
        }
    }
}
