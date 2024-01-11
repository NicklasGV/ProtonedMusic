using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtonedMusicAPI.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "itemProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 1, 4, 10, 27, 3, 490, DateTimeKind.Local).AddTicks(6084));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 1, 4, 10, 27, 3, 490, DateTimeKind.Local).AddTicks(6091));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2024, 1, 4, 10, 27, 3, 729, DateTimeKind.Local).AddTicks(7419));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$2a.jeS0SDQ10Y1jPpZS0EeKu3Uhudk3Sm4zLDeKqmsBS42PSuLMLK");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$Ih12fCWVFazZhP0qC8GLK.KrZv72nPidlVgjXqNHI/YkmgC2Tnxd.");

            migrationBuilder.UpdateData(
                table: "newsLikes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2024, 1, 4, 10, 27, 3, 729, DateTimeKind.Local).AddTicks(7464));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "itemProducts");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 1, 2, 8, 33, 39, 772, DateTimeKind.Local).AddTicks(6422));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2024, 1, 2, 8, 33, 39, 772, DateTimeKind.Local).AddTicks(6435));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2024, 1, 2, 8, 33, 39, 909, DateTimeKind.Local).AddTicks(7829));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$O0QDlj4wIjt01vf9B.D2w.QgeTADH6NZqY3bFRc0tF6VlrQca68PW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$aSVCKChmxb9Skb7t1qr2EORIdi8/6bOytJF.l98.J2u8DgWXb4XwG");

            migrationBuilder.UpdateData(
                table: "newsLikes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2024, 1, 2, 8, 33, 39, 909, DateTimeKind.Local).AddTicks(7897));
        }
    }
}
