using InventoryManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infarstructure.EFCore;

public static class ApplicationDbContextExtention
{
    public async static Task<List<T>> ToPaginateList<T>(this IQueryable<T> query, int pageIndex, int pageSize) where T : class
    {
        return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    public static IQueryable<T> ById<T>(
      this IQueryable<T> query, Guid id) where T : BaseEntity
      => query.AsNoTracking().Where(p => p.Id == id);
}
