namespace CoffeeLocator.Domain.Interfaces;

public interface IGooglePlacesService
{
    Task<string?> GetPlacePhotoUrlAsync(string googlePlaceId);
    Task<double?> GetGoogleRatingAsync(string googlePlaceId);

}