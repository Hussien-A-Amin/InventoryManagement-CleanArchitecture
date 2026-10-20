using FluentValidation;

namespace InventoryManagement.Application.Dtos;

public record ConfirmEmailRequest(
   string Email,
   string Code
);
public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .EmailAddress();

        RuleFor(x => x.Code)
            .NotEmpty();
    }
}
