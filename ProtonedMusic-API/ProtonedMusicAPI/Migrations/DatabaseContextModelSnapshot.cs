﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProtonedMusicAPI.Database;

#nullable disable

namespace ProtonedMusicAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.ArtistSong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("MusicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("MusicId");

                    b.ToTable("ArtistSong");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.CalendarContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("FamilyMember")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("CalendarContent");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "T-Shirt"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pop"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Cap"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Special"
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("EventPicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("TimeofEvent")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 1, 4, 10, 27, 3, 490, DateTimeKind.Local).AddTicks(6084),
                            Description = "Test event",
                            Price = 249.95m,
                            TimeofEvent = new DateTime(2023, 5, 2, 23, 23, 0, 0, DateTimeKind.Unspecified),
                            Title = "First Event"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2024, 1, 4, 10, 27, 3, 490, DateTimeKind.Local).AddTicks(6091),
                            Description = "Test 2 for testing making events",
                            Price = 546.95m,
                            TimeofEvent = new DateTime(2023, 9, 17, 13, 20, 0, 0, DateTimeKind.Unspecified),
                            Title = "Super Event"
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.FrontpagePost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Banner")
                        .HasColumnType("int");

                    b.Property<string>("FrontpagePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Frontpages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Banner = 0,
                            FrontpagePicturePath = "assets/img/bannerLogo1.jpg",
                            Text = "Protoned Music"
                        },
                        new
                        {
                            Id = 2,
                            Banner = 1,
                            FrontpagePicturePath = "assets/img/bannerLogo2.jpg",
                            Text = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Illo vel omnis sunt dolores, voluptas"
                        },
                        new
                        {
                            Id = 3,
                            Banner = 2,
                            FrontpagePicturePath = "assets/img/bannerLogo3.jpg",
                            Text = "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Illo vel omnis sunt dolores, voluptas"
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.ItemProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("itemProducts");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Link", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("LinkAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Link");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Music", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SongFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SongName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SongPicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Music");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Album = "Around the worlds",
                            Artist = "Connor Price",
                            SongFilePath = "assets/music/audio1.mp3",
                            SongName = "Chatter",
                            SongPicturePath = ""
                        },
                        new
                        {
                            Id = 2,
                            Album = "Bjørn",
                            Artist = "Sigurd",
                            SongFilePath = "assets/music/audio2.mp3",
                            SongName = "FlipFlop",
                            SongPicturePath = ""
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("News");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTime = new DateTime(2024, 1, 4, 10, 27, 3, 729, DateTimeKind.Local).AddTicks(7419),
                            Text = "Sorry if you lost important data or something funny, but hey whoever needed to resetting the database needed it. You can see under here when it last got reset",
                            Title = "DATABASE GOT RESET"
                        },
                        new
                        {
                            Id = 2,
                            DateTime = new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "So ProtonedMusic's website is now up and running!",
                            Title = "Website Running!"
                        },
                        new
                        {
                            Id = 3,
                            DateTime = new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Text = "Check out my new song in merchandise",
                            Title = "NEW SONG OUT"
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.NewsLike", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("news_Id")
                        .HasColumnType("int");

                    b.Property<int>("user_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("news_Id");

                    b.HasIndex("user_Id");

                    b.ToTable("newsLikes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateTime = new DateTime(2024, 1, 4, 10, 27, 3, 729, DateTimeKind.Local).AddTicks(7464),
                            news_Id = 1,
                            user_Id = 1
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(600)");

                    b.Property<double>("DiscountProcent")
                        .HasColumnType("float");

                    b.Property<bool>("IsDiscounted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ProductPicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Testproduct for seeing 2 categories",
                            DiscountProcent = 20.0,
                            IsDiscounted = true,
                            Name = "Rock Shirt",
                            Price = 399.95m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Testproduct cap",
                            DiscountProcent = 0.0,
                            IsDiscounted = false,
                            Name = "Normal Cap",
                            Price = 560m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Today's video is sponsored by Raid Shadow Legends, one of the biggest mobile role-playing games of 2019 and it's totally free! Currently almost 10 million users have joined Raid over the last six months, and it's one of the most impressive games in its class with detailed models, environments and smooth 60 frames per second animations! All the champions in the game can be customized with unique gear that changes your strategic buffs and abilities! So what are you waiting for? Go to the video description! Good luck and I'll see you there!",
                            DiscountProcent = 0.0,
                            IsDiscounted = false,
                            Name = "Raid Shadow Legends",
                            Price = 299.95m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Testproduct song",
                            DiscountProcent = 80.0,
                            IsDiscounted = true,
                            Name = "Pop song",
                            Price = 760m
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 4
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Upcoming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)");

                    b.Property<DateTime>("timeOf")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("upcomings");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AddonRoles")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Postal")
                        .HasColumnType("int");

                    b.Property<string>("ProfilePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddonRoles = 0,
                            Address = "Test Vej 1",
                            City = "Test By",
                            Country = "Denmark",
                            Email = "testmail1",
                            FirstName = "Joey",
                            LastName = "Test",
                            Password = "$2b$10$2a.jeS0SDQ10Y1jPpZS0EeKu3Uhudk3Sm4zLDeKqmsBS42PSuLMLK",
                            PhoneNumber = 12345678,
                            Postal = 1234,
                            Role = 1
                        },
                        new
                        {
                            Id = 2,
                            AddonRoles = 0,
                            Address = "Test Vej 2",
                            City = "Test By",
                            Country = "Denmark",
                            Email = "testmail2",
                            FirstName = "Børge",
                            LastName = "Jep",
                            Password = "$2b$10$Ih12fCWVFazZhP0qC8GLK.KrZv72nPidlVgjXqNHI/YkmgC2Tnxd.",
                            PhoneNumber = 12345679,
                            Postal = 1234,
                            Role = 0
                        });
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Artist", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.ArtistSong", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProtonedMusicAPI.Database.Entities.Music", "Music")
                        .WithMany()
                        .HasForeignKey("MusicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Music");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.ItemProduct", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProtonedMusicAPI.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Link", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.Artist", null)
                        .WithMany("Links")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.NewsLike", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.News", "News")
                        .WithMany("NewsLikes")
                        .HasForeignKey("news_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProtonedMusicAPI.Database.Entities.User", "User")
                        .WithMany("NewsLikes")
                        .HasForeignKey("user_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Order", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.ProductCategory", b =>
                {
                    b.HasOne("ProtonedMusicAPI.Database.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProtonedMusicAPI.Database.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Artist", b =>
                {
                    b.Navigation("Links");

                    b.Navigation("Songs");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.News", b =>
                {
                    b.Navigation("NewsLikes");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.User", b =>
                {
                    b.Navigation("NewsLikes");
                });
#pragma warning restore 612, 618
        }
    }
}
