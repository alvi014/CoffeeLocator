using FluentValidation;
using CoffeeLocator.Application.DTOs.Auth;

namespace CoffeeLocator.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("El formato del correo no es válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña no puede estar vacía.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.")
            .Matches(@"[A-Z]").WithMessage("La contraseña debe tener al menos una letra mayúscula.")
            .Matches(@"[0-9]").WithMessage("La contraseña debe tener al menos un número.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("El nombre completo es obligatorio.")
            .MinimumLength(3).WithMessage("El nombre es demasiado corto.");
    }
}