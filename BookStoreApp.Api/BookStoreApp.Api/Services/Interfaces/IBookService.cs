using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);


        Task<bool> SaveChangesAsync();
    }
}
