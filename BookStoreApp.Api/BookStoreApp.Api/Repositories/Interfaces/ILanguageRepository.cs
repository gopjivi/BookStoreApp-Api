using BookStoreApp.Api.Models;

namespace BookStoreApp.Api.Repositories.Interfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
    }
}
