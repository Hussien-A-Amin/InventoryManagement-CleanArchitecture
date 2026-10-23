using FluentValidation;

namespace InventoryManagement.Api.Authentication.Jwt;

public class TokenRequest
{
    public string RefreshToken { get; set; } = default!;

}
public class TokenRequestValidator : AbstractValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
