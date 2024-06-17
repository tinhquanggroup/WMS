using WMS.Core.Domain.Shared.Pagination;
using WMS.Core.Domain.Shared.QueryParams;

namespace WMS.Core.Infrastructure.Data.Repositories.Core;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TResult?> GetSingleAsync<TResult>(QueryOptions<TEntity, TResult> options);
    Task<List<TResult>> GetMultipleAsync<TResult>(QueryOptions<TEntity, TResult> options);
    IQueryable<TEntity> Sorting(IQueryable<TEntity> queryable, string? sortColumn, string? sortOrder);
    Task<IPagedList<TResult>> GetPagedListAsync<TResult>(QueryOptions<TEntity, TResult> options);
    IQueryable<TEntity> GetQueryable();
    void Insert(TEntity entity);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
}