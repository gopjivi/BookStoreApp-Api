using BookStoreApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace BookStoreApp.Api.DbContexts
{
    public class BookStoreDbContext:DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {

        }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>().HasData(
                new Language
                {
                    LanguageID=1,
                   LanguageName="Tamil"

                },
                new Language
                 {
                     LanguageID = 2,
                     LanguageName = "English"

                },
                new Language
                  {
                      LanguageID = 3,
                      LanguageName = "Kannada"

                },
                new Language
                   {
                       LanguageID = 4,
                       LanguageName = "Malayalam"

                },
                new Language
                    {
                        LanguageID = 5,
                        LanguageName = "Hindi"

                },
                new Language
                     {
                         LanguageID = 6,
                         LanguageName = "Telugu"

                },
                 new Language
                 {
                     LanguageID = 7,
                     LanguageName = "Urdu"

                 },
                 new Language
                  {
                      LanguageID = 8,
                      LanguageName = "Punjabi"

                 },
                  new Language
                   {
                       LanguageID = 9,
                       LanguageName = "Marathi"

                  },
                   new Language
                    {
                        LanguageID = 10,
                        LanguageName = "Bengali"

                   }

              );

            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    GenreID = 1,
                    GenreName = "Fantasy"

                }
              

              );
            modelBuilder.Entity<Author>().HasData(
               new Author
               {
                   AuthorID = 1,
                   Name = "Jane Austen",
                   DisplayName= "Jane Austen",
                   Biography= "Jane Austen was an English novelist known primarily for her six major novels..."

               }


             );
            modelBuilder.Entity<Book>().HasData(
              new Book
              {
                  BookID = 1,
                  Title = "Sample Book",
                  Price =100,
                  PublicationDate=Convert.ToDateTime("2005-04-23"),
                  Edition = "",
                  StockQuantity=50,
                  StorageSection="Tamil-Fiction",
                  StorageShelf="",
                  IsOfferAvailable=false,
                  AuthorID=1,
                  GenreID=1,    
                  LanguageID=1

              }


            );

        }
    }
}
