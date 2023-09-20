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
    }
}
