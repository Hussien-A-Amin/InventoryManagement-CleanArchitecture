using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Infarstructure.EFCore;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? Address { get; set; }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;




}
