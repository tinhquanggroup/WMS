namespace WMS.Core.Domain.Shared.Pagination;

public interface IPagedList<T>
{
    List<T> Items { get; }
    int Page { get; }
    int PageSize { get; }
    int TotalCount { get; }
    bool HasNextPage { get; }
    bool HasPreviousPage { get; }
}