namespace CoffeeLocator.Application.DTOs.Reviews;

public record CreateReviewDto(
    string UserId,
    string Comment,
    int Rating,
    Guid CoffeeShopId
);