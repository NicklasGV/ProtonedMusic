using ProtonedMusic.Utility.Models;

namespace ProtonedMusic.Repository.Database
{
    public class DatabaseContext : DbContext
    {
        // Konstruktør, der tager DbContextOptions som parameter
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSet til at repræsentere ProductModel i databasen
        public DbSet<ProductModel> Product { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<ProductCategoryModel> ProductCategories { get; set; }

        ////Hardcode
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>()
                .HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoryModel>()
                .HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Category)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCategoryModel>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<ProductCategoryModel>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategoryModel>().HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel
                {
                    Id = 1,
                    ProductName = "ProductTest1",
                    ProductDescription = "ProductDescription1",
                    ProductPrice = 200M,
                },
                new ProductModel
                {
                    Id = 2,
                    ProductName = "ProductTest2",
                    ProductDescription = "ProductDescription2",
                    ProductPrice = 399.95M
                },
                new ProductModel
                {
                    Id = 3,
                    ProductName = "ProductTest3",
                    ProductDescription = "ProductDescription3",
                    ProductPrice = 600M
                });


            modelBuilder.Entity<CategoryModel>().HasData(new CategoryModel
            {
                Id = 1,
                Name = "Techno"
            },
            new CategoryModel
            {
                Id = 2, 
                Name = "Pop"
            },
            new CategoryModel
            {
                Id = 3,
                Name = "EDM"
            },
            new CategoryModel
            {
                Id = 4,
                Name = "Dubstep"
            });


            modelBuilder.Entity<ProductCategoryModel>().HasData(new ProductCategoryModel 
            {
                CategoryId = 1,
                ProductId = 2
            },
            new ProductCategoryModel
            {
                CategoryId = 2,
                ProductId = 2
            },
            new ProductCategoryModel
            {
                CategoryId = 3,
                ProductId = 3
            },
            new ProductCategoryModel
            {
                CategoryId = 1,
                ProductId = 1

            },
            new ProductCategoryModel
            {
                CategoryId = 4,
                ProductId = 1
            });
        }
    }
}
