namespace InventoryManagement.Domain;

public class Product : DeletableEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }


    public decimal Price { get; set; }



}

