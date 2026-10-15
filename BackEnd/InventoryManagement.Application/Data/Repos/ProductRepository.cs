using InventoryManagement.Application.Data.Extentions;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Data;

public class ProductRepository(ApplicationDataUnit _dataUnit) : Repository<Product>(_dataUnit)
{
    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .ById(id)
            .SelectDto()
            .FirstOrDefaultAsync();
    }

    public async Task<List<ProductDto>> GetFilteredAsync(PublicFilterViewModel filter)
    {
        var query = _dbSet.CanShowForPublic();

        //ApplyFilters


        filter.ItemsCounter = await query.CountAsync();

        return await query
            .AsNoTracking()
            .SelectDto()
            .ToPaginateList(filter.Page, filter.PageSize);

    }


    public async Task<ProductDto?> CreateAsync(CreateProductDto productDto)
    {
        var newProduct = new Product() 
        {
            Title = productDto.Title ,
            Description = productDto.Description,
            Price = productDto.Price,


        };
        await _dbSet.AddAsync(newProduct);
        await _dataUnit.SaveChangesAsync();

        return await GetByIdAsync(newProduct.Id);

    }

    public async Task<ProductDto?> EditProduct(EditProductDto productDto)
    {
       
        await _dbSet.Where(p => p.Id == productDto.Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.Title, productDto.Title)
            .SetProperty(p => p.Description, productDto.Description)
            .SetProperty(p => p.Price, productDto.Price)
        );

        return await GetByIdAsync(productDto.Id);

    }

   
    public async Task<bool> DeleteProduct(Guid id)
    {
       
      var rs=  await _dbSet.Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return rs > 0;


    }

   






    
}

