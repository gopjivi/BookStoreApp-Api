using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Newtonsoft.Json;

namespace BookStoreApp.Api.Models
{
    [Index(nameof(LanguageName), IsUnique = true)]
    public class Language
    {/// <summary>
    /// Language id
    /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("language_id")]
        public int LanguageID { get; set; }
        /// <summary>
        /// Language name
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("language_name")]
        public string LanguageName { get; set; }
        [JsonIgnore]
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
