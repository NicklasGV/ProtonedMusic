using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProtonedMusicAPI.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeofEvent = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
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
                    Description = table.Column<string>(type: "nvarchar(600)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
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
                    PhoneNumber = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Postal = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pop" },
                    { 2, "Metal" },
                    { 3, "EDM" },
                    { 4, "Rock" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Created", "Description", "Price", "TimeofEvent", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 26, 7, 56, 0, 758, DateTimeKind.Local).AddTicks(7978), "Test", 249.95m, new DateTime(2023, 5, 2, 23, 23, 0, 0, DateTimeKind.Unspecified), "Test" },
                    { 2, new DateTime(2023, 10, 26, 7, 56, 0, 758, DateTimeKind.Local).AddTicks(7986), "Test2", 546.95m, new DateTime(2023, 9, 17, 13, 20, 0, 0, DateTimeKind.Unspecified), "Test2" }
                });

            migrationBuilder.InsertData(
                table: "News",
                columns: new[] { "Id", "DateTime", "Text", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 26, 7, 56, 0, 930, DateTimeKind.Local).AddTicks(356), "Sorry if you lost important data or something funny, but hey whoever needed to resetting the database needed it. You can see under here when it last got reset", "SERVER GOT RESET" },
                    { 2, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "So ProtonedMusic's website is now up and running!", "Website Running!" },
                    { 3, new DateTime(2023, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Check out my new song in merchandise", "NEW SONG OUT" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Testproduct-1", "Testproduct-1", 399.95m },
                    { 2, "Testproduct-2", "Testproduct-2", 560m },
                    { 3, "Today's video is sponsored by Raid Shadow Legends, one of the biggest mobile role-playing games of 2019 and it's totally free! Currently almost 10 million users have joined Raid over the last six months, and it's one of the most impressive games in its class with detailed models, environments and smooth 60 frames per second animations! All the champions in the game can be customized with unique gear that changes your strategic buffs and abilities! So what are you waiting for? Go to the video description! Good luck and I'll see you there!", "Testproduct-3", 299.95m },
                    { 4, "Testproduct-4", "Testproduct-4", 760m }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "City", "Country", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Postal", "Role" },
                values: new object[,]
                {
<<<<<<<< HEAD:ProtonedMusic-API/ProtonedMusicAPI/Migrations/20231027073712_data.cs
                    { 1, "Test Vej 1", "Test By", "Denmark", "testmail1", "Joey", "Test", "$2b$10$WGgzmSi7YjLGSByYOJcqy.jvRwI7tdpAXZ/go62lFJs/f3yyxc7QK", 12345678, 1234, 1 },
                    { 2, "Test Vej 2", "Test By", "Denmark", "testmail2", "Børge", "Jep", "$2b$10$woey3r.oxsOBfQhsLEZNwuxo01Qbb5dl4b0CKYLSW.wR3f9r5t6L2", 12345679, 1234, 0 }
========
                    { 1, "Test Vej 1", "Test By", "Denmark", "testmail1", "Joey", "Test", "$2b$10$YI9f91svvzbjxMMe9vKsIuFmdZKSPMuu0qUm0tQe2OdHxZQ2xdhHa", 12345678, 1234, 1 },
                    { 2, "Test Vej 2", "Test By", "Denmark", "testmail2", "Børge", "Jep", "$2b$10$F5e5nqCKtY0nRpmzE2mQzeuZKxSDV5Wu6YB7dEaXCezp9Qa/EmAqy", 12345679, 1234, 0 }
>>>>>>>> e327f01125e630064dd7a2c78835cbe112df68a8:ProtonedMusic-API/ProtonedMusicAPI/Migrations/20231026055601_Data.cs
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 3, 2 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "newsLikes",
                columns: new[] { "Id", "DateTime", "news_Id", "user_Id" },
                values: new object[] { 1, new DateTime(2023, 10, 26, 7, 56, 0, 930, DateTimeKind.Local).AddTicks(424), 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_newsLikes_news_Id",
                table: "newsLikes",
                column: "news_Id");

            migrationBuilder.CreateIndex(
                name: "IX_newsLikes_user_Id",
                table: "newsLikes",
                column: "user_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "newsLikes");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
