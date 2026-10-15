using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Dtos;

public class CreateCustomerDto :IValidatableObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return new ValidationResult("Name is required.", new[] { nameof(Name) });
        }
       

    }
}
