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

        ////Hardcode
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ProductModel>().HasData(
        //        new ProductModel
        //        {
        //            Id = 1,
        //            ProductName = "ProductTest1",
        //            ProductCategory = "ProductCategory1",
        //            ProductDescription = "ProductDescription1",
        //            ProductPrice = 200,
        //        },
        //        new ProductModel
        //        {
        //            Id = 2,
        //            ProductName = "ProductTest2",
        //            ProductCategory = "ProductCategory2",
        //            ProductDescription = "ProductDescription2",
        //            ProductPrice = 400
        //        },
        //        new ProductModel
        //        {
        //            Id = 3,
        //            ProductName = "ProductTest3",
        //            ProductCategory = "ProductCategory3",
        //            ProductDescription = "ProductDescription3",
        //            ProductPrice = 600
        //        });
        //}
    }
}
