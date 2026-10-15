using InventoryManagement.Application.Data.Extentions;
using InventoryManagement.Application.Dtos;
using InventoryManagement.Application.Filters;
using InventoryManagement.Domain;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Data;

public class CustomerRepository(ApplicationDataUnit _dataUnit) : Repository<Customer>(_dataUnit)
{
    public async Task<CustomerDto?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .ById(id)
            .SelectDto()
            .FirstOrDefaultAsync();
    }

    public async Task<List<CustomerDto>> GetFilteredAsync(PublicFilterViewModel filter)
    {
        var query = _dbSet.CanShowForPublic();

        //ApplyFilters


        filter.ItemsCounter = await query.CountAsync();

        return await query
            .AsNoTracking()
            .SelectDto()
            .ToPaginateList(filter.Page, filter.PageSize);

    }


    public async Task<CustomerDto?> CreateAsync(CreateCustomerDto CustomerDto)
    {
        var newCustomer = new Customer() 
        {
            Name = CustomerDto.Name 


        };
        await _dbSet.AddAsync(newCustomer);
        await _dataUnit.SaveChangesAsync();

        return await GetByIdAsync(newCustomer.Id);

    }

    public async Task<CustomerDto?> EditCustomer(EditCustomerDto CustomerDto)
    {
       
        await _dbSet.Where(p => p.Id == CustomerDto.Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(p => p.Name, CustomerDto.Name)
        );

        return await GetByIdAsync(CustomerDto.Id);

    }

   
    public async Task<bool> DeleteCustomer(Guid id)
    {
       
      var rs=  await _dbSet.Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return rs > 0;


    }

   






    
}

