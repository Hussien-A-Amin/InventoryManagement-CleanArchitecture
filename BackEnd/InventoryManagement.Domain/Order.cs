namespace InventoryManagement.Domain;

public class Order : DeletableEntity
{

    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItem> OrderItems { get; set; } = [];
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

}
