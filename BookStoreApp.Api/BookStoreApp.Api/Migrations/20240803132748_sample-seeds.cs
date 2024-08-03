using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class sampleseeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "author_id", "biography", "display_name", "name" },
                values: new object[] { 1, "Jane Austen was an English novelist known primarily for her six major novels...", "Jane Austen", "Jane Austen" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "book_id", "author_id", "book_image", "edition", "genre_id", "is_offer_available", "language_id", "price", "publication_date", "stock_quantity", "storage_section", "storage_shelf", "title" },
                values: new object[] { 1, 1, null, "", 1, false, 1, 100f, new DateTime(2005, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, "Tamil-Fiction", "", "Sample Book" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "author_id",
                keyValue: 1);
        }
    }
}
