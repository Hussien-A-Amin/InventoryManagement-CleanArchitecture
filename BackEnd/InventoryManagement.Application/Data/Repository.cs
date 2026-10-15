using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Data;

public class Repository<T> where T : class
{

    protected readonly ApplicationDataUnit dataUnit;

    protected readonly DbSet<T> _dbSet;
    public Repository(ApplicationDataUnit unitOfWork)
    {
        dataUnit = unitOfWork;
        _dbSet = dataUnit._context.Set<T>();
    }
}