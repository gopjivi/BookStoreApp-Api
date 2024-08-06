using BookStoreApp.Api.DbContexts;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookStoreDbContext _context;
        public AuthorRepository(BookStoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.OrderByDescending(c => c.AuthorID).ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }
        public async Task<bool> AddAuthorAsync(Author author)
        {
            _context.Authors.Add(author);
            return await SaveChangesAsync();
        }
        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                return await SaveChangesAsync();

            }
            return false;
        }

        public async Task<bool> CheckAuthorNameIsExists(string authorName)
        {
            var author = _context.Authors.Where(c => c.DisplayName == authorName).FirstOrDefault();
            if (author != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckAuthorExistsInBook(int authorID)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.AuthorID == authorID && a.Books.Count() > 0)
                .FirstOrDefaultAsync();

            return author != null;
        }


        public async Task<bool> CheckAuthorNameIsExistsForUpdate(int authorID, string authorName)
        {
            var author = _context.Authors.Where(c => c.DisplayName == authorName && c.AuthorID != authorID).FirstOrDefault();
            if (author != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
