using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> GetGenreByIdAsync(int id);
        Task<bool> AddGenreAsync(Genre genre);
        Task<bool> UpdateGenreAsync(Genre genre);
        Task<bool> DeleteGenreAsync(int id);

        Task<bool> CheckGenreNameIsExists(string genreName);
        Task<IEnumerable<Genre>> GetAllGenresWithBookCountAsync();
        Task<bool> SaveChangesAsync();
    }
}
