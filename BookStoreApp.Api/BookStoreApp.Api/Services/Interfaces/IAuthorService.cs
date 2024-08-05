using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<bool> AddAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);

        Task<bool> CheckAuthorNameIsExists(string authorName);
        Task<bool> CheckAuthorNameIsExistsForUpdate(int authorID, string authorName);
        Task<bool> SaveChangesAsync();
    }
}
