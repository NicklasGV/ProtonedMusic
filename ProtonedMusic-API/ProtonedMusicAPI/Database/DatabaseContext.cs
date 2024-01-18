namespace ProtonedMusicAPI.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<NewsLike> newsLikes { get; set; }
        public DbSet<FrontpagePost> Frontpages { get; set; }
        public DbSet<Upcoming> upcomings { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<CalendarContent> CalendarContent { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ItemProduct> itemProducts { get; set; }
        public DbSet<Artist> Artist { get; set; }
        public DbSet<Link> Link { get; set; }
        public DbSet<ArtistSong> ArtistSong { get; set; }
        public DbSet<ArtistLink> ArtistLink { get; set; }
        public DbSet<FooterPost> FooterPosts { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

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
            modelBuilder.Entity<ProductOrder>().HasKey(pc => new { pc.ProductId, pc.Id });

            modelBuilder.Entity<CalendarContent>().HasData(new CalendarContent
            {
                Id =1,
                Title = "Title",
                Content = "This is a test content of a calendar content",
                Date = new DateTime(),
                ArtistId = 1,
            });

            modelBuilder.Entity<FooterPost>().HasData(new FooterPost
            {
                Id = 1,
                Description = "Immerse yourself in the pulsating beats and electrifying rhythms of Protoned Music. Elevate your auditory experience and let the music take you to new heights. Protoned Music - where every beat is a journey.",
                Address = "Ballerup-Centret 2, 2750 Ballerup",
                AddressMapLink = "https://maps.app.goo.gl/A9awSZe6Lm2mnzBVA",
                Mail = "Info@protonedmusic.com",
                Phonenumber = "+45 12345678"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Rock Shirt",
                Price = 399.95M,
                Description = "Testproduct for seeing 2 categories",
                IsDiscounted = true,
                DiscountProcent = 20,
            },
            new Product
            {
                Id = 2,
                Name = "Normal Cap",
                Price = 560,
                Description = "Testproduct cap",
            },
            new Product
            {
                Id = 3,
                Name = "Raid Shadow Legends",
                Price = 299.95M,
                Description = "Today's video is sponsored by Raid Shadow Legends, one of the biggest mobile role-playing games of 2019 and it's totally free! Currently almost 10 million users have joined Raid over the last six months, and it's one of the most impressive games in its class with detailed models, environments and smooth 60 frames per second animations! All the champions in the game can be customized with unique gear that changes your strategic buffs and abilities! So what are you waiting for? Go to the video description! Good luck and I'll see you there!"
            },
            new Product
            {
                Id = 4,
                Name = "Pop song",
                Price = 760,
                Description = "Testproduct song",
                IsDiscounted = true,
                DiscountProcent = 80,
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "T-Shirt"
            },
            new Category
            {
                Id = 2,
                Name = "Pop"
            },
            new Category
            {
                Id = 3,
                Name = "Cap"
            },
            new Category
            {
                Id = 4,
                Name = "Special"
            });

            modelBuilder.Entity<ProductCategory>().HasData(new ProductCategory
            {
                CategoryId = 1,
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
                CategoryId = 4,
                ProductId = 3,
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
                    Title = "First Event",
                    Description = "Test event",
                    Price = 249.95M,
                    Created = DateTime.Now,
                    TimeofEvent = new DateTime(2023,5,2,23,23,00),
                },
                new Event
                {
                    Id = 2,
                    Title = "Super Event",
                    Description = "Test 2 for testing making events",
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

            modelBuilder.Entity<FrontpagePost>().HasData(new FrontpagePost
            {
                Id = 1,
                Text = "Protoned Music",
                Banner = Banner.LeftBanner,
                FrontpagePicturePath = "assets/img/bannerLogo1.jpg"
            },
            new FrontpagePost
            {
                Id = 2,
                Text = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Illo vel omnis sunt dolores, voluptas",
                Banner = Banner.RightBanner,
                FrontpagePicturePath = "assets/img/bannerLogo2.jpg"
            },
            new FrontpagePost
            {
                Id = 3,
                Text = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Illo vel omnis sunt dolores, voluptas",
                Banner = Banner.MiddleBanner,
                FrontpagePicturePath = "assets/img/bannerLogo3.jpg"
            });

            modelBuilder.Entity<Music>().HasData(new Music
            {
                Id = 1,
                SongName = "Chipi chipi chapa chapa",
                Album = "Chipi chipi",
                SongFilePath = "assets/music/chibichibi.mp3",
                SongPicturePath = "assets/img/chipichapa.gif"
            },
            new Music
            {
                Id = 2,
                SongName = "FlipFlop",
                Album = "Bjørn",
                SongFilePath = "assets/music/audio2.mp3",
                SongPicturePath = ""
            },
            new Music
            {
                Id = 3,
                SongName = "Chatter",
                Album = "Around the worlds",
                SongFilePath = "assets/music/audio1.mp3",
                SongPicturePath = ""
            });

            modelBuilder.Entity<Artist>().HasData(new Artist
            {
                Id = 1,
                UserId = 1,
                Name = "Joey Testoe",
                Info = "Bedste Sanger",
            });

            modelBuilder.Entity<ArtistSong>().HasData(new ArtistSong
            {
                Id = 1,
                ArtistId = 1,
                MusicId = 1,
            },
            new ArtistSong
            {
                Id = 2,
                ArtistId = 1,
                MusicId = 2,
            },
            new ArtistSong
            {
                Id = 3,
                ArtistId = 1,
                MusicId = 3,
            });

            modelBuilder.Entity<ArtistLink>().HasData(new ArtistLink
            {
                Id = 1,
                ArtistId = 1,
                LinkId = 1,
            });

            modelBuilder.Entity<Link>().HasData(new Link
            {
                Id = 1,
                Title = "Discord",
                LinkAddress = "https://discord.gg/Jt4rwUZGGS"
            });

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                OrderNumber = "4654322",
                //quantity = 5,
            });
            modelBuilder.Entity<ProductOrder>().HasData(new ProductOrder
            {
                OrderId = 1,
                ProductId = 1,
            });
        }
    }
}
