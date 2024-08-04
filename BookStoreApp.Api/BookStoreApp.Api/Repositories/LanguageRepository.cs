using BookStoreApp.Api.DbContexts;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Api.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BookStoreDbContext _context;
        public LanguageRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            return await _context.Languages.ToListAsync();
        }
    }
}

