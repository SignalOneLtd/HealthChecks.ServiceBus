using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using SignalOne.HealthChecks.ServiceBus.Azure.Management;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalOne.HealthChecks.ServiceBus.Azure
{
    internal static class ServiceCollectionExtensions
    {
        private static Type QueueRuleType = typeof(IQueueRule);
        private static Type TopicRuleType = typeof(ITopicRule);
        private static Type SubscriptionRuleType = typeof(ISubscriptionRule);

        internal static IServiceCollection AddAzureServiceBusDefaultServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAddSingleton<IPostConfigureOptions<QueueHealthCheckOptions>, DefaultOptionsConfigurationProvider>();
            services.TryAddSingleton<IPostConfigureOptions<TopicHealthCheckOptions>, DefaultOptionsConfigurationProvider>();
            services.TryAddSingleton<IPostConfigureOptions<SubscriptionHealthCheckOptions>, DefaultOptionsConfigurationProvider>();
            services.TryAddSingleton<IServiceBusManagementClientFactory, ServiceBusManagementClientFactory>();

            return services.TryAddQueueRules().TryAddTopicRules().TryAddSubscriptionRules();
        }

        internal static IServiceCollection TryAddQueueRules(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (services.Any(x => x.ServiceType == QueueRuleType))
                return services;

            LocateTypesOfInAssembly(QueueRuleType).ForEach(t => services.Add(new ServiceDescriptor(QueueRuleType, t, ServiceLifetime.Singleton)));

            return services;
        }

        internal static IServiceCollection TryAddTopicRules(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (services.Any(x => x.ServiceType == TopicRuleType))
                return services;

            LocateTypesOfInAssembly(TopicRuleType).ForEach(t => services.Add(new ServiceDescriptor(TopicRuleType, t, ServiceLifetime.Singleton)));

            return services;
        }


        internal static IServiceCollection TryAddSubscriptionRules(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (services.Any(x => x.ServiceType == SubscriptionRuleType))
                return services;

            LocateTypesOfInAssembly(SubscriptionRuleType).ForEach(t => services.Add(new ServiceDescriptor(SubscriptionRuleType, t, ServiceLifetime.Singleton)));

            return services;
        }

        internal static List<Type> LocateTypesOfInAssembly(Type type) => type.Assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && !x.IsInterface && type.IsAssignableFrom(x)).ToList();
    }
}
