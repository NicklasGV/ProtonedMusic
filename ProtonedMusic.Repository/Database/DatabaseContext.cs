using ProtonedMusic.Repository.Database.Entities;

namespace ProtonedMusic.Repository.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Product)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(p => p.ProductCategories)
                .WithOne(pc => pc.Category)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

        }
    }

}