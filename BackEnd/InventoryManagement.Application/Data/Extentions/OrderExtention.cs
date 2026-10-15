using InventoryManagement.Application.Dtos;
using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Data.Extentions;

internal static class OrderExtention
{
    public static IQueryable<Order> CanShowForPublic(
       this IQueryable<Order> query)
       => query.Where(p => !p.IsDeleted);



    public static IQueryable<OrderDto> SelectDto(
       this IQueryable<Order> query)
       => query.AsNoTracking().Select(p => new OrderDto()
       {
           
       });
}


     



