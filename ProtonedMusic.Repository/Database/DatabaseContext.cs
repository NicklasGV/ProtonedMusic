using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Repository.Database
{
    public class DatabaseContext : DbContext
    {
        // Konstruktør, der tager DbContextOptions som parameter
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        // DbSet til at repræsentere ProductModel i databasen
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<UserModel> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel
                {
                    Id = 1,
                    ProductName = "ProductTest1",
                    ProductCategory = "ProductCategory1",
                    ProductDescription = "ProductDescription1",
                    ProductPrice = 200,
                },
                new ProductModel
                {
                    Id = 2,
                    ProductName = "ProductTest2",
                    ProductCategory = "ProductCategory2",
                    ProductDescription = "ProductDescription2",
                    ProductPrice = 400
                },
                new ProductModel
                {
                    Id = 3,
                    ProductName = "ProductTest3",
                    ProductCategory = "ProductCategory3",
                    ProductDescription = "ProductDescription3",
                    ProductPrice = 600

                },
                new ProductModel
                {
                    Id = 4,
                    ProductName = "ProductTest4",
                    ProductCategory = "ProductCategory4",
                    ProductDescription = "ProductDescription4",
                    ProductPrice = 800
                },
                new ProductModel
                {
                    Id = 5,
                    ProductName = "ProductTest5",
                    ProductCategory = "ProductCategory5",
                    ProductDescription = "ProductDescription5",
                    ProductPrice = 1000
                });
        }
    }
}
