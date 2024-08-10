using BookStoreApp.Api.Models;
using BookStoreApp.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenresController> _logger;

        public GenresController(IGenreService genreService, ILogger<GenresController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        /// <summary>
        /// Api to get all the Genres
        /// </summary>
        /// <returns> return all the genres</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
        {
            try
            {
                _logger.LogInformation("Fetching all genres.");

                var genres = await _genreService.GetAllGenresAsync();

                _logger.LogInformation("Successfully fetched all genres.");
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all genres.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        /// <summary>
        /// Api to get all the Genres with BookCount
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllGenresWithBookCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenresWithBookCount()
        {
            try
            {
                _logger.LogInformation("Fetching all genres with book count.");

                var genres = await _genreService.GetAllGenresWithBookCountAsync();

                _logger.LogInformation("Successfully fetched all genres with book count.");
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all genres with book count.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
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
            try
            {
                _logger.LogInformation($"Checking if genre name '{name}' exists.");

                bool isExists = await _genreService.CheckGenreNameIsExists(name);

                _logger.LogInformation($"Genre name '{name}' exists: {isExists}.");
                return Ok(isExists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while checking if genre name '{name}' exists.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
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
            try
            {
                _logger.LogInformation($"Fetching genre with ID {id}.");

                var genre = await _genreService.GetGenreByIdAsync(id);

                if (genre == null)
                {
                    _logger.LogWarning($"Genre with ID {id} not found.");
                    return NotFound();
                }

                _logger.LogInformation($"Successfully fetched genre with ID {id}.");
                return Ok(genre);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching genre with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
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
            try
            {
                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for genre creation. Genre name: {GenreName}.", genre.GenreName);
                    return BadRequest(ModelState);
                }
                _logger.LogInformation("Creating a new genre.");

                var createdGenre = await _genreService.AddGenreAsync(genre);

                if (createdGenre)
                {
                    _logger.LogInformation($"Successfully created genre with ID {genre.GenreID}.");
                    return CreatedAtAction(nameof(GetGenreById), new { id = genre.GenreID }, genre);
                }

                _logger.LogWarning("Failed to create a new genre.");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new genre.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
