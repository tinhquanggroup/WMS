using System.Text.Json;

namespace WMS.WF.Infrastructure.Services;

public class ApiClient(HttpClient httpClient) : IApiClient
{
    public async Task<T?> GetSingleAsync<T>(string requestUri)
    {
        var response = await httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await JsonSerializer.DeserializeAsync<T>(responseStream, options);
    }

}