using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic;
using System.ComponentModel;

namespace ProtonedMusicAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<NewsLike> newsLikes { get; set; }
        public DbSet<Upcoming> upcomings { get; set; }

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

            modelBuilder.Entity<User>()
                .HasMany(u => u.NewsLikes)
                .WithOne(nl => nl.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<News>()
                .HasMany(n => n.NewsLikes)
                .WithOne(nl => nl.News)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NewsLike>()
                .HasOne(nl => nl.User)
                .WithMany(u => u.NewsLikes)
                .HasForeignKey(nl => nl.user_Id);

            modelBuilder.Entity<NewsLike>()
                .HasOne(nl => nl.News)
                .WithMany(n => n.NewsLikes)
                .HasForeignKey(nl => nl.news_Id);


            modelBuilder.Entity<ProductCategory>().HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Testproduct-1",
                Price = 399.95M,
                Description = "Testproduct-1"
            },
            new Product
            {
                Id = 2,
                Name = "Testproduct-2",
                Price = 560,
                Description = "Testproduct-2",
            },
            new Product
            {
                Id = 3,
                Name = "Testproduct-3",
                Price = 299.95M,
                Description = "Today's video is sponsored by Raid Shadow Legends, one of the biggest mobile role-playing games of 2019 and it's totally free! Currently almost 10 million users have joined Raid over the last six months, and it's one of the most impressive games in its class with detailed models, environments and smooth 60 frames per second animations! All the champions in the game can be customized with unique gear that changes your strategic buffs and abilities! So what are you waiting for? Go to the video description! Good luck and I'll see you there!"
            },
            new Product
            {
                Id = 4,
                Name = "Testproduct-4",
                Price = 760,
                Description = "Testproduct-4",
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Pop"
            },
            new Category
            {
                Id = 2,
                Name = "Metal"
            },
            new Category
            {
                Id = 3,
                Name = "EDM"
            },
            new Category
            {
                Id = 4,
                Name = "Rock"
            });

            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                CategoryId = 1,
                ProductId = 1,
            }, new ProductCategory
            {
                CategoryId = 2,
                ProductId = 1,
            },
            new ProductCategory
            {
                CategoryId = 1,
                ProductId = 2
            },
            new ProductCategory
            {
                CategoryId = 3,
                ProductId = 2,
            },
            new ProductCategory
            {
                CategoryId = 2,
                ProductId = 4,
            });

            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test",
                    Price = 249.95M,
                    Created = DateTime.Now,
                    TimeofEvent = new DateTime(2023,5,2,23,23,00),
                },
                new Event
                {
                    Id = 2,
                    Title = "Test2",
                    Description = "Test2",
                    Price = 546.95M,
                    Created = DateTime.Now,
                    TimeofEvent = new DateTime(2023, 9, 17, 13, 20, 00),
                }); ;


            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Joey",
                    LastName = "Test",
                    Email = "testmail1",
                    Password = BCrypt.Net.BCrypt.HashPassword("Passw0rd"),
                    Role = Role.Admin,
                    PhoneNumber = 12345678,
                    Address = "Test Vej 1",
                    City = "Test By",
                    Postal = 1234,
                    Country = "Denmark"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Børge",
                    LastName = "Jep",
                    Email = "testmail2",
                    Password = BCrypt.Net.BCrypt.HashPassword("Password"),
                    Role = Role.Customer,
                    PhoneNumber = 12345679,
                    Address = "Test Vej 2",
                    City = "Test By",
                    Postal = 1234,
                    Country = "Denmark"
                }
                );

            modelBuilder.Entity<News>().HasData(new News
            {
                Id = 1,
                Title = "DATABASE GOT RESET",
                Text = "Sorry if you lost important data or something funny, but hey whoever needed to resetting the database needed it. You can see under here when it last got reset",
                DateTime = DateTime.Now,
            },
            new News
            {
                Id = 2,
                Title = "Website Running!",
                Text = "So ProtonedMusic's website is now up and running!",
                DateTime = new DateTime(2022, 01, 01),
            },
            new News
            {
                Id = 3,
                Title = "NEW SONG OUT",
                Text = "Check out my new song in merchandise",
                DateTime = new DateTime(2023, 08, 12),
            });

            modelBuilder.Entity<NewsLike>().HasData(new NewsLike
            {
                Id = 1,
                user_Id = 1,
                news_Id = 1,
                DateTime = DateTime.Now,

            });
        }
    }
}
