using ProtonedMusic.Repository.Database;
using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public DatabaseContext _context { get; set; }

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<ProductModel>> GetAllProduct()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<ProductModel> GetProductById(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductModel> DeleteProductById(int id)
        {
            var productToDelete = await _context.Product.FindAsync(id);

            if (productToDelete != null)
            {
                _context.Remove(productToDelete);
                await _context.SaveChangesAsync();
            }
            return productToDelete;
        }
    }
}
