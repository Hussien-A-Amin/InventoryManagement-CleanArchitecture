using InventoryManagement.Domain;

namespace InventoryManagement.Application.Dtos;

public class OrderDto 
{
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

}
