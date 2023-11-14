using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

public class SwapiWrapper
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly IMemoryCache _cache;

    public SwapiWrapper(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _baseUrl = "https://swapi.dev/api/";
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<string> GetFilmsAsync()
    {
        const string endpoint = "films/";
        var filmsData = await GetCachedDataAsync(endpoint);

        // Deserialize the films data to get the film names
        var films = JsonSerializer.Deserialize<FilmList>(filmsData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (films?.Results != null)
        {
            var filmNames = films.Results.Select(film => film.Title).ToList();
            return JsonSerializer.Serialize(filmNames);
        }

        return "[]"; // Return an empty array if no films found
    }

    public class Starship
    {
        public string Name { get; set; }
        
    }

    public async Task<string> GetStarshipsForFilmAsync(int filmId)
    {
        const string endpoint = "films/";
        var filmsData = await GetCachedDataAsync(endpoint);

        // Deserialize the films data to get the film details
        var films = JsonSerializer.Deserialize<FilmList>(filmsData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var selectedFilm = films?.Results?.FirstOrDefault(film => film.Id == filmId);

        if (selectedFilm != null)
        {
            
            var starshipUrls = selectedFilm.Starships;
            var starshipNames = await GetStarshipNamesAsync(starshipUrls);
            return JsonSerializer.Serialize(starshipNames);
        }

        return "[]"; // Return an empty array if no film found for the given ID
    }

    private async Task<List<string>> GetStarshipNamesAsync(List<string> starshipUrls)
    {
        var starshipNames = new List<string>();

        foreach (var starshipUrl in starshipUrls)
        {
            string starshipData = await GetCachedDataAsync(starshipUrl);
            var starship = JsonSerializer.Deserialize<Starship>(starshipData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (starship != null)
            {
                starshipNames.Add(starship.Name);
            }
        }

        return starshipNames;
    }


    public class Character
    {
        public string Name { get; set; }
        
    }

    public async Task<string> GetCharactersForFilmAsync(int filmId)
    {
        const string endpoint = "films/";
        var filmsData = await GetCachedDataAsync(endpoint);

        // Deserialize the films data to get the film details
        var films = JsonSerializer.Deserialize<FilmList>(filmsData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        var selectedFilm = films?.Results?.FirstOrDefault(film => film.Id == filmId);

        if (selectedFilm != null)
        {
            
            var characterUrls = selectedFilm.Characters;
            var characterNames = await GetCharacterNamesAsync(characterUrls);
            return JsonSerializer.Serialize(characterNames);
        }

        return "[]"; // Return an empty array if no film found for the given ID
    }

    private async Task<List<string>> GetCharacterNamesAsync(List<string> characterUrls)
    {
        var characterNames = new List<string>();

        foreach (var characterUrl in characterUrls)
        {
            string characterData = await GetCachedDataAsync(characterUrl);
            var character = JsonSerializer.Deserialize<Character>(characterData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (character != null)
            {
                characterNames.Add(character.Name);
            }
        }

        return characterNames;
    }
   

    private async Task<string> GetCachedDataAsync(string endpoint)
    {
        if (_cache.TryGetValue(endpoint, out string cachedData))
        {
            return cachedData;
        }

        string apiUrl = $"{_baseUrl}{endpoint}";
        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            _cache.Set(endpoint, responseData, TimeSpan.FromMinutes(5));
            return responseData;
        }
        else
        {
            throw new Exception($"Failed to fetch data from {apiUrl}. Status code: {response.StatusCode}");
        }
    }
}

public class FilmList
{
    public List<Film> Results { get; set; }
}

public class Film
{
    public int Id { get; set; }  // Add this property to represent the ID of the film
    public string Title { get; set; }
    // Other properties related to the film

    // Constructor to initialize the Starships list
    public Film()
    {
        Starships = new List<string>();
        Characters = new List<string>();
    }
    public List<string> Characters { get; set; }
    public List<string> Starships { get; set; }
}


