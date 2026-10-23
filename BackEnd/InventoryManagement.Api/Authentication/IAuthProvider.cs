using InventoryManagement.Infarstructure.EFCore;

namespace InventoryManagement.Api.Authentication.Jwt;

public interface IAuthProvider
{
    TokenResponse CreateToken(ApplicationUser user);
    string? ValidateToken(string token);
}
public class TokenResponse
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public int ExpireIn { get; set; } 

}
