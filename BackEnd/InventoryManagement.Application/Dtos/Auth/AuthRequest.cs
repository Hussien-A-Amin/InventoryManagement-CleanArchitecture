using FluentValidation;

namespace InventoryManagement.Application.Dtos;

public record AuthRequest(string Email, string Password);
public class AuthRequestValidator : AbstractValidator<AuthRequest>
{
    public AuthRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MinimumLength(5)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(10)
            
            ;
    }
}
