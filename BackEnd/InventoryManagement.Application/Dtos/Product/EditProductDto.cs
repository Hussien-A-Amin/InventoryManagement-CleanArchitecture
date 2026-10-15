using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Dtos;

public class EditProductDto :IValidatableObject
{
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Title))
        {
            yield return new ValidationResult("Title is required.", new[] { nameof(Title) });
        }
        if (string.IsNullOrWhiteSpace(Description))
        {
            yield return new ValidationResult("Description is required.", new[] { nameof(Description) });
        }

        if (Price < 0)
        {
            yield return new ValidationResult("Price cannot be negative.", new[] { nameof(Price) });
        }

    }
}
