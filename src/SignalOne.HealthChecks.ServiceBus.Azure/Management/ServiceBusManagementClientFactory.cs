using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Linq;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Management
{
    internal sealed class ServiceBusManagementClientFactory : IServiceBusManagementClientFactory
    {
        public IServiceBusNamespace CreateClient(HealthCheckOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var azureClient = Microsoft.Azure.Management.Fluent.Azure
                                                        .Configure()
                                                        .Authenticate(options.ServiceCredentials)
                                                        .WithDefaultSubscription();

            var sbNamespace = azureClient.ServiceBusNamespaces.List().FirstOrDefault(x => x.Name.Equals(options.Namespace, StringComparison.OrdinalIgnoreCase));

            if (sbNamespace == null)
                throw new Exception($"Unable to locate service by namespace: '{options.Namespace}'");

            return sbNamespace;
        }
    }
}