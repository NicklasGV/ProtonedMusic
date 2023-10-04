namespace ProtonedMusicAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Product
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();
        }
        public async Task<Product> CreateAsync(Product newProduct)
        {
            _context.Product.Add(newProduct);

            await _context.SaveChangesAsync();
            newProduct = await FindByIdAsync(newProduct.Id);
            return newProduct;
        }

        public async Task<Product?> FindByIdAsync(int productId)
        {
            return await _context.Product
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<Product?> UpdateByIdAsync(int productId, Product updateProduct)
        {
            //FindById erstattes med koden inklusiv AsNoTracking da Entity Framework ikke kan håndtere at der bliver arbejdet med to objekter der er ens.
            var product = await _context.Product
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                // Vi anskaffer os de eksisterende kategorier og de opdateret kategorier så de kan samlignes.
                List<int> existing = new List<int>(product.ProductCategories.Select(pc => pc.CategoryId).ToList());
                List<int> update = new List<int>(updateProduct.ProductCategories.Select(pc => pc.CategoryId).ToList());
                // Vi undtager her de opdateret kategorier i den eksisterende liste og modsat med den eksisterende og opdateret liste.
                List<int> insert = update.Except(existing).ToList();
                List<int> remove = existing.Except(update).ToList();

                if (insert.Count > 0)
                {
                    List<ProductCategory> InsertProductCategories = insert.Select(i => new ProductCategory { CategoryId = i, ProductId = productId }).ToList();
                    _context.ProductCategories.AddRange(InsertProductCategories);
                }

                if (remove.Count > 0)
                {
                    List<ProductCategory> removeProductCategories = remove.Select(r => new ProductCategory { CategoryId = r, ProductId = productId }).ToList();
                    _context.ProductCategories.RemoveRange(removeProductCategories);
                }
                //Vi laver en AddRange og RemoveRange for at tilføje de nye og opdateret kategorier og fjerne de gamle som måske ikke skulle være der.

                //Her opdateres de standarde ting med navn osv.
                product = await _context.Product.FindAsync(productId);
                product.Name = updateProduct.Name;
                product.Price = updateProduct.Price;
                product.Description = updateProduct.Description;

                await _context.SaveChangesAsync();
                //Vi finder produktet igen med alle de nye ændringer og sender den til produkt som vi retunere
                product = await FindByIdAsync(productId);
            }
            return product;
        }

        public async Task<Product?> DeleteByIdAsync(int productId)
        {
            var product = await FindByIdAsync(productId);

            if (product != null)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }
    }
}
