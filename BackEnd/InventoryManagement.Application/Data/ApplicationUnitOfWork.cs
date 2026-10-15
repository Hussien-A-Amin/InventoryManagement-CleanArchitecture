using InventoryManagement.Infarstructure.EFCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.Application.Data;

public abstract class ApplicationDataUnit
{
    #region Core

    public string ErrorMessage { get; set; } = string.Empty;
    public readonly ApplicationDbContext _context;
    public readonly ILogger<ApplicationDataUnit> logger;

    public ApplicationDataUnit(ApplicationDbContext context, ILogger<ApplicationDataUnit> logger)
    {
        _context = context;
        this.logger = logger;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    #endregion


    public ProductRepository Products => new ProductRepository(this);
    public OrderRepository Orders => new OrderRepository(this);
    public CustomerRepository Customers => new CustomerRepository(this);





}
