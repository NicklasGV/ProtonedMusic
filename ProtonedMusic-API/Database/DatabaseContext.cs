
using ProtonedMusic_API.Database.Entities;

namespace ProtonedMusic_API.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>().HasData(
                new Products
                {
                    id = 1,
                    ProductName = "ProductNameTest",
                    ProductCategory = "ProductCategoryTest",
                    ProductDescription = "ProductDescriptionTest",
                    ProductPrice = 200,
                    ProductDate = 18 - 09 - 2023
                });

                
        }
    }
}
