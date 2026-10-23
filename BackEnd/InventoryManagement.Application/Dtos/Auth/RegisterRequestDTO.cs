using FluentValidation;

namespace InventoryManagement.Application.Dtos;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(2, 90)
            .WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .Length(3, 100)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .NotEmpty();

    }
}
