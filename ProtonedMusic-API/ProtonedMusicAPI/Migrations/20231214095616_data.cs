using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProtonedMusicAPI.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalendarContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FamilyMember = table.Column<string>(type: "nvarchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    EventPicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeofEvent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frontpages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Banner = table.Column<int>(type: "int", nullable: false),
                    FrontpagePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frontpages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Album = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    SongFilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SongPicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    ProductPicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDiscounted = table.Column<bool>(type: "bit", nullable: false),
                    DiscountProcent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "upcomings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(80)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(600)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeOf = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_upcomings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    AddonRoles = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Postal = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ProfilePicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    PicturePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Artist_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "newsLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_Id = table.Column<int>(type: "int", nullable: false),
                    news_Id = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newsLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_newsLikes_News_news_Id",
                        column: x => x.news_Id,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_newsLikes_User_user_Id",
                        column: x => x.user_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistSong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Artist_Id = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    music_Id = table.Column<int>(type: "int", nullable: false),
                    MusicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistSong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistSong_Music_MusicId",
                        column: x => x.MusicId,
                        principalTable: "Music",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Link",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LinkAddress = table.Column<string>(type: "nvarchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Link", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Link_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itemProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_itemProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemProducts_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "T-Shirt" },
                    { 2, "Pop" },
                    { 3, "Cap" },
                    { 4, "Special" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Created", "Description", "EventPicturePath", "Price", "TimeofEvent", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 14, 10, 56, 16, 624, DateTimeKind.Local).AddTicks(177), "Test event", null, 249.95m, new DateTime(2023, 5, 2, 23, 23, 0, 0, DateTimeKind.Unspecified), "First Event" },
                    { 2, new DateTime(2023, 12, 14, 10, 56, 16, 624, DateTimeKind.Local).AddTicks(192), "Test 2 for testing making events", null, 546.95m, new DateTime(2023, 9, 17, 13, 20, 0, 0, DateTimeKind.Unspecified), "Super Event" }
                });

            migrationBuilder.InsertData(
                table: "Frontpages",
                columns: new[] { "Id", "Banner", "FrontpagePicturePath", "Text" },
                values: new object[,]
                {
                    { 1, 0, "assets/img/bannerLogo1.jpg", "Protoned Music" },
                    { 2, 1, "assets/img/bannerLogo2.jpg", "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Illo vel omnis sunt dolores, voluptas" },
                    { 3, 2, "assets/img/bannerLogo3.jpg", "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Illo vel omnis sunt dolores, voluptas" }
                });

            migrationBuilder.InsertData(
                table: "Music",
                columns: new[] { "Id", "Album", "Artist", "SongFilePath", "SongName", "SongPicturePath" },
                values: new object[,]
                {
                    { 1, "Around the worlds", "Connor Price", "assets/music/audio1.mp3", "Chatter", "" },
                    { 2, "Bjørn", "Sigurd", "assets/music/audio2.mp3", "FlipFlop", "" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "DateTime", "Text", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 12, 14, 10, 56, 16, 795, DateTimeKind.Local).AddTicks(2285), "Sorry if you lost important data or something funny, but hey whoever needed to resetting the database needed it. You can see under here when it last got reset", "DATABASE GOT RESET" },
                    { 2, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "So ProtonedMusic's website is now up and running!", "Website Running!" },
                    { 3, new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check out my new song in merchandise", "NEW SONG OUT" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "DiscountProcent", "IsDiscounted", "Name", "Price", "ProductPicturePath" },
                values: new object[,]
                {
                    { 1, "Testproduct for seeing 2 categories", 20.0, true, "Rock Shirt", 399.95m, null },
                    { 2, "Testproduct cap", 0.0, false, "Normal Cap", 560m, null },
                    { 3, "Today's video is sponsored by Raid Shadow Legends, one of the biggest mobile role-playing games of 2019 and it's totally free! Currently almost 10 million users have joined Raid over the last six months, and it's one of the most impressive games in its class with detailed models, environments and smooth 60 frames per second animations! All the champions in the game can be customized with unique gear that changes your strategic buffs and abilities! So what are you waiting for? Go to the video description! Good luck and I'll see you there!", 0.0, false, "Raid Shadow Legends", 299.95m, null },
                    { 4, "Testproduct song", 80.0, true, "Pop song", 760m, null }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "AddonRoles", "Address", "City", "Country", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Postal", "ProfilePicturePath", "Role" },
                values: new object[,]
                {
                    { 1, 0, "Test Vej 1", "Test By", "Denmark", "testmail1", "Joey", "Test", "$2b$10$1xE5ch3IIG1hdKAnunBceue8lf/VOAGWMk6qNNn/GBKDKFXuCjsKu", 12345678, 1234, null, 1 },
                    { 2, 0, "Test Vej 2", "Test By", "Denmark", "testmail2", "Børge", "Jep", "$2b$10$wZYK8Yx6gLuzuy33NUUxruHSSVAflZE98uzqom9Hr2nCf3K7vlLua", 12345679, 1234, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 4, 3 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "newsLikes",
                columns: new[] { "Id", "DateTime", "news_Id", "user_Id" },
                values: new object[] { 1, new DateTime(2023, 12, 14, 10, 56, 16, 795, DateTimeKind.Local).AddTicks(2414), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Artist_UserId",
                table: "Artist",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSong_ArtistId",
                table: "ArtistSong",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistSong_MusicId",
                table: "ArtistSong",
                column: "MusicId");

            migrationBuilder.CreateIndex(
                name: "IX_itemProducts_OrderId",
                table: "itemProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_itemProducts_ProductId",
                table: "itemProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Link_ArtistId",
                table: "Link",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_newsLikes_news_Id",
                table: "newsLikes",
                column: "news_Id");

            migrationBuilder.CreateIndex(
                name: "IX_newsLikes_user_Id",
                table: "newsLikes",
                column: "user_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistSong");

            migrationBuilder.DropTable(
                name: "CalendarContent");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Frontpages");

            migrationBuilder.DropTable(
                name: "itemProducts");

            migrationBuilder.DropTable(
                name: "Link");

            migrationBuilder.DropTable(
                name: "newsLikes");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "upcomings");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
