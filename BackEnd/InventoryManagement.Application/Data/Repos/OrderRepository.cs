using InventoryManagement.Application.Data.Extentions;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        await dataUnit.BeginTransactionAsync();
        var newOrder = OrderDto.GetOrder();
        await _dbSet.AddAsync(newOrder);
        await _dataUnit.SaveChangesAsync();

        await GetDbSet<Customer>()
              .ById(OrderDto.CustomerId)
              .TryUpdateTotals();


        await dataUnit.CommitTransactionAsync();



        return await GetByIdAsync(newOrder.Id);

    }

    public async Task<OrderDto?> EditOrder(EditOrderDto OrderDto)
    {

        await dataUnit.BeginTransactionAsync();

        await dataUnit._context.Set<OrderItem>()
            .Where(p => p.OrderId == OrderDto.Id)
            .ExecuteDeleteAsync();



        OrderDto.OrderItems.ForEach(p =>
        {
            dataUnit._context.Set<OrderItem>().Add(new OrderItem()
            {
                OrderId = OrderDto.Id,
                ProductId = p.ProductId,
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice,
            });

        });


        await _dataUnit.SaveChangesAsync();

        await _dbSet
            .ById(OrderDto.Id)
            .ExecuteUpdateAsync(p => p.SetProperty(p => p.TotalAmount, m => m.OrderItems.Sum(p => p.UnitPrice * p.Quantity)
        ));


        await GetDbSet<Customer>()
              .ById(await _dbSet.ById(OrderDto.Id).GetCustomerId())
             .TryUpdateTotals();

       

        await dataUnit.CommitTransactionAsync();

        return await GetByIdAsync(OrderDto.Id);

    }


    public async Task<bool> DeleteOrder(Guid id)
    {
        var rs = await _dbSet.ById(id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.IsDeleted, true)
            .SetProperty(p => p.DeletedAtUtc, DateTimeOffset.UtcNow)
        );

        await GetDbSet<Customer>()
              .ById(await _dbSet.ById(id).GetCustomerId())
              .TryUpdateTotals();


        return rs == 1;
    }

    public async Task<bool> DeletePermanently(Guid id)
    {


        var rs = await _dbSet.Where(p => p.Id == id)
              .ExecuteDeleteAsync();

        return rs > 0;


    }











}

