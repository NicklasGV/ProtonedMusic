using ProtonedMusicAPI.Interfaces.ICategory;

namespace ProtonedMusicAPI.Services
{
    public class CategoriService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoriService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        private CategoryResponse MapCategoryToCategoryResponse(Category category)
        {
            CategoryResponse response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
            };
            if (category.ProductCategories.Count > 0)
            {
                response.Products = category.ProductCategories.Select(x => new CategoryproductResponse
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    Description = x.Product.Description,
                }).ToList();
            }
            return response;
        }

        private Category MapCategoryRequestToCategory(CategoryRequest newCategory)
        {
            return new Category
            {
                Name = newCategory.Name,
            };
        }

        public async Task<List<CategoryResponse>> GetAll()
        {
            List<Category> categories = await _categoryRepository.GetAll();
            if (categories == null)
            {
                throw new ArgumentNullException();
            }
            return categories.Select(MapCategoryToCategoryResponse).ToList();
        }

        public async Task<CategoryResponse> Create(CategoryRequest newCategory)
        {
            var category = await _categoryRepository.Create(MapCategoryRequestToCategory(newCategory));

            if (newCategory == null)
            {
                throw new ArgumentNullException();
            }
            return MapCategoryToCategoryResponse(category);
        }

        public async Task<CategoryResponse?> FindById(int categoryId)
        {
            var category = await _categoryRepository.FindById(categoryId);

            if (category != null)
            {
                return MapCategoryToCategoryResponse(category);
            }
            return null;
        }

        public async Task<CategoryResponse?> UpdateById(int categoryId, CategoryRequest updateCategory)
        {
            var category = await _categoryRepository.UpdateById(categoryId, MapCategoryRequestToCategory(updateCategory));

            if (category != null)
            {
                return MapCategoryToCategoryResponse(category);
            }
            return null;
        }

        public async Task<CategoryResponse?> DeleteById(int categoryId)
        {
            var category = await _categoryRepository.DeleteById(categoryId);

            if (category != null)
            {
                return MapCategoryToCategoryResponse(category);
            }
            return null;
        }
    }
}
