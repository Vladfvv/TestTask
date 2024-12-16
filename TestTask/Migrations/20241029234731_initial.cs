using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestTask.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    QuantityPublished = table.Column<int>(type: "int", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "John", "Smith" },
                    { 2, "Ivan", "Karpov" },
                    { 3, "Pavel", "Doe" },
                    { 6, "Frank", "Sidorov" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Price", "PublishDate", "QuantityPublished", "Title" },
                values: new object[,]
                {
                    { 1, 1, 15.029999999999999, new DateTime(2015, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 1600, "The Red Army" },
                    { 3, 2, 4.0, new DateTime(2020, 7, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, "Something forbidden" },
                    { 4, 2, 6.0, new DateTime(2016, 7, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 100, "Well" },
                    { 5, 3, 44.0, new DateTime(2016, 2, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 16203, "Bird in a cage" },
                    { 7, 3, 14.029999999999999, new DateTime(2016, 1, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), 1640, "Need for speed" },
                    { 10, 6, 1521.03, new DateTime(2003, 4, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), 11600, "Even coolest story" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
