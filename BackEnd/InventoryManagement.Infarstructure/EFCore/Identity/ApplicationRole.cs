using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Infarstructure.EFCore;

public class ApplicationRole : IdentityRole<Guid>
{

    public ICollection<ApplicationUser> Users { get; } = [];

}
