using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Api.Authentication.Jwt;

public class JwtOptions
{
    [Required]
    public string Key { get; init; } = string.Empty;
    [Required]
    public string Issuer { get; init; } = string.Empty;
    [Required]
    public string Audience { get; init; } = string.Empty;
    [Required, Range(1, int.MaxValue)]
    public int ExpiredIn { get; init; }
}
