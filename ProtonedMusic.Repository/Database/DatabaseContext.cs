using ProtonedMusic.Utility.Helpers;
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

        ////Hardcode
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
                });

            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    FirstName = "Joey",
                    LastName = "Test",
                    Email = "Testmail",
                    Password = "passw0rd",
                    Role = (CloudinaryDotNet.Actions.Role)Role.Admin,
                    PhoneNumber = "1234567890",
                    Address = "Test Vej 1",
                    City = "Test By",
                    Postal = "1234",
                    Country = "Denmark"

                },
                new UserModel
                {
                    Id = 2,
                    FirstName = "Børge",
                    LastName = "Test",
                    Email = "Testmail",
                    Password = "password",
                    Role = (CloudinaryDotNet.Actions.Role)Role.Customer,
                    PhoneNumber = "1234567890",
                    Address = "Test Vej 1",
                    City = "Test By",
                    Postal = "1234",
                    Country = "Denmark"
                });
        }


        //private List<ProductModel> testData = new List<ProductModel>{
        //        new ProductModel()
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
        //        }
        //};

        //public List<ProductModel> ProductsTestData
        //{
        //    get { return testData ; }
        //}
    }

}