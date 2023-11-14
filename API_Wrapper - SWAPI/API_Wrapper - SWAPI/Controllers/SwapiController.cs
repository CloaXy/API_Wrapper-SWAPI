using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]")]
public class SwapiController : ControllerBase
{
    private readonly SwapiWrapper _swapiWrapper;

    public SwapiController(HttpClient httpClient, IMemoryCache cache)
    {
        _swapiWrapper = new SwapiWrapper(httpClient, cache);
    }

    [HttpGet("films")]
    public async Task<IActionResult> GetFilms()
    {
        try
        {
            var films = await _swapiWrapper.GetFilmsAsync();
            return Ok(films);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("starships/{filmId}")]
    public async Task<IActionResult> GetStarshipsForFilm(int filmId)
    {
        try
        {
            var starships = await _swapiWrapper.GetStarshipsForFilmAsync(filmId);
            return Ok(starships);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("characters/{filmId}")]
    public async Task<IActionResult> GetCharactersForFilm(int filmId)
    {
        try
        {
            var characters = await _swapiWrapper.GetCharactersForFilmAsync(filmId);
            return Ok(characters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}
