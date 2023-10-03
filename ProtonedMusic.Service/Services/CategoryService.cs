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
            return await _categoryRepository.CreateCategory(category);
        }

        public async Task<CategoryModel> DeleteCategoryById(int id)
        {
            return await _categoryRepository.DeleteCategoryById(id);
        }

        public async Task<List<CategoryModel>> GetAllCategory()
        {
            List<CategoryModel> categories = await _categoryRepository.GetAllCategory();

            return categories.Select(MapCategoryToCategoryModel).ToList();
        }

        public async Task<CategoryModel> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }

        public async Task<CategoryModel> UpdateCategory(CategoryModel updateCategory)
        {
            CategoryModel categoryProduct = new CategoryModel
            {
                Id = updateCategory.Id,
                Name = updateCategory.Name,
            };
            if (updateCategory.ProductCategories.Count > 0)
            {
                categoryProduct.Products = updateCategory.ProductCategories.Select(x => new Categoryproduct
                {
                    Id = x.Product.Id,
                    Name = x.Product.ProductName,
                    Price = x.Product.ProductPrice,
                    Description = x.Product.ProductDescription
                }).ToList();
            }

            return await _categoryRepository.UpdateCategory(updateCategory);
        }
    }
}
