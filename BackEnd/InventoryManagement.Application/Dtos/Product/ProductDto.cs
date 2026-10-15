using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Dtos;

public class ProductDto 
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public decimal Price { get; set; }

}
