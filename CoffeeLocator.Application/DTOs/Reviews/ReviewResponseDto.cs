namespace CoffeeLocator.Application.DTOs.Reviews;

public record ReviewResponseDto(
    Guid Id,
    string UserName,
    string Comment,
    int Rating,
    DateTime CreatedAt 
);