using InventoryManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Dtos;

public class CreateOrderDto :IValidatableObject
{
    public Guid CustomerId { get; set; }
    public List<CreateOrderItemDto> OrderItems { get; set; } = [];
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (OrderItems == null || !OrderItems.Any())
        {
            yield return new ValidationResult("Order must have at least one item.", new[] { nameof(OrderItems) });
        }
    }
    internal Order GetOrder()
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId=CustomerId,
            OrderItems = OrderItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList(),

            TotalAmount = OrderItems.Sum(item => item.UnitPrice * item.Quantity),
            Status = OrderStatus.Pending
        };
        return order;
    }
}
public class CreateOrderItemDto :IValidatableObject
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice         { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Quantity <= 0)
        {
            yield return new ValidationResult("Quantity must be greater than zero.", new[] { nameof(Quantity) });
        }
        if(UnitPrice <= 0)
        {
            yield return new ValidationResult("Unit price must be greater than zero.", new[] { nameof(UnitPrice) });
        }
    }
}
