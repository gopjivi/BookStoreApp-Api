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

        /// <summary>
        /// Api to get all the Genres
        /// </summary>
        /// <returns> return all the genres</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();

            return Ok(genres);
        }

        /// <summary>
        /// Api to get all the Genres with BookCount
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllGenresWithBookCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenresWithBookCount()
        {
            var genres = await _genreService.GetAllGenresWithBookCountAsync();

            return Ok(genres);
        }

        /// <summary>
        /// Api to check Genre name exists or not
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return true if exists else false </returns>
        [HttpGet("CheckGenreNameIsExists/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> CheckGenreNameIsExists(string name)
        {
           
           bool isExists = await _genreService.CheckGenreNameIsExists(name);
            return Ok(isExists);

        }

        /// <summary>
        /// Api to get Genre by GenreID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return only the particular genre details </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        /// <summary>
        /// Api to create new genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns> return new genre with auto generated id</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
