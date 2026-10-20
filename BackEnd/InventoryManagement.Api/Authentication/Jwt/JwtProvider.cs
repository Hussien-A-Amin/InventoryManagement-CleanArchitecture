using InventoryManagement.Infarstructure.EFCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace InventoryManagement.Api.Authentication.Jwt;

public class JwtProvider(JwtOptions jwtOption) : IAuthProvider
{
    private readonly JwtOptions _jwtOptions = jwtOption;

    public TokenResponse CreateToken(ApplicationUser user,
        IEnumerable<string> userRoles, IEnumerable<string> userPermissions)
    {

        Claim[] claims = new Claim[] {
            new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiredIn = _jwtOptions.ExpiredIn;

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiredIn),
            signingCredentials: signingCredentials
        );

        return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(jwt), expiredIn * 60);
    }

    public string? ValidateToken(string token)
    {
        var TokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        try
        {
            TokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero, // To make Token Expired when time Exceed 
            }, out SecurityToken _validatedToken);

            var JwtToken = (JwtSecurityToken)_validatedToken;

            return JwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch
        {
            return null;
        }

    }
}
