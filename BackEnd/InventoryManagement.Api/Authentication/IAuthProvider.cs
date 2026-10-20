using InventoryManagement.Infarstructure.EFCore;

namespace InventoryManagement.Api.Authentication.Jwt;

public interface IAuthProvider
{
    TokenResponse CreateToken(ApplicationUser user, IEnumerable<string> userRoles, IEnumerable<string> userPermissions);
    string? ValidateToken(string token);
}
public record TokenResponse(string Token, int ExpireIn);
