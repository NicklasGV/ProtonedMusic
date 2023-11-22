﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProtonedMusicAPI.Database;

#nullable disable

namespace ProtonedMusicAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231116065639_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
                            Name = "Pop"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Metal"
                        },
                        new
                        {
                            Id = 3,
                            Name = "EDM"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Rock"
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
                            Created = new DateTime(2023, 11, 16, 7, 56, 39, 354, DateTimeKind.Local).AddTicks(1318),
                            Description = "Test",
                            Price = 249.95m,
                            TimeofEvent = new DateTime(2023, 5, 2, 23, 23, 0, 0, DateTimeKind.Unspecified),
                            Title = "Test"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2023, 11, 16, 7, 56, 39, 354, DateTimeKind.Local).AddTicks(1329),
                            Description = "Test2",
                            Price = 546.95m,
                            TimeofEvent = new DateTime(2023, 9, 17, 13, 20, 0, 0, DateTimeKind.Unspecified),
                            Title = "Test2"
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

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Image", b =>
                {
                    b.Property<int?>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ImageId"));

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
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
                            DateTime = new DateTime(2023, 11, 16, 7, 56, 39, 490, DateTimeKind.Local).AddTicks(3386),
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
                            DateTime = new DateTime(2023, 11, 16, 7, 56, 39, 490, DateTimeKind.Local).AddTicks(3438),
                            news_Id = 1,
                            user_Id = 1
                        });
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
                            Description = "Testproduct-1",
                            Name = "Testproduct-1",
                            Price = 399.95m
                        },
                        new
                        {
                            Id = 2,
                            Description = "Testproduct-2",
                            Name = "Testproduct-2",
                            Price = 560m
                        },
                        new
                        {
                            Id = 3,
                            Description = "Today's video is sponsored by Raid Shadow Legends, one of the biggest mobile role-playing games of 2019 and it's totally free! Currently almost 10 million users have joined Raid over the last six months, and it's one of the most impressive games in its class with detailed models, environments and smooth 60 frames per second animations! All the champions in the game can be customized with unique gear that changes your strategic buffs and abilities! So what are you waiting for? Go to the video description! Good luck and I'll see you there!",
                            Name = "Testproduct-3",
                            Price = 299.95m
                        },
                        new
                        {
                            Id = 4,
                            Description = "Testproduct-4",
                            Name = "Testproduct-4",
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
                            ProductId = 1,
                            CategoryId = 2
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
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("Postal")
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
                            Password = "$2b$10$8W7RuoiAYo/FEuY/HVb6Z.grWc306JaYwmWJoRX2Il3ZaQmBKWqaK",
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
                            Password = "$2b$10$.mS8aMiz/ZA7OBatqgb.weRAcnOHJJVU7Xq4RQnAaNf7XZauUcd6K",
                            PhoneNumber = 12345679,
                            Postal = 1234,
                            Role = 0
                        });
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

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("ProtonedMusicAPI.Database.Entities.News", b =>
                {
                    b.Navigation("NewsLikes");
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
