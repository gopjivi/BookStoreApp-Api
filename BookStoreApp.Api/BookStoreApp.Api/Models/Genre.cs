using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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
        public string GenreName { get; set; }

        [NotMapped]
        public int? BookCount { get; set; }

        [JsonIgnore]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();

    }

}
