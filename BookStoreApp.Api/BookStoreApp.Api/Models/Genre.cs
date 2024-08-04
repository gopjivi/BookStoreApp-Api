using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.Api.Models
{

    [Index(nameof(GenreName), IsUnique = true)]
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("genre_id")]
        public int GenreID { get; set; }

        [Required]
        [StringLength(100)]
        [Column("genre_name")]
        public required string GenreName { get; set; }

        [NotMapped]
        public int BookCount { get; set; }

        public IEnumerable<Book> Books { get; set; } = new List<Book>();

    }

}
