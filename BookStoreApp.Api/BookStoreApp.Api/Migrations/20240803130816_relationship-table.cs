using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class relationshiptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Books",
                newName: "author_id");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                newName: "IX_Books_author_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_author_id",
                table: "Books",
                column: "author_id",
                principalTable: "Authors",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_author_id",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "author_id",
                table: "Books",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_author_id",
                table: "Books",
                newName: "IX_Books_AuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "author_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
