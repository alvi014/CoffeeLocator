using CoffeeLocator.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace CoffeeLocator.Infrastructure.ExternalServices;

public class GooglePlacesService : IGooglePlacesService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GooglePlacesService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoogleMaps:ApiKey"] ?? "";
    }

    /// <summary>
    /// Gets the photo URL for a place by its Google Place ID.
    /// </summary>
    /// <param name="googlePlaceId"></param>
    /// <returns></returns>
    public async Task<string?> GetPlacePhotoUrlAsync(string googlePlaceId)
    {

        return $"https://maps.googleapis.com/maps/api/place/photo?maxwidth=400&photo_reference={googlePlaceId}&key={_apiKey}";
    }

    /// <summary>
    /// <see langword="get"/>s the Google rating for a place by its Google Place ID.
    /// </summary>
    /// <param name="googlePlaceId"></param>
    /// <returns></returns>
    public async Task<double?> GetGoogleRatingAsync(string googlePlaceId)
    {
        return 4.5; 
    }
}