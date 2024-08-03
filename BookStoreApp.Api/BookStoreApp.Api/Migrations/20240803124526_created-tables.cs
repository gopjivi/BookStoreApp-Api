using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class createdtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LanguageName",
                table: "Languages",
                newName: "language_name");

            migrationBuilder.RenameColumn(
                name: "LanguageID",
                table: "Languages",
                newName: "language_id");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_LanguageName",
                table: "Languages",
                newName: "IX_Languages_language_name");

            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    biography = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genre_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    publication_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    edition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stock_quantity = table.Column<int>(type: "int", nullable: false),
                    book_image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    storage_section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    storage_shelf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_offer_available = table.Column<bool>(type: "bit", nullable: false),
                    language_id = table.Column<int>(type: "int", nullable: false),
                    genre_id = table.Column<int>(type: "int", nullable: false),
                    author_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_Book_Author_author_id",
                        column: x => x.author_id,
                        principalTable: "Author",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Genres_genre_id",
                        column: x => x.genre_id,
                        principalTable: "Genres",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Book_Languages_language_id",
                        column: x => x.language_id,
                        principalTable: "Languages",
                        principalColumn: "language_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "genre_id", "genre_name" },
                values: new object[] { 1, "Fantasy" });

            migrationBuilder.CreateIndex(
                name: "IX_Author_display_name",
                table: "Author",
                column: "display_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_author_id",
                table: "Book",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_genre_id",
                table: "Book",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_Book_language_id",
                table: "Book",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "IX_Genres_genre_name",
                table: "Genres",
                column: "genre_name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.RenameColumn(
                name: "language_name",
                table: "Languages",
                newName: "LanguageName");

            migrationBuilder.RenameColumn(
                name: "language_id",
                table: "Languages",
                newName: "LanguageID");

            migrationBuilder.RenameIndex(
                name: "IX_Languages_language_name",
                table: "Languages",
                newName: "IX_Languages_LanguageName");
        }
    }
}
