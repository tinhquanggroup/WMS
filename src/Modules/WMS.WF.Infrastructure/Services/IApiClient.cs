namespace WMS.WF.Infrastructure.Services;

public interface IApiClient
{
    Task<T?> GetSingleAsync<T>(string requestUri);
}