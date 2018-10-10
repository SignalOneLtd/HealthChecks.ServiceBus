using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;

namespace SignalOne.HealthChecks.ServiceBus.Azure
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddAzureServiceBusDefaultServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IPostConfigureOptions<QueueHealthCheckOptions>, DefaultOptionsConfigurationProvider>();
            services.TryAddSingleton<IPostConfigureOptions<TopicHealthCheckOptions>, DefaultOptionsConfigurationProvider>();
            services.TryAddSingleton<IPostConfigureOptions<SubscriptionHealthCheckOptions>, DefaultOptionsConfigurationProvider>();

            return services;
        }
    }
}
