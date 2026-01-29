using FluentValidation;
using CoffeeLocator.Application.DTOs.CoffeeShops;

namespace CoffeeLocator.Application.Validators;

public class CreateCoffeeShopValidator : AbstractValidator<CreateCoffeeShopDto>
{
    /// <summary>
    /// Validator for creating a new coffee shop.
    /// </summary>
    public CreateCoffeeShopValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la cafetería es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("El nombre no puede ser solo espacios.");

        RuleFor(x => x.GooglePlaceId)
            .NotEmpty().WithMessage("El Google Place ID es obligatorio para la sincronización con mapas.")
            .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("El Google Place ID tiene un formato inválido.");

        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("La latitud es obligatoria.")
            .InclusiveBetween(-90, 90).WithMessage("La latitud debe ser una coordenada válida entre -90 y 90.");

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("La longitud es obligatoria.")
            .InclusiveBetween(-180, 180).WithMessage("La longitud debe ser una coordenada válida entre -180 y 180.");

  
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("La dirección física es necesaria.");
    }
}