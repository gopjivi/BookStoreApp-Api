using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using BookStoreApp.Api.Services.Interfaces;

namespace BookStoreApp.Api.Services
{
    public class BookService :IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> AddBookAsync(Book book)
        {
           return await _bookRepository.AddBookAsync(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            return await _bookRepository.UpdateBookAsync(book);
        }
    }
}
