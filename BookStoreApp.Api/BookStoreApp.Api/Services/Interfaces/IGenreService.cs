using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Services.Interfaces
{
    public interface IGenreService
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
