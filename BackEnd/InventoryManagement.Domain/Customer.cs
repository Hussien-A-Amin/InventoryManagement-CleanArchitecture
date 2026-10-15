namespace InventoryManagement.Domain;

public class Customer : DeletableEntity
{

    public string Name { get; set; } = default!;
    public decimal CreditLimit { get; set; } 
    public decimal Credit { get; set; }
    public List<Order> Orders { get; set; } = [];

}

