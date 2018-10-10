using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Management
{
    internal interface IServiceBusManagementClientFactory
    {
        IServiceBusManagementClient CreateClient(HealthCheckOptions options);
    }
}
