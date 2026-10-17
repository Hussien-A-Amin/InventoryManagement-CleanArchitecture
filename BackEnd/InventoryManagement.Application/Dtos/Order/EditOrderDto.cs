using InventoryManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Dtos;

public class EditOrderDto :IValidatableObject
{
    public Guid Id { get; set; }

    public List<CreateOrderItemDto> OrderItems { get; set; } = [];
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {

        if (OrderItems == null || !OrderItems.Any())
        {
            yield return new ValidationResult("Order must have at least one item.", new[] { nameof(OrderItems) });
        }

    }

}
