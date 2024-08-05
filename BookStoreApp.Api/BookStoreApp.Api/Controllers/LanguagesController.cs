using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Language>>> GetAllLanguages()
        {
            var languages = await _languageService.GetAllLanguagesAsync();

            return Ok(languages);
        }
    }
}
