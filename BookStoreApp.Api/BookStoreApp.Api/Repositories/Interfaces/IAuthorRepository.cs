using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<bool> AddAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int id);

        Task<bool> CheckAuthorNameIsExists(string authorName);
        Task<bool> CheckAuthorNameIsExistsForUpdate(int authorID,string authorName);

        Task<bool> CheckAuthorExistsInBook(int authorID);
        Task<bool> SaveChangesAsync();
    }
}
