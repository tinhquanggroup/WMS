using WMS.Core.Infrastructure.Data.Repositories.Core;

namespace WMS.Core.Infrastructure.Data.Uow;

public interface IUnitOfWork
{
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}