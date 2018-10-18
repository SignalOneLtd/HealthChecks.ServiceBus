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

            // TODO :: Support alternative subscription id's
            // TODO :: Support UserAgents
            // TODO :: Support Logging Level
            var azureClient = Microsoft.Azure.Management.Fluent.Azure
                                                        .Configure()
                                                        .Authenticate(options.ServiceCredentials)
                                                        .WithDefaultSubscription();

            // TODO :: Support looking up by Namespace ID (GUID)
            // TODO :: Use Async Overloads
            var sbNamespace = azureClient.ServiceBusNamespaces.List().FirstOrDefault(x => x.Name.Equals(options.Namespace, StringComparison.OrdinalIgnoreCase));

            if (sbNamespace == null)
                throw new Exception($"Unable to locate service by namespace: '{options.Namespace}'");

            return sbNamespace;
        }
    }
}