using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Infarstructure.EFCore;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? Address { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<ApplicationSession> Sessions { get; set; } = [];

    // This property is not mapped to the database and is used to hold the current session for the user in memory.
    [NotMapped]
    public ApplicationSession? CurrentSession;
    public List<string> Roles= [];
    public Dictionary<string, string?> Permissions  = [];
}
