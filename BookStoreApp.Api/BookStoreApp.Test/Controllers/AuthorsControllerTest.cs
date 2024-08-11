using BookStoreApp.Api.Controllers;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using FluentAssertions;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreApp.Test.Controllers
{

    public class AuthorsControllerTest
    {

        private readonly Mock<IAuthorService> _authorService;
        private readonly Mock<ILogger<AuthorsController>> _logger;
        private readonly AuthorsController _authorsController;

        public AuthorsControllerTest()
        { 
             //setup dependencies
            _authorService = new Mock<IAuthorService>();
            _logger = new Mock<ILogger<AuthorsController>>();
            _authorsController = new AuthorsController(_authorService.Object, _logger.Object);
        }

        private readonly List<Author> authors = new List<Author>()
{
    new Author { AuthorID = 1, Name = "John Doe", DisplayName = "John Doe", Biography = "Biography of Author One" },
    new Author { AuthorID = 2, Name = "Jane Smith", DisplayName = "Jane Smith", Biography = "Biography of Author Two" }
};

        [Fact]
        public async Task GetAllAuthors_ReturnsOk_OnSuccess()
        {
            // Arrange
            var mockAuthors = authors;


            _authorService.Setup(service => service.GetAllAuthorsAsync())
                .ReturnsAsync(mockAuthors);

            // Act
            var result = await _authorsController.GetAllAuthors();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
           
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().BeEquivalentTo(mockAuthors);
        }

        [Fact]
        public async Task GetAllAuthors_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var exceptionMessage = "Database error";
            _authorService.Setup(service => service.GetAllAuthorsAsync())
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _authorsController.GetAllAuthors();

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error"); 
        }

        [Fact]
        public async Task CheckAuthorNameIsExists_ReturnsOk_WithTrue_WhenAuthorExists()
        {
            // Arrange
            string authorName = "John Doe";
          
            _authorService.Setup(service => service.CheckAuthorNameIsExists(authorName))
                          .ReturnsAsync(true);

            // Act
            var result = await _authorsController.CheckAuthorNameIsExists(authorName);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var exists = Assert.IsType<bool>(actionResult.Value);

            actionResult.StatusCode.Should().Be(200);
            exists.Should().Be(true);
        }

        [Fact]
        public async Task CheckAuthorNameIsExists_ReturnsOk_WithFalse_WhenAuthorDoesNotExist()
        {
            // Arrange
            string authorName = "Jane Smith";

            // Set up the mock to return false when the author name does not exist
            _authorService.Setup(service => service.CheckAuthorNameIsExists(authorName))
                          .ReturnsAsync(false);

            // Act
            var result = await _authorsController.CheckAuthorNameIsExists(authorName);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var exists = Assert.IsType<bool>(actionResult.Value);

            actionResult.StatusCode.Should().Be(200);
            exists.Should().Be(false);
        }

        [Fact]
        public async Task CheckAuthorNameIsExists_ReturnsInternalServerError_OnException()
        {
            // Arrange
            string authorName = "John Doe";
            var exceptionMessage = "Database error";
            _authorService.Setup(service => service.CheckAuthorNameIsExists(authorName))
                          .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _authorsController.CheckAuthorNameIsExists(authorName);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task CheckAuthorNameIsExistsForUpdate_ReturnsOk_WithTrue_WhenAuthorNameExists()
        {
            // Arrange
            int authorId = 1;
            string authorName = "John Doe";
            _authorService.Setup(service => service.CheckAuthorNameIsExistsForUpdate(authorId, authorName))
                          .ReturnsAsync(true);

            // Act
            var result = await _authorsController.CheckAuthorNameIsExistsForUpdate(authorId, authorName);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var exists = Assert.IsType<bool>(actionResult.Value);

            actionResult.StatusCode.Should().Be(200);
            exists.Should().Be(true);
        }

        [Fact]
        public async Task CheckAuthorNameIsExistsForUpdate_ReturnsOk_WithFalse_WhenAuthorNameDoesNotExist()
        {
            // Arrange
            int authorId = 2;
            string authorName = "Jane Smith";
            _authorService.Setup(service => service.CheckAuthorNameIsExistsForUpdate(authorId, authorName))
                          .ReturnsAsync(false);

            // Act
            var result = await _authorsController.CheckAuthorNameIsExistsForUpdate(authorId, authorName);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var exists = Assert.IsType<bool>(actionResult.Value);

            actionResult.StatusCode.Should().Be(200);
            exists.Should().Be(false);
        }


        [Fact]
        public async Task CheckAuthorNameIsExistsForUpdate_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int authorId = 3;
            string authorName = "Faulty Author";
            _authorService.Setup(service => service.CheckAuthorNameIsExistsForUpdate(authorId, authorName))
                          .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _authorsController.CheckAuthorNameIsExistsForUpdate(authorId, authorName);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task CheckAuthorExistsInBook_ReturnsOk_WithTrue_WhenAuthorExists()
        {
            // Arrange
            int authorId = 1;
            _authorService.Setup(service => service.CheckAuthorExistsInBook(authorId))
                          .ReturnsAsync(true);

            // Act
            var result = await _authorsController.CheckAuthorExistsInBook(authorId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var exists = Assert.IsType<bool>(actionResult.Value);

            actionResult.StatusCode.Should().Be(200);
            exists.Should().Be(true);
        }

        [Fact]
        public async Task CheckAuthorExistsInBook_ReturnsOk_WithFalse_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 2;
            _authorService.Setup(service => service.CheckAuthorExistsInBook(authorId))
                          .ReturnsAsync(false);

            // Act
            var result = await _authorsController.CheckAuthorExistsInBook(authorId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var exists = Assert.IsType<bool>(actionResult.Value);

            actionResult.StatusCode.Should().Be(200);
            exists.Should().Be(false);
        }

        [Fact]
        public async Task CheckAuthorExistsInBook_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int authorId = 3;
            _authorService.Setup(service => service.CheckAuthorExistsInBook(authorId))
                          .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _authorsController.CheckAuthorExistsInBook(authorId);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task GetAuthorById_ReturnsOk_OnSuccess()
        {
            // Arrange
            int authorId = 1;
            Author mockAuthor = authors[0];

            _authorService.Setup(service => service.GetAuthorByIdAsync(authorId))
                          .ReturnsAsync(mockAuthor);

            // Act
            var result = await _authorsController.GetAuthorById(authorId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
           

            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().Be(mockAuthor);
        }

        [Fact]
        public async Task GetAuthorById_ReturnsNotFound_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;

            _authorService.Setup(service => service.GetAuthorByIdAsync(authorId))
                          .ReturnsAsync((Author)null);

            // Act
            var result = await _authorsController.GetAuthorById(authorId);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result.Result);

            actionResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetAuthorById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int authorId = 1;

            _authorService.Setup(service => service.GetAuthorByIdAsync(authorId))
                          .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _authorsController.GetAuthorById(authorId);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);

            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task CreateAuthor_ReturnCreated_OnSuccess()
        {
            //Arrange
           Author author = authors[0];
            _authorService.Setup(service => service.AddAuthorAsync(author)).ReturnsAsync(true);


            //Act
            var result = await _authorsController.CreateAuthor(author);

            //Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);


            actionResult.StatusCode.Should().Be(201);
            actionResult.Should().NotBeNull();

        }

        [Fact]
        public async Task CreateAuthor_ReturnsBadRequest_OnModelStateFailure()
        {
            // Arrange
            Author author = authors[0];
            _authorsController.ModelState.AddModelError("Name", "Required"); 

            // Act
            var result = await _authorsController.CreateAuthor(author);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(400);
            actionResult.Value.Should().BeOfType<SerializableError>(); 
        }

        [Fact]
        public async Task CreateAuthor_ReturnsBadRequest_OnFailure()
        {
            // Arrange
            Author author = authors[0];
            _authorService.Setup(service => service.AddAuthorAsync(author)).ReturnsAsync(false);

            // Act
            var result = await _authorsController.CreateAuthor(author);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result.Result);
            actionResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreateGenre_ReturnInternalServerError_OnException()
        {
            // Arrange
            Author author = authors[0];
            _authorService.Setup(service => service.AddAuthorAsync(It.IsAny<Author>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _authorsController.CreateAuthor(author);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsOk_OnSuccess()
        {
            // Arrange
            int authorId = 1;
            var authorToUpdate = new Author { AuthorID = authorId, Name = "Updated Name" };
            _authorService.Setup(service => service.UpdateAuthorAsync(authorToUpdate)).ReturnsAsync(true);

            // Act
            var result = await _authorsController.UpdateAuthor(authorId, authorToUpdate);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            int authorId = 1;
            var invalidAuthor = new Author { AuthorID = authorId };
            _authorsController.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await _authorsController.UpdateAuthor(authorId, invalidAuthor);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result);
            actionResult.StatusCode.Should().Be(400);
            actionResult.Value.Should().BeOfType<SerializableError>(); 
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            int providedId = 1;
            int authorId = 2; // Mismatched ID
            var authorToUpdate = new Author { AuthorID = authorId };

            // Act
            var result = await _authorsController.UpdateAuthor(providedId, authorToUpdate);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result);
            actionResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsNotFound_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;
            var authorToUpdate = new Author { AuthorID = authorId };
            _authorService.Setup(service => service.UpdateAuthorAsync(authorToUpdate)).ReturnsAsync(false);

            // Act
            var result = await _authorsController.UpdateAuthor(authorId, authorToUpdate);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            actionResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateAuthor_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int authorId = 1;
            var authorToUpdate = new Author { AuthorID = authorId };
            _authorService.Setup(service => service.UpdateAuthorAsync(authorToUpdate))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _authorsController.UpdateAuthor(authorId, authorToUpdate);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task DeleteAuthor_ReturnsNoContent_OnSuccess()
        {
            // Arrange
            int authorId = 1;
            _authorService.Setup(service => service.DeleteAuthorAsync(authorId)).ReturnsAsync(true);

            // Act
            var result = await _authorsController.DeleteAuthor(authorId);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
            actionResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task DeleteAuthor_ReturnsNotFound_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;
            _authorService.Setup(service => service.DeleteAuthorAsync(authorId)).ReturnsAsync(false);

            // Act
            var result = await _authorsController.DeleteAuthor(authorId);

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
            actionResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DeleteAuthor_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int authorId = 1;
            var exceptionMessage = "Database error";
            _authorService.Setup(service => service.DeleteAuthorAsync(authorId))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _authorsController.DeleteAuthor(authorId);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }








    }
}
