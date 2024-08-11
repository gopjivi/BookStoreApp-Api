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
    public class LanguagesControllerTest
    {
        private readonly Mock<ILanguageService> _languageService;
        private readonly Mock<ILogger<LanguagesController>> _logger;
        private readonly LanguagesController _languagesController;
        public LanguagesControllerTest()
        {
            //setup dependencies
            _languageService = new Mock<ILanguageService>();
            _logger = new Mock<ILogger<LanguagesController>>();
            _languagesController = new LanguagesController(_languageService.Object, _logger.Object);
        }

        private List<Language> languages = new List<Language>()
          {
                new Language { LanguageID = 1, LanguageName = "Tamil"  },
                new Language { LanguageID = 2, LanguageName = "English"  }
         };

        [Fact]
        public async Task GetAllLanguages_ReturnsOk_OnSuccess()
        {
            // Arrange
            var mockLanguages = languages;

            _languageService.Setup(service => service.GetAllLanguagesAsync())
                                .ReturnsAsync(mockLanguages);

            // Act
            var result = await _languagesController.GetAllLanguages();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);

            actionResult.StatusCode.Should().Be(200);
            actionResult.Value.Should().BeEquivalentTo(mockLanguages);
        }

        [Fact]
        public async Task GetAllLanguages_ReturnsInternalServerError_OnException()
        {
            // Arrange
            _languageService.Setup(service => service.GetAllLanguagesAsync())
                                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _languagesController.GetAllLanguages();

            // Assert
            var actionResult = Assert.IsType<ObjectResult>(result.Result);
            actionResult.StatusCode.Should().Be(500);
            actionResult.Value.Should().Be("Internal server error");
        }

    }

}
