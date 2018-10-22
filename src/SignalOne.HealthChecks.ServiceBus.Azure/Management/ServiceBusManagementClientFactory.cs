using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Linq;
using AzureClient = Microsoft.Azure.Management.Fluent.Azure;
using IAzureClient = Microsoft.Azure.Management.Fluent.IAzure;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Management
{
    internal sealed class ServiceBusManagementClientFactory : IServiceBusManagementClientFactory
    {
        public IServiceBusNamespace CreateClient(HealthCheckOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var azureClient = AzureClient.Configure()
                                         .WithLogLevel(options)
                                         .WithUserAgent(options)
                                         .Authenticate(options.ServiceCredentials)
                                         .WithSubscription(options);

            // TODO :: Use Async Overloads
            IServiceBusNamespace sbNamespace;
            if (Guid.TryParse(options.Namespace, out var _))
                sbNamespace = azureClient.ServiceBusNamespaces.GetById(options.Namespace);
            else
                sbNamespace = azureClient.ServiceBusNamespaces.List().FirstOrDefault(x => x.Name.Equals(options.Namespace, StringComparison.OrdinalIgnoreCase)); ;

            if (sbNamespace == null)
                throw new Exception($"Unable to locate service by namespace: '{options.Namespace}'");

            return sbNamespace;
        }
    }

    internal static class AzureExtensions
    {
        internal static AzureClient.IConfigurable WithLogLevel(this AzureClient.IConfigurable azure, HealthCheckOptions options)
        {
            if (options.LogLevel == null)
                return azure;

            return azure.WithLogLevel(options.LogLevel.Value);
        }

        internal static AzureClient.IConfigurable WithUserAgent(this AzureClient.IConfigurable azure, HealthCheckOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Product) || string.IsNullOrWhiteSpace(options.Version))
                return azure;

            return azure.WithUserAgent(options.Product, options.Version);
        }

        internal static IAzureClient WithSubscription(this AzureClient.IAuthenticated azure, HealthCheckOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.SubscriptionId))
                return azure.WithDefaultSubscription();

            return azure.WithSubscription(options.SubscriptionId);
        }
    }
}