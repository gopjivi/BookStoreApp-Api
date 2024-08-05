using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using BookStoreApp.Api.Services.Interfaces;

namespace BookStoreApp.Api.Services
{
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<bool> AddGenreAsync(Genre genre)
        {
            return await _genreRepository.AddGenreAsync(genre);
        }

        public async Task<bool> CheckGenreNameIsExists(string genreName)
        {
            return await _genreRepository.CheckGenreNameIsExists(genreName);
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            return await _genreRepository.DeleteGenreAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _genreRepository.GetAllGenresAsync();
        }

        public async Task<IEnumerable<Genre>> GetAllGenresWithBookCountAsync()
        {
            return await _genreRepository.GetAllGenresWithBookCountAsync(); 
        }

        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _genreRepository.GetGenreByIdAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _genreRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateGenreAsync(Genre genre)
        {
            return await _genreRepository.UpdateGenreAsync(genre);
        }
    }
}
