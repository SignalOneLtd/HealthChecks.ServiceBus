using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Management
{
    internal sealed class ServiceBusManagementClientFactory : IServiceBusManagementClientFactory
    {
        public IServiceBusManagementClient CreateClient(HealthCheckOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return new ServiceBusManagementClient(options.BaseUri, options.ServiceCredentials);
        }
    }
}
