using InventoryManagement.Api.ErrorHandlers;
using InventoryManagement.Application.Data;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ApplicationDataUnit dataUnit) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(PublicFilterViewModel? filter = null)
    {
        filter ??= new PublicFilterViewModel();

        var v = await dataUnit.Products.GetFilteredAsync(filter);

        var rs = Result.Success(v);

        return Ok(rs);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var product = await dataUnit.Products.GetByIdAsync(id);





        if (product == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(product));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
    {

        var product = await dataUnit.Products.CreateAsync(productDto);

        if (product == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(product));
    }

    [HttpPut]
    public async Task<IActionResult> EditProduct(EditProductDto productDto)
    {

        var product = await dataUnit.Products.EditProduct(productDto);

        if (product == null)
        {
            var rs = Result.Failure()
                .AddError(Error.NotFound)
                ;

            return NotFound(rs);
        }
        return Ok(Result.Success(product));
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        if (!await dataUnit.Products.DeleteProduct(id))
        {
            var rs = Result.Failure()
                  .AddError(Error.NotFound) ;

            return NotFound(rs);
        }

        return Ok(Result.Success());
    }


}
