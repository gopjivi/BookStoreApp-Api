using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthorsAsync();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all authors.");
                throw; // Allow the global error handler to handle the exception
            }
        }

        [HttpGet("CheckAuthorNameIsExists/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<bool>> CheckAuthorNameIsExists(string name)
        {
            try
            {
                bool isExists = await _authorService.CheckAuthorNameIsExists(name);
                return Ok(isExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if author name exists.");
                throw;
            }
        }

        [HttpGet("CheckAuthorNameIsExistsForUpdate/{id}/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<bool>> CheckAuthorNameIsExistsForUpdate(int id, string name)
        {
            try
            {
                bool isExists = await _authorService.CheckAuthorNameIsExistsForUpdate(id, name);
                return Ok(isExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if author name exists for update.");
                throw;
            }
        }

        [HttpGet("CheckAuthorExistsInBook/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<bool>> CheckAuthorExistsInBook(int id)
        {
            try
            {
                bool isExists = await _authorService.CheckAuthorExistsInBook(id);
                return Ok(isExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking if author exists in book.");
                throw;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Author>> GetAuthorById(int id)
        {
            try
            {
                var author = await _authorService.GetAuthorByIdAsync(id);
                if (author == null)
                {
                    _logger.LogWarning("Author with ID {Id} not found.", id);
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting author by ID {Id}.", id);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateAuthor.");
                return BadRequest(ModelState);
            }
            try
            {
                var createdAuthor = await _authorService.AddAuthorAsync(author);

                if (createdAuthor)
                {
                    _logger.LogInformation("Author {AuthorName} created successfully with ID {AuthorId}.", author.Name, author.AuthorID);
                    return CreatedAtAction(nameof(GetAuthorById), new { id = author.AuthorID }, author);
                }

                _logger.LogWarning("Failed to create author {AuthorName}.", author.Name);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new author.");
                throw;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            try
            {
                if (id != author.AuthorID)
                {
                    _logger.LogWarning("Author ID mismatch. Provided ID: {Id}, Author ID: {AuthorId}.", id, author.AuthorID);
                    return BadRequest();
                }

                var result = await _authorService.UpdateAuthorAsync(author);
                if (!result)
                {
                    _logger.LogWarning("Author with ID {Id} not found for update.", id);
                    return NotFound();
                }

                _logger.LogInformation("Author with ID {Id} updated successfully.", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating author with ID {Id}.", id);
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Author with ID {Id} not found for deletion.", id);
                    return NotFound();
                }

                _logger.LogInformation("Author with ID {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting author with ID {Id}.", id);
                throw;
            }
        }
    }
}
