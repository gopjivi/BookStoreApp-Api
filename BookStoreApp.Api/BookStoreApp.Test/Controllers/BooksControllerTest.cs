using BookStoreApp.Api.Controllers;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Test.Controllers
{
    public class BooksControllerTest
    {
        private readonly Mock<IBookService> _bookService;
        private readonly Mock<ILogger<BooksController>> _logger;
        private readonly BooksController _booksController;

        public BooksControllerTest()
        {
            //setup dependencies
            _bookService = new Mock<IBookService>();
            _logger = new Mock<ILogger<BooksController>>();
            _booksController = new BooksController(_bookService.Object, _logger.Object);
        }

        private readonly List<Book> books = new List<Book>()
    {
        new Book { BookID = 1, Title = "Book One", Price = 10.99f, PublicationDate = DateTime.Now, Edition = "1st", StockQuantity = 10, StorageSection = "A1", IsOfferAvailable = false, LanguageID = 1, GenreID = 1, AuthorID = 1 },
        new Book { BookID = 2, Title = "Book Two", Price = 15.99f, PublicationDate = DateTime.Now, Edition = "2nd", StockQuantity = 5, StorageSection = "B1", IsOfferAvailable = true, LanguageID = 2, GenreID = 2, AuthorID = 2 }
    };

        [Fact]
        public async Task GetAllBooks_ReturnsOk_OnSuccess()
        {
            // Arrange
            var mockBooks = books;


            _bookService.Setup(service => service.GetAllBooksAsync())
                .ReturnsAsync(mockBooks);

            // Act
            var result = await _booksController.GetAllBooks();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);

            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().BeEquivalentTo(mockBooks);
        }

        [Fact]
        public async Task GetAllBooks_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var exceptionMessage = "Database error";
            _bookService.Setup(service => service.GetAllBooksAsync())
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _booksController.GetAllBooks();

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task GetBookById_ReturnsOk_OnSuccess()
        {
            // Arrange
            int bookId = 1;
            Book mockBook = books[0];
           _bookService.Setup(service => service.GetBookByIdAsync(bookId))
                        .ReturnsAsync(mockBook);

            // Act
            var result = await _booksController.GetBookById(bookId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().BeEquivalentTo(mockBook);
        }

        [Fact]
        public async Task GetBookById_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;
            _bookService.Setup(service => service.GetBookByIdAsync(bookId))
                        .ReturnsAsync((Book)null);

            // Act
            var result = await _booksController.GetBookById(bookId);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);
            actionResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBookById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int bookId = 1;
            var exceptionMessage = "Database error";
            _bookService.Setup(service => service.GetBookByIdAsync(bookId))
                        .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _booksController.GetBookById(bookId);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task CreateBook_ReturnCreated_OnSuccess()
        {
            //Arrange
            Book book = books[0];
            _bookService.Setup(service => service.AddBookAsync(book)).ReturnsAsync(true);


            //Act
            var result = await _booksController.CreateBook(book);

            //Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);


            actionResult.StatusCode.Should().Be(201);
            actionResult.Should().NotBeNull();

        }

        [Fact]
        public async Task CreateBook_ReturnsBadRequest_OnModelStateFailure()
        {
            // Arrange
            Book book = books[0];
            _booksController.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await _booksController.CreateBook(book);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(400);
            actionResult.Value.Should().BeOfType<SerializableError>();
        }

        [Fact]
        public async Task CreateBook_ReturnsBadRequest_OnFailure()
        {
            // Arrange
            Book book = books[0];
            _bookService.Setup(service => service.AddBookAsync(book)).ReturnsAsync(false);

            // Act
            var result = await _booksController.CreateBook(book);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result.Result);
            actionResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreateBook_ReturnInternalServerError_OnException()
        {
            // Arrange
            Book book = books[0];
            _bookService.Setup(service => service.AddBookAsync(It.IsAny<Book>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _booksController.CreateBook(book);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task UpdateBook_ReturnsOk_OnSuccess()
        {
            // Arrange
            int bookId = 1;
            var bookToUpdate = new Book { BookID = bookId, Title = "Updated Title" };
            _bookService.Setup(service => service.UpdateBookAsync(bookToUpdate)).ReturnsAsync(true);

            // Act
            var result = await _booksController.UpdateBook(bookId, bookToUpdate);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task UpdateBook_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            int bookId = 1;
            var invalidBook = new Book { BookID = bookId };
            _booksController.ModelState.AddModelError("Title", "Required");

            // Act
            var result = await _booksController.UpdateBook(bookId, invalidBook);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            actionResult.StatusCode.Should().Be(400);
            actionResult.Value.Should().BeOfType<SerializableError>();
        }

        [Fact]
        public async Task UpdateBook_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            int providedId = 1;
            int bookId = 2; // Mismatched ID
            var bookToUpdate = new Book { BookID = bookId };

            // Act
            var result = await _booksController.UpdateBook(providedId, bookToUpdate);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
            actionResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;
            var bookToUpdate = new Book { BookID = bookId };
            _bookService.Setup(service => service.UpdateBookAsync(bookToUpdate)).ReturnsAsync(false);

            // Act
            var result = await _booksController.UpdateBook(bookId, bookToUpdate);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            actionResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateBook_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int bookId = 1;
            var bookToUpdate = new Book { BookID = bookId };
            _bookService.Setup(service => service.UpdateBookAsync(bookToUpdate))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _booksController.UpdateBook(bookId, bookToUpdate);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task DeleteBook_ReturnsNoContent_OnSuccess()
        {
            // Arrange
            int bookId = 1;
            _bookService.Setup(service => service.DeleteBookAsync(bookId)).ReturnsAsync(true);

            // Act
            var result = await _booksController.DeleteBook(bookId);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            actionResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteBook_ReturnsNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;
            _bookService.Setup(service => service.DeleteBookAsync(bookId)).ReturnsAsync(false);

            // Act
            var result = await _booksController.DeleteBook(bookId);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            actionResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DeleteBook_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int bookId = 1;
            var exceptionMessage = "Database error";
            _bookService.Setup(service => service.DeleteBookAsync(bookId))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _booksController.DeleteBook(bookId);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }




    }
}
