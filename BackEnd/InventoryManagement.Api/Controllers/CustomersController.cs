using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(ApplicationDataUnit dataUnit) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetCustomers(PublicFilterViewModel? filter = null)
    {
        filter ??= new PublicFilterViewModel();

        await dataUnit.Customers.GetFilteredAsync(filter);
        return Ok();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var Customer = await dataUnit.Customers.GetByIdAsync(id);
        if (Customer == null)
        {
            return NotFound();
        }
        return Ok(Customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDto CustomerDto)
    {
        
        var Customer = await dataUnit.Customers.CreateAsync(CustomerDto);

        return Ok(Customer);
    }

    [HttpPut]
    public async Task<IActionResult> EditCustomer(EditCustomerDto CustomerDto)
    {
        
        var Customer = await dataUnit.Customers.EditCustomer(CustomerDto);

        return Ok(Customer);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        if (await dataUnit.Customers.DeleteCustomer(id))
        {
            return NotFound();
        }
        return Ok();
    }


}
