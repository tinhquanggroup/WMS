using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WMS.Core.Infrastructure.Data.EFContext;
using WMS.Core.Infrastructure.Data.Uow;

namespace WMS.Core.Infrastructure
{
    public static class DependencyInjection
    {
        private const string SqlConnectionStringName = "WMS_Database";

        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var sqlConnection = configuration.GetConnectionString(SqlConnectionStringName);
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(sqlConnection));

            services.AddScoped<IApplicationDbContext>(sp =>
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
