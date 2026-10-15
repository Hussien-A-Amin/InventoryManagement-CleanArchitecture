using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Dtos;

public class EditOrderDto :IValidatableObject
{
    public Guid Id { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return new List<ValidationResult>();
    }
}
