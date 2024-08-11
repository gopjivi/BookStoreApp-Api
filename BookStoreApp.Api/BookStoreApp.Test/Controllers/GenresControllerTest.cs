using BookStoreApp.Api.Controllers;
using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services.Interfaces;
using FluentAssertions;
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
    public class GenresControllerTest
    {
        private readonly Mock<IGenreService> _genreService;
        private readonly Mock<ILogger<GenresController>> _logger;
        private readonly GenresController _genresController;
        public GenresControllerTest()
        {
            //setup dependencies
            _genreService = new Mock<IGenreService>();
            _logger = new Mock<ILogger<GenresController>>();
            _genresController = new GenresController(_genreService.Object, _logger.Object);
        }

        private List<Genre> genres = new List<Genre>()
          {
                new Genre { GenreID = 1, GenreName = "Fiction", BookCount = 10  },
                new Genre { GenreID = 2, GenreName = "Non-Fiction", BookCount = 5  }
         };


        [Fact]
        public async Task CreateGenre_ReturnCreated_OnSuccess()
        {
            //Arrange
            Genre genre = genres[0];
            _genreService.Setup(service => service.AddGenreAsync(genre)).ReturnsAsync(true);


            //Act
            var result = await _genresController.CreateGenre(genre);

            //Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);


            actionResult.StatusCode.Should().Be(201);
            actionResult.Should().NotBeNull();

        }


        [Fact]
        public async Task CreateGenre_ReturnsBadRequest_OnModelStateFailure()
        {
            // Arrange
            Genre genre = genres[0];
            _genresController.ModelState.AddModelError("GenreName", "Required"); 

            // Act
            var result = await _genresController.CreateGenre(genre);

            // Assert
            var actionResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(400);
            actionResult.Value.Should().BeOfType<SerializableError>(); 
        }

        [Fact]
        public async Task CreateGenre_ReturnBadRequest_OnFailure()
        {
            // Arrange
            Genre genre = genres[0];
            _genreService.Setup(service => service.AddGenreAsync(genre)).ReturnsAsync(false);

            // Act
            var result = await _genresController.CreateGenre(genre);

            // Assert
            var actionResult = Assert.IsType<BadRequestResult>(result.Result);
            actionResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CreateGenre_ReturnInternalServerError_OnException()
        {
            // Arrange
            Genre genre = genres[0];
            _genreService.Setup(service => service.AddGenreAsync(It.IsAny<Genre>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _genresController.CreateGenre(genre);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task GetAllGenres_ReturnsOk_OnSuccess()
        {
            // Arrange
            var mockGenres = genres;

            _genreService.Setup(service => service.GetAllGenresAsync()).ReturnsAsync(mockGenres);

            // Act
            var result = await _genresController.GetAllGenres();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);

            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().BeEquivalentTo(mockGenres);
        }

        [Fact]
        public async Task GetAllGenres_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _genreService.Setup(service => service.GetAllGenresAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _genresController.GetAllGenres();

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task GetAllGenresWithBookCount_ReturnsOk_OnSuccess()
        {
            // Arrange
            var mockGenres = genres;
            _genreService.Setup(service => service.GetAllGenresWithBookCountAsync()).ReturnsAsync(mockGenres);

            // Act
            var result = await _genresController.GetAllGenresWithBookCount();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().BeEquivalentTo(mockGenres);
        }

        [Fact]
        public async Task GetAllGenresWithBookCount_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _genreService.Setup(service => service.GetAllGenresWithBookCountAsync()).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _genresController.GetAllGenresWithBookCount();

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task CheckGenreNameIsExists_ReturnsOk_WithTrue_WhenGenreExists()
        {
            // Arrange
            string genreName = "Fiction";
            _genreService.Setup(service => service.CheckGenreNameIsExists(genreName)).ReturnsAsync(true);

            // Act
            var result = await _genresController.CheckGenreNameIsExists(genreName);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task CheckGenreNameIsExists_ReturnsOk_WithFalse_WhenGenreDoesNotExists()
        {
            // Arrange
            string genreName = "NonExistingGenre";
            _genreService.Setup(service => service.CheckGenreNameIsExists(genreName)).ReturnsAsync(false);

            // Act
            var result = await _genresController.CheckGenreNameIsExists(genreName);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().Be(false);
        }

        [Fact]
        public async Task CheckGenreNameIsExists_ReturnsInternalServerError_OnException()
        {
            // Arrange
            string genreName = "Fiction";
            _genreService.Setup(service => service.CheckGenreNameIsExists(genreName)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _genresController.CheckGenreNameIsExists(genreName);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

        [Fact]
        public async Task GetGenreById_ReturnsOk_OnSuccess()
        {
            // Arrange
            int genreId = 1;
            Genre genre = genres[0];
            _genreService.Setup(service => service.GetGenreByIdAsync(genreId)).ReturnsAsync(genre);

            // Act
            var result = await _genresController.GetGenreById(genreId);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().Be(genre);
        }

        [Fact]
        public async Task GetGenreById_ReturnsNotFound_WhenGenreDoesNotExist()
        {
            // Arrange
            int genreId = 1;
            _genreService.Setup(service => service.GetGenreByIdAsync(genreId)).ReturnsAsync((Genre)null);

            // Act
            var result = await _genresController.GetGenreById(genreId);

            // Assert
           
        }


        [Fact]
        public async Task GetGenreById_ReturnsInternalServerError_OnException()
        {
            // Arrange
            int genreId = 1;
            _genreService.Setup(service => service.GetGenreByIdAsync(genreId)).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _genresController.GetGenreById(genreId);

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }



    }
}
