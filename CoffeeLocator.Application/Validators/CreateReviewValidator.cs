using FluentValidation;
using CoffeeLocator.Application.DTOs.Reviews;

namespace CoffeeLocator.Application.Validators;

public class CreateReviewValidator : AbstractValidator<CreateReviewDto>
{
    /// <summary>
    /// Metod <see langword="for"/> creating validation rules for <see cref="CreateReviewDto"/>.
    /// </summary>
    public CreateReviewValidator()
    {
        RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("El comentario no puede estar vacío.")
            .MinimumLength(10).WithMessage("Por favor, escribe un comentario un poco más detallado (mínimo 10 caracteres).")
            .MaximumLength(500).WithMessage("El comentario es demasiado largo, intenta resumirlo un poco (máximo 500 caracteres).")
            .Must(c => !string.IsNullOrWhiteSpace(c)).WithMessage("El comentario no puede consistir solo en espacios.");

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5).WithMessage("La calificación debe ser un número entre 1 y 5 estrellas.");

        RuleFor(x => x.CoffeeShopId)
            .NotEmpty().WithMessage("Es necesario indicar a qué cafetería pertenece esta reseña.");
    }
}