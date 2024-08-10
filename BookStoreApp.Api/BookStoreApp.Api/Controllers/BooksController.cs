using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBookService bookService, ILogger<BooksController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        /// <summary>
        /// Api to get all the books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            try
            {
                _logger.LogInformation("Fetching all books.");

                var books = await _bookService.GetAllBooksAsync();

                _logger.LogInformation("Successfully fetched all books.");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all books.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Api to get the particular book by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching book with ID {id}.");

                var book = await _bookService.GetBookByIdAsync(id);

                if (book == null)
                {
                    _logger.LogWarning($"Book with ID {id} not found.");
                    return NotFound();
                }

                _logger.LogInformation($"Successfully fetched book with ID {id}.");
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching book with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Api to create a new book
        /// </summary>
        /// <param name="book">book details to create</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            try
            {
                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for book creation. Book title: {BookTitle}.", book.Title);
                    return BadRequest(ModelState);
                }

                _logger.LogInformation("Creating a new book.");

                var createdBook = await _bookService.AddBookAsync(book);

                if (createdBook)
                {
                    _logger.LogInformation($"Successfully created book with ID {book.BookID}.");
                    return CreatedAtAction(nameof(GetBookById), new { id = book.BookID }, book);
                }

                _logger.LogWarning("Failed to create a new book.");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new book.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Api to update a particular book
        /// </summary>
        /// <param name="id">BookID to update</param>
        /// <param name="book">updated book details</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            try
            {
                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for book update. Book title: {BookTitle}.", book.Title);
                    return BadRequest(ModelState);
                }

                if (id != book.BookID)
                {
                    _logger.LogWarning($"Book ID in URL ({id}) does not match ID in body ({book.BookID}).");
                    return BadRequest();
                }

                _logger.LogInformation($"Updating book with ID {id}.");

                var result = await _bookService.UpdateBookAsync(book);
                if (!result)
                {
                    _logger.LogWarning($"Book with ID {id} not found for update.");
                    return NotFound();
                }

                _logger.LogInformation($"Successfully updated book with ID {id}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating book with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Api to delete a particular book
        /// </summary>
        /// <param name="id">BookID to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting book with ID {id}.");

                var result = await _bookService.DeleteBookAsync(id);
                if (!result)
                {
                    _logger.LogWarning($"Book with ID {id} not found for deletion.");
                    return NotFound();
                }

                _logger.LogInformation($"Successfully deleted book with ID {id}.");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting book with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
