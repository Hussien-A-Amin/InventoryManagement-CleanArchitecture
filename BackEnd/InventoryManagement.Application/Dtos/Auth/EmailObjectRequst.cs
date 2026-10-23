using FluentValidation;

namespace InventoryManagement.Application.Dtos;

public record EmailObjectRequst(
    string Email
);
public class EmailObjectRequestValidator : AbstractValidator<EmailObjectRequst>
{
    public EmailObjectRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("User ID is required.")
            .EmailAddress();


    }
}