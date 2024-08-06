using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();

            string json = JsonConvert.SerializeObject(books);
            
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)

        {
            
       
            var createdBook = await _bookService.AddBookAsync(book);

            if (createdBook)
            {
                return CreatedAtAction(nameof(GetBookById), new { id = book.BookID }, book);
            }

            return BadRequest();
        }

        // PUT: api/books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.BookID)
            {
                return BadRequest();
            }

            var result = await _bookService.UpdateBookAsync(book);
            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // DELETE: api/books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
