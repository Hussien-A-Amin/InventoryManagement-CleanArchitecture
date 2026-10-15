namespace InventoryManagement.Domain;

public abstract class DeletableEntity : AuditableEntity
{
    public DateTimeOffset? DeletedAtUtc { get; set; }
    public Guid? DeletedById { get; set; }
    public bool IsDeleted { get; set; }
}
