using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using BookStoreApp.Api.Services.Interfaces;

namespace BookStoreApp.Api.Services
{
    public class LanguageService:ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
           return await _languageRepository.GetAllLanguagesAsync();
        }
    }
}
