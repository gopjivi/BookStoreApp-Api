using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class tableupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_author_id",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Genres_genre_id",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Languages_language_id",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Author",
                table: "Author");

            migrationBuilder.RenameTable(
                name: "Book",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "Author",
                newName: "Authors");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "Books",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Book_language_id",
                table: "Books",
                newName: "IX_Books_language_id");

            migrationBuilder.RenameIndex(
                name: "IX_Book_genre_id",
                table: "Books",
                newName: "IX_Books_genre_id");

            migrationBuilder.RenameIndex(
                name: "IX_Book_author_id",
                table: "Books",
                newName: "IX_Books_AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Author_display_name",
                table: "Authors",
                newName: "IX_Authors_display_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_genre_id",
                table: "Books",
                column: "genre_id",
                principalTable: "Genres",
                principalColumn: "genre_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_language_id",
                table: "Books",
                column: "language_id",
                principalTable: "Languages",
                principalColumn: "language_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_genre_id",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_language_id",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "Book");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Book",
                newName: "author_id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_language_id",
                table: "Book",
                newName: "IX_Book_language_id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_genre_id",
                table: "Book",
                newName: "IX_Book_genre_id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorID",
                table: "Book",
                newName: "IX_Book_author_id");

            migrationBuilder.RenameIndex(
                name: "IX_Authors_display_name",
                table: "Author",
                newName: "IX_Author_display_name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                column: "book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Author",
                table: "Author",
                column: "author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_author_id",
                table: "Book",
                column: "author_id",
                principalTable: "Author",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Genres_genre_id",
                table: "Book",
                column: "genre_id",
                principalTable: "Genres",
                principalColumn: "genre_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Languages_language_id",
                table: "Book",
                column: "language_id",
                principalTable: "Languages",
                principalColumn: "language_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
