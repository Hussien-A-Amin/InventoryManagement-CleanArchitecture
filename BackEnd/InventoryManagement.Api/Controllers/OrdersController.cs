using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
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

        await dataUnit.Orders.GetFilteredAsync(filter);
        return Ok();
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var Order = await dataUnit.Orders.GetByIdAsync(id);
        if (Order == null)
        {
            return NotFound();
        }
        return Ok(Order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto OrderDto)
    {
        
        var Order = await dataUnit.Orders.CreateAsync(OrderDto);

        return Ok(Order);
    }

    [HttpPut]
    public async Task<IActionResult> EditOrder(EditOrderDto OrderDto)
    {
        
        var Order = await dataUnit.Orders.EditOrder(OrderDto);

        return Ok(Order);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        if (await dataUnit.Orders.DeleteOrder(id))
        {
            return NotFound();
        }
        return Ok();
    }


}
