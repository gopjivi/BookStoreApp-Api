using BookStoreApp.Api.DbContexts;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories
{
    public class BookRepository :IBookRepository
    {
        private readonly BookStoreDbContext _context;
        public BookRepository(BookStoreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.OrderByDescending(c => c.BookID).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            return await SaveChangesAsync();

        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            return await SaveChangesAsync();

        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                return await SaveChangesAsync();

            }
            return false;
        }


        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
   
