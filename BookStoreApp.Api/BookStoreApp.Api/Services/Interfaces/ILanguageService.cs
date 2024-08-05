using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
    }
}
