namespace InventoryManagement.Domain;

public class OrderItem : BaseEntity
{
    public decimal UnitPrice { get; set; }


    public int Quantity { get; set; }
    public decimal TotalAmount => UnitPrice * Quantity;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;

    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;

}
