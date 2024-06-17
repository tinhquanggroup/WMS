using WMS.Core.Infrastructure.Data.EFContext;
using WMS.Core.Infrastructure.Data.Repositories.Core;

namespace WMS.Core.Infrastructure.Data.Uow;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private bool _disposed;
    private readonly Dictionary<Type, object> _repositories = [];

    public async Task BeginTransaction()
    {
        await dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransaction()
    {
        await dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransaction()
    {
        await dbContext.Database.RollbackTransactionAsync();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity);

        if (_repositories.TryGetValue(type, out var repository))
            return (IGenericRepository<TEntity>)repository;

        repository = new GenericRepository<TEntity>(dbContext);
        _repositories[type] = repository;

        return (IGenericRepository<TEntity>)repository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

}