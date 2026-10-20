using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
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

        var v = await dataUnit.Customers.GetFilteredAsync(filter);
        var rs = Result.Success(v);

        return Ok(rs);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetCustomerById(Guid id)
    {
        var customer = await dataUnit.Customers.GetByIdAsync(id);

        if (customer == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }

        return Ok(Result.Success(customer));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CreateCustomerDto CustomerDto)
    {

        var customer = await dataUnit.Customers.CreateAsync(CustomerDto);


        if (customer == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(customer));
    }

    [HttpPut]
    public async Task<IActionResult> EditCustomer(EditCustomerDto CustomerDto)
    {
        var customer = await dataUnit.Customers.EditCustomer(CustomerDto);
        if (customer == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(customer));
    }

    [HttpPatch("{id:Guid}/SetCreditLimit")]
    public async Task<IActionResult> SetCreditLimit(Guid id, [FromQuery] decimal newCreditLimit)
    {
        if (!await dataUnit.Customers.SetCreditLimit(id, newCreditLimit))
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound);

            return NotFound(rs);
        }

        return Ok(Result.Success());
    }
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteCustomer(Guid id)
    {
        if (!await dataUnit.Customers.DeleteCustomer(id))
        {
            var rs = Result.Failure()
                 .AddError(Error.NotFound);

            return NotFound(rs);
        }

        return Ok(Result.Success());
    }


}
