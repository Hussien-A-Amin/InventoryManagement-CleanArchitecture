using FluentValidation;

namespace InventoryManagement.Application.Dtos;

public record ResetPasswordRequest(
    string email,
    string code,
    string newPassword
);
public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(x => x.email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is required and should be a valid email address.");
        RuleFor(x => x.code)
            .NotEmpty()
            .WithMessage("Code is required.");
        RuleFor(x => x.newPassword)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("New password is required and should be at least 6 characters long.");
    }
}
