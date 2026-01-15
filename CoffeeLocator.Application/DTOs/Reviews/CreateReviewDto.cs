namespace CoffeeLocator.Application.DTOs.Reviews;

public record CreateReviewDto(
    string Comment,
    int Rating,
    Guid CoffeeShopId
);