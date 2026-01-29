using System;
using System.Collections.Generic;
using CoffeeLocator.Application.DTOs.Reviews;

namespace CoffeeLocator.Application.DTOs.CoffeeShops;

public record CoffeeShopDetailDto(
    Guid Id,
    string Name,
    string? Description,
    string Address,
    string GooglePlaceId,
    double Latitude,
    double Longitude,
    bool IsPremium,
    double AverageRating,
    int TotalReviews,
    List<ReviewResponseDto> RecentReviews,
    string? ImageUrl
);  