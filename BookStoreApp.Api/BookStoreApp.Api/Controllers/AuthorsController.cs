using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            return Ok(authors);
        }


        [HttpGet("CheckAuthorNameIsExists/{name}")]
        public async Task<ActionResult<bool>> CheckAuthorNameIsExists(string name)
        {

            bool isExists = await _authorService.CheckAuthorNameIsExists(name);
            return Ok(isExists);

        }

        [HttpGet("CheckAuthorNameIsExistsForUpdate/{id}/{name}")]
        public async Task<ActionResult<bool>> CheckAuthorNameIsExistsForUpdate(int id,string name)
        {

            bool isExists = await _authorService.CheckAuthorNameIsExistsForUpdate(id,name);
            return Ok(isExists);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            var createdAurthor = await _authorService.AddAuthorAsync(author);

            if (createdAurthor)
            {
                return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorID }, author);
            }

            return BadRequest();
        }

        // PUT: api/authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.AuthorID)
            {
                return BadRequest();
            }

            var result = await _authorService.UpdateAuthorAsync(author);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthorAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
