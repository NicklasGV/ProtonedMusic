using ProtonedMusic.Repository.Database;
using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // DatabaseContext til dataadgang
        public DatabaseContext _context { get; set; }

        // Konstruktør, der tager en DatabaseContext som parameter
        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        // Hent alle produkter fra databasen
        public async Task<List<ProductModel>> GetAllProduct()
        {
            return await _context.Product.ToListAsync();
        }

        // Hent et produkt efter ID fra databasen
        public async Task<ProductModel> GetProductById(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Slet et produkt efter ID fra databasen
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

        // Opret et nyt produkt i databasen
        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
