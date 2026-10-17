using InventoryManagement.Application.Dtos;
using InventoryManagement.Domain;
using InventoryManagement.Infarstructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;

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
           Id = p.Id,
           TotalAmount = p.TotalAmount,
           CustomerName = p.Customer.Name,
           CustomerId = p.CustomerId,
           Status = p.Status,
           Items = p.OrderItems.Select(i => new OrderItemDto()
           {
               ProductTitle = i.Product.Title,
               Quantity = i.Quantity,
               UnitPrice = i.UnitPrice
           }).ToList()
       });



    public static async Task<Guid> GetCustomerId(
    this IQueryable<Order> query)
    {
        return await query.Select(p => p.CustomerId).FirstOrDefaultAsync();
            
    }
   
}






