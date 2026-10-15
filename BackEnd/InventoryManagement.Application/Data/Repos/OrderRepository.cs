using InventoryManagement.Application.Data.Extentions;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Data;

public class OrderRepository(ApplicationDataUnit _dataUnit) : Repository<Order>(_dataUnit)
{
    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .ById(id)
            .SelectDto()
            .FirstOrDefaultAsync();
    }

    public async Task<List<OrderDto>> GetFilteredAsync(PublicFilterViewModel filter)
    {
        var query = _dbSet.CanShowForPublic();

        //ApplyFilters


        filter.ItemsCounter = await query.CountAsync();

        return await query
            .AsNoTracking()
            .SelectDto()
            .ToPaginateList(filter.Page, filter.PageSize);

    }


    public async Task<OrderDto?> CreateAsync(CreateOrderDto OrderDto)
    {
        var newOrder = new Order() 
        {


        };
        await _dbSet.AddAsync(newOrder);
        await _dataUnit.SaveChangesAsync();

        return await GetByIdAsync(newOrder.Id);

    }

    public async Task<OrderDto?> EditOrder(EditOrderDto OrderDto)
    {
       
        //await _dbSet.Where(p => p.Id == OrderDto.Id)
        //    .ExecuteUpdateAsync(setters => setters
        //);

        return await GetByIdAsync(OrderDto.Id);

    }

   
    public async Task<bool> DeleteOrder(Guid id)
    {
       
      var rs=  await _dbSet.Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return rs > 0;


    }

   






    
}

