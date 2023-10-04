﻿namespace ProtonedMusicAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Category
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .ToListAsync();
        }

        public async Task<Category> CreateAsync(Category newCategory)
        {
            _context.Category.Add(newCategory);

            await _context.SaveChangesAsync();
            newCategory = await FindByIdAsync(newCategory.Id);
            return newCategory;
        }

        public async Task<Category?> FindByIdAsync(int categoryId)
        {
            return await _context.Category
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Product)
                .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Category?> UpdateByIdAsync(int categoryId, Category updateCategory)
        {
            var category = await FindByIdAsync(categoryId);

            if (category != null)
            {
                category.Name = updateCategory.Name;

                await _context.SaveChangesAsync();
            }
            return category;
        }

        public async Task<Category> DeleteByIdAsync(int categoryId)
        {
            var category = await FindByIdAsync(categoryId);

            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
            return category;
        }

    }
}
