namespace ProtonedMusicAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.ProductCategories)
            //    .WithOne(pc => pc.Product)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Category>()
            //    .HasMany(p => p.ProductCategories)
            //    .WithOne(pc => pc.Category)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(pc => pc.Category)
            //    .WithMany(p => p.ProductCategories)
            //    .HasForeignKey(pc => pc.CategoryId);

            //modelBuilder.Entity<ProductCategory>()
            //    .HasOne(pc => pc.Product)
            //    .WithMany(p => p.ProductCategories)
            //    .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Joey",
                    LastName = "Test",
                    Email = "TestMail1",
                    Password = "Passw0rd",
                    Role = Role.Admin,
                    PhoneNumber = 12345678,
                    AddressLineOne = "Test Vej 1",
                    AddressLineTwo = "",
                    City = "Test By",
                    Postal = 1234,
                    Country = "Denmark"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Børge",
                    LastName = "Jep",
                    Email = "TestMail2",
                    Password = "Password",
                    Role = Role.Customer,
                    PhoneNumber = 12345679,
                    AddressLineOne = "Test Vej 2",
                    AddressLineTwo = "",
                    City = "Test By",
                    Postal = 1234,
                    Country = "Denmark"
                }
                );
        }
    }
}
