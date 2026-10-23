using InventoryManagement.Infarstructure.Costants;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Api.Authentication.Jwt;

public class JwtProvider(JwtOptions jwtOption) : IAuthProvider
{
    private readonly JwtOptions _jwtOptions = jwtOption;

    public TokenResponse CreateToken(ApplicationUser user)
    {

        Claim[] claims = new Claim[] {
            new Claim(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
            new Claim("SessionId", user.CurrentSession!.Id.ToString()),
        };
        user.Roles.ForEach(role => claims = claims.Append(new Claim(ClaimTypes.Role, role)).ToArray());

        user.Permissions.ToList().ForEach(permission => claims = claims.Append(new Claim(permission.Key, permission.Value ?? string.Empty)).ToArray());



        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var expiredIn = _jwtOptions.ExpiredIn;

        var jwt = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiredIn),
            signingCredentials: signingCredentials
        );

        return new TokenResponse()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            RefreshToken=user.CurrentSession.RawToken!,
            ExpireIn = expiredIn * 60
        };
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
