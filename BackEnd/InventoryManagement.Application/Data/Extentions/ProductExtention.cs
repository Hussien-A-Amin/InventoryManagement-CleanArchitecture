using InventoryManagement.Application.Dtos;
using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Data.Extentions;

internal static class ProductExtention
{
    public static IQueryable<Product> CanShowForPublic(
       this IQueryable<Product> query)
       => query.Where(p => !p.IsDeleted);



    public static IQueryable<ProductDto> SelectDto(
       this IQueryable<Product> query)
       => query.AsNoTracking().Select(p => new ProductDto()
       {
           Id = p.Id,
           Title = p.Title,
           Price = p.Price,
       });
}


     



