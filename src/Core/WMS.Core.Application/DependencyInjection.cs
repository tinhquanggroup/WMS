using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using WMS.Core.Application.Behaviors;

namespace WMS.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();

                //config.NotificationPublisher = new TaskWhenAllPublisher();
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
