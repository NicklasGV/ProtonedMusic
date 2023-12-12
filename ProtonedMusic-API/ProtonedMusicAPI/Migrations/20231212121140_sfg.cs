using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProtonedMusicAPI.Migrations
{
    /// <inheritdoc />
    public partial class sfg : Migration
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

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 12, 13, 11, 40, 245, DateTimeKind.Local).AddTicks(516));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 12, 12, 13, 11, 40, 245, DateTimeKind.Local).AddTicks(530));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 12, 12, 13, 11, 40, 383, DateTimeKind.Local).AddTicks(5397));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$yCoidSNNPB9uXkNBmTw0lu4dkr6lktgjJ/VGyAthH9HvN65sCfWAW");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$ABdZuqFu0qr9DgcocOqSBeTrqFpDbvegJYOHay0IsxprtI/xzdH8S");

            migrationBuilder.UpdateData(
                table: "newsLikes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 12, 12, 13, 11, 40, 383, DateTimeKind.Local).AddTicks(5531));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarContent");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2023, 12, 4, 8, 44, 12, 629, DateTimeKind.Local).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 12, 4, 8, 44, 12, 629, DateTimeKind.Local).AddTicks(6197));

            migrationBuilder.UpdateData(
                table: "News",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 12, 4, 8, 44, 12, 879, DateTimeKind.Local).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2b$10$6hsOyOwFUILMlHcEOnvhOeGRpuip7ku6aZXhD2H721rBFub7g5oMa");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2b$10$jI1e7/yEHeZg57973i4rNOiRcZEXMtv3KoX.mgX/B/wMvYSME6a.C");

            migrationBuilder.UpdateData(
                table: "newsLikes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2023, 12, 4, 8, 44, 12, 879, DateTimeKind.Local).AddTicks(9795));
        }
    }
}
