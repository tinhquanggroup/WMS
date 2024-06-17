using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WMS.WF.Infrastructure.Configurations;
using WMS.WF.Infrastructure.Services;

namespace WMS.WF.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            services.AddHttpClient<IApiClient, ApiClient>(client =>
            {
                var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
                if (apiSettings != null) client.BaseAddress = new Uri(apiSettings.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
    }
}
