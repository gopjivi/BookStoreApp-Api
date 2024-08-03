using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace BookStoreApp.Api.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("book_id")]
        public int BookID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("title")]
        public required string Title { get; set; } = string.Empty;

        [Required]
        [Column("price")]
        public required float Price { get; set; }

        [Required]
        [Column("publication_date")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [Column("edition")]
        public string Edition { get; set; } = string.Empty;

        [Required]
        [Column("stock_quantity")]
        public int StockQuantity { get; set; } = 0;

        [Column("book_image")]
        public byte[]? BookImage { get; set; }

        [Required]
        [Column("storage_section")]
        public required string StorageSection { get; set; } = string.Empty;


        [Column("storage_shelf")]
        public string? StorageShelf { get; set; } = string.Empty;

        [Required]
        [Column("is_offer_available")]
        public bool IsOfferAvailable { get; set; }

        [Column("language_id")]
        public int LanguageID { get; set; }

        [ForeignKey("LanguageID")]
        public Language Language { get; set; }

        [Column("genre_id")]
        public int GenreID { get; set; }

        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }

        [Column("author_id")]
        public int AuthorID { get; set; }


        [ForeignKey("AuthorID")]
        public Author Author { get; set; }



    }
}
