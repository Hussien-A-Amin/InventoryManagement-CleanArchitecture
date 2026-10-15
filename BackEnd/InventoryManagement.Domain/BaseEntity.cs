using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace InventoryManagement.Domain;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.CreateVersion7();
}
