using Microsoft.EntityFrameworkCore;
using Entities = WMS.Core.Domain.Entities;

namespace WMS.Core.Infrastructure.Data.EFContext;

public interface IApplicationDbContext
{
    DbSet<Entities.Attribute> Attributes { get; set; }
    DbSet<Entities.Inventory> Inventories { get; set; }
    DbSet<Entities.InventoryTransaction> InventoryTransactions { get; set; }
    DbSet<Entities.Location> Locations { get; set; }
    DbSet<Entities.Product> Products { get; set; }
    DbSet<Entities.ProductAttribute> ProductAttributes { get; set; }
    DbSet<Entities.ProductCategory> ProductCategories { get; set; }
    DbSet<Entities.Zone> Zones { get; set; }

}