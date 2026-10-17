namespace InventoryManagement.Application.Dtos;

public class CustomerDto : EntityDto
{
    public required string Name { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Credit { get; set; }

}
