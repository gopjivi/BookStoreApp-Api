using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly ILogger<LanguagesController> _logger;

        public LanguagesController(ILanguageService languageService, ILogger<LanguagesController> logger)
        {
            _languageService = languageService;
            _logger = logger;
        }

        /// <summary>
        ///  Get all the languages
        /// </summary>
        /// <returns>return all the languages</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Language>>> GetAllLanguages()
        {
            try
            {
                _logger.LogInformation("Fetching all languages.");

                var languages = await _languageService.GetAllLanguagesAsync();

                _logger.LogInformation("Successfully fetched all languages.");
                return Ok(languages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all languages.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
