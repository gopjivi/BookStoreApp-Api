using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Humanizer.Localisation;
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
        /// <summary>
        /// Api to get all the Authors
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            return Ok(authors);
        }

        /// <summary>
        /// Api to check author name exists or not
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return true if name exists</returns>
        [HttpGet("CheckAuthorNameIsExists/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CheckAuthorNameIsExists(string name)
        {

            bool isExists = await _authorService.CheckAuthorNameIsExists(name);
            return Ok(isExists);

        }

        /// <summary>
        /// Api to check auhtor name exists or not while updating author details
        /// </summary>
        /// <param name="id">updating author id</param>
        /// <param name="name"> new author name</param>
        /// <returns>return true if name exists</returns>
        [HttpGet("CheckAuthorNameIsExistsForUpdate/{id}/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CheckAuthorNameIsExistsForUpdate(int id,string name)
        {

            bool isExists = await _authorService.CheckAuthorNameIsExistsForUpdate(id,name);
            return Ok(isExists);

        }

        /// <summary>
        /// Api to check paricular author mapped in book
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return true if it mapped</returns>
        [HttpGet("CheckAuthorExistsInBook/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> CheckAuthorExistsInBook(int id)
        {

            bool isExists = await _authorService.CheckAuthorExistsInBook(id);
            return Ok(isExists);

        }

        /// <summary>
        /// Api to get particular auhtor by autthorID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        /// <summary>
        /// Api to create new auhtor
        /// </summary>
        /// <param name="author"></param>
        /// <returns>return new author with id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            var createdAurthor = await _authorService.AddAuthorAsync(author);

            if (createdAurthor)
            {
                return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorID }, author);
            }

            return BadRequest();
        }

        /// <summary>
        /// Api to update particular author detail
        /// </summary>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        // PUT: api/authors/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            return Ok(result);
        }

        /// <summary>
        /// Api to delete particular Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/authors/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
