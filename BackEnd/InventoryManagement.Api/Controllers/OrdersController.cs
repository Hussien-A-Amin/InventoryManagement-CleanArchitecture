using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ApplicationDataUnit dataUnit) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetOrders(PublicFilterViewModel? filter = null)
    {
        filter ??= new PublicFilterViewModel();

        var v=await dataUnit.Orders.GetFilteredAsync(filter);
        var rs = Result.Success(v);

        return Ok(rs);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var order = await dataUnit.Orders.GetByIdAsync(id);
        if (order == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(order));
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto OrderDto)
    {
        
        var order = await dataUnit.Orders.CreateAsync(OrderDto);

        if (order == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(order));
    }

    [HttpPut]
    public async Task<IActionResult> EditOrder(EditOrderDto OrderDto)
    {
        var order = await dataUnit.Orders.EditOrder(OrderDto);
        if (order == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound);

            return NotFound(rs);
        }
        return Ok(Result.Success(order));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        if (!await dataUnit.Orders.DeleteOrder(id))
        {
            var rs = Result.Failure()
                 .AddError(Error.NotFound);

            return NotFound(rs);
        }

        return Ok(Result.Success());
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeletePermanently(Guid id)
    {
        if (!await dataUnit.Orders.DeletePermanently(id))
        {
            var rs = Result.Failure()
                            .AddError(Error.NotFound);

            return NotFound(rs);
        }

        return Ok(Result.Success());
    }


}
