using BookStoreApp.Api.DbContexts;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStoreApp.Api.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookStoreDbContext _context;
        public GenreRepository(BookStoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres.OrderByDescending(c => c.GenreID).ToListAsync();
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<bool> AddGenreAsync(Genre genre)
        {
            _context.Genres.Add(genre);
            return await SaveChangesAsync();

        }

        public async Task<bool> UpdateGenreAsync(Genre genre)
        {
             _context.Entry(genre).State = EntityState.Modified;
            return await SaveChangesAsync();

        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
                return await SaveChangesAsync();

            }
            return false;
        }

        public async Task<bool> CheckGenreNameIsExists(string genreName)
        {
            var genre =  _context.Genres.Where(c=>c.GenreName== genreName).FirstOrDefault();
            if (genre != null)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresWithBookCountAsync()
        {
            var result = await _context.Genres
                .GroupJoin(
                    _context.Books,
                    genre => genre.GenreID,
                    book => book.GenreID,
                    (genre, books) => new { Genre = genre, Books = books }
                )
                .Select(g => new Genre
                {
                    GenreID = g.Genre.GenreID,
                    GenreName = g.Genre.GenreName,
                    BookCount = g.Books.Count()
                })
                .ToListAsync();

            return result;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
