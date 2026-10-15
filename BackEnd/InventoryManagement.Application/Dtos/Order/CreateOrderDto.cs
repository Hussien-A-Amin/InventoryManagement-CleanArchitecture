using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Dtos;

public class CreateOrderDto :IValidatableObject
{
    public Guid Id { get; set; }
   
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return new List<ValidationResult>();
    }
}
