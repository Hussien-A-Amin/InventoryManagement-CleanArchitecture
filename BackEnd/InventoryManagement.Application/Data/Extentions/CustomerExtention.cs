using InventoryManagement.Application.Dtos;
using InventoryManagement.Domain;
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
           Name = p.Name,
       });
}


     



