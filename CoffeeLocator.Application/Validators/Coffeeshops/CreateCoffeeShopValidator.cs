using FluentValidation;
using CoffeeLocator.Application.DTOs.CoffeeShops;

namespace CoffeeLocator.Application.Validators.CoffeeShops;

public class CreateCoffeeShopValidator : AbstractValidator<CreateCoffeeShopDto>
{
    public CreateCoffeeShopValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.GooglePlaceId)
            .NotEmpty().WithMessage("Google Place ID is required.");

        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }
}