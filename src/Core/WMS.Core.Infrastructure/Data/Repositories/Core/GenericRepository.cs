using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WMS.Core.Domain.Abstractions;
using WMS.Core.Domain.Shared.Pagination;
using WMS.Core.Domain.Shared.QueryParams;
using WMS.Core.Infrastructure.Data.EFContext;
using WMS.Core.Infrastructure.Extensions;

namespace WMS.Core.Infrastructure.Data.Repositories.Core;

public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    internal ApplicationDbContext Context = context;
    internal DbSet<TEntity> DbSet = context.Set<TEntity>();

    public async Task<TResult?> GetSingleAsync<TResult>(QueryOptions<TEntity, TResult> options)
    {
        var queryable = DbSet.AsNoTracking();

        if (options.Predicate != null)
        {
            queryable = queryable.Where(options.Predicate);
        }

        if (options.Includes != null)
        {
            queryable = options.Includes.Aggregate(queryable, (current, include) => current.Include(include));
        }

        return await queryable.Select(options.Selector).FirstOrDefaultAsync(options.CancellationToken);
    }

    public IQueryable<TEntity> Sorting(IQueryable<TEntity> queryable, string? sortColumn, string? sortOrder)
    {
        Expression<Func<TEntity, object>> keySelector = sortColumn?.ToLower() switch
        {
            "created_at" => entity => ((BaseEntity)(object)entity).CreatedAt,
            "updated_at" => entity => ((BaseEntity)(object)entity).UpdatedAt,
            _ => entity => ((BaseEntity)(object)entity).RowId
        };

        return sortOrder == "desc"
            ? queryable.OrderByDescending(keySelector)
            : queryable.OrderBy(keySelector);
    }

    public async Task<IPagedList<TResult>> GetPagedListAsync<TResult>(QueryOptions<TEntity, TResult> options)
    {
        var queryable = DbSet.AsNoTracking();

        // has filter
        if (options.Predicate != null)
        {
            queryable = queryable.Where(options.Predicate);
        }

        // has includes
        if (options.Includes != null)
        {
            queryable = options.Includes.Aggregate(queryable, (current, include) => current.Include(include));
        }

        // sorting
        queryable = Sorting(queryable, options.SortColumn, options.SortOrder);

        return await queryable
            .Select(options.Selector)
            .ToPagedListAsync(options.Page, options.PageSize);
    }

    public async Task<List<TResult>> GetMultipleAsync<TResult>(QueryOptions<TEntity, TResult> options)
    {
        var queryable = DbSet.AsNoTracking();

        // has filter
        if (options.Predicate != null)
        {
            queryable = queryable.Where(options.Predicate);
        }

        // has includes
        if (options.Includes != null)
        {
            queryable = options.Includes.Aggregate(queryable, (current, include) => current.Include(include));
        }

        // sorting
        queryable = Sorting(queryable, options.SortColumn, options.SortOrder);

        return await queryable
            .Select(options.Selector)
            .ToListAsync(options.CancellationToken);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return DbSet;
    }

    public void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void Delete(object id)
    {
        var entityToDelete = DbSet.Find(id);
        if (entityToDelete is null)
        {
            return;
        }
        Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }
        DbSet.Remove(entityToDelete);
    }

    public void Update(TEntity entityToUpdate)
    {
        DbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}