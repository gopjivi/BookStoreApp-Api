using BookStoreApp.Api.Models;
using BookStoreApp.Api.Repositories.Interfaces;
using BookStoreApp.Api.Services.Interfaces;

namespace BookStoreApp.Api.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
         return  await  _authorRepository.AddAuthorAsync(author);
        }

        public async Task<bool> CheckAuthorNameIsExists(string authorName)
        {
            return await _authorRepository.CheckAuthorNameIsExists(authorName);
        }

        public async Task<bool> CheckAuthorNameIsExistsForUpdate(int authorID, string authorName)
        {
            return await _authorRepository.CheckAuthorNameIsExistsForUpdate(authorID,authorName);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _authorRepository.DeleteAuthorAsync(id);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAuthorsAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetAuthorByIdAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _authorRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            return await _authorRepository.UpdateAuthorAsync(author);
        }
    }
}
