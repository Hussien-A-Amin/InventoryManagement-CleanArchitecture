using InventoryManagement.Application.Dtos;
using InventoryManagement.Domain;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Data.Extentions;

internal static class CustomerExtention
{
    public static IQueryable<Customer> CanShowForPublic(
       this IQueryable<Customer> query)
       => query.Where(p => !p.IsDeleted);



    public static IQueryable<CustomerDto> SelectDto(
       this IQueryable<Customer> query)
       => query.AsNoTracking().Select(p => new CustomerDto()
       {
           Id = p.Id,
           Name = p.Name,
           Credit = p.Credit,
           CreditLimit = p.CreditLimit,
       });


    public static Task<bool> CheckRules(
       this IQueryable<Customer> query, Guid id)
       => query
        .ById(id)
        .Where(p => p.Credit > p.CreditLimit)
        .AnyAsync();



    public static async Task TryUpdateTotals(
       this IQueryable<Customer> query)
    {
        var rs= await query
            .Where(p => p.Credit > p.CreditLimit)
            .ExecuteUpdateAsync(p => p
            .SetProperty(p => p.Credit, m => m.Orders.Where(p => !p.IsDeleted).Sum(p => p.TotalAmount)

            ));
        if (rs!=1)
        {
            throw new InvalidOperationException("Customer has exceeded their credit limit.");
        }

    }
}






