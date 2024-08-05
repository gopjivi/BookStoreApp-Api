using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();

            return Ok(genres);
        }

        [HttpGet("GetAllGenresWithBookCount")]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenresWithBookCount()
        {
            var genres = await _genreService.GetAllGenresWithBookCountAsync();

            return Ok(genres);
        }

        [HttpGet("CheckGenreNameIsExists/{name}")]
        public async Task<ActionResult<bool>> CheckGenreNameIsExists(string name)
        {
           
           bool isExists = await _genreService.CheckGenreNameIsExists(name);
            return Ok(isExists);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre(Genre genre)
        {
            var createdGenre = await _genreService.AddGenreAsync(genre);

            if (createdGenre)
            {
                return CreatedAtAction(nameof(GetGenreById), new { id = genre.GenreID }, genre);
            }

            return BadRequest();
        }


    }
}
