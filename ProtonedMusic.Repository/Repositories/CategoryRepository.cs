using ProtonedMusic.Repository.Database;
using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        // DatabaseContext til dataadgang
        public DatabaseContext _context { get; set; }

        // Konstruktør, der tager en DatabaseContext som parameter
        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Hent alle produkter fra databasen
        public async Task<List<CategoryModel>> GetAllCategory()
        {
            return await _context.Category.ToListAsync();
        }

        // Hent et produkt efter ID fra databasen
        public async Task<CategoryModel> GetCategoryById(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Slet et produkt efter ID fra databasen
        public async Task<CategoryModel> DeleteCategoryById(int id)
        {
            var CategoryToDelete = await _context.Category.FindAsync(id);

            if (CategoryToDelete != null)
            {
                _context.Remove(CategoryToDelete);
                await _context.SaveChangesAsync();
            }
            return CategoryToDelete;
        }

        // Opret et nyt produkt i databasen
        public async Task<CategoryModel> CreateCategory(CategoryModel category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<CategoryModel> UpdateCategory(CategoryModel category)
        {
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
