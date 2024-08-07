using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookStoreApp.Api.Models
{
    /// <summary>
    /// Genres Table
    /// </summary>
    [Index(nameof(GenreName), IsUnique = true)]
    public class Genre
    {

        /// <summary>
        /// Auto genreated GenreID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("genre_id")]
        public int GenreID { get; set; }

        /// <summary>
        /// Genre Name
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("genre_name")]
        public string GenreName { get; set; }

        [NotMapped]
        public int? BookCount { get; set; }

        [JsonIgnore]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

    }

}
