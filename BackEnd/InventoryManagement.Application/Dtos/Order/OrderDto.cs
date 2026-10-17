using InventoryManagement.Domain;

namespace InventoryManagement.Application.Dtos;

public class OrderDto : EntityDto
{
    public decimal TotalAmount { get; set; }

    public string CustomerName { get; set; } = default!;
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItemDto> Items { get; set; } = [];
}
public class OrderItemDto 
{
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public string ProductTitle { get; set; } = default!;


}
