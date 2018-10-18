using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using SignalOne.HealthChecks.ServiceBus.Azure.Management;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks
{
    internal sealed class QueueHealthCheck : HealthCheckBase<QueueHealthCheckOptions>
    {
        public QueueHealthCheck(IOptionsSnapshot<QueueHealthCheckOptions> optionsSnapshot, IServiceBusManagementClientFactory managementClientFactory)
            : base(optionsSnapshot, managementClientFactory)
        {
        }

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, QueueHealthCheckOptions options, IServiceBusNamespace client, CancellationToken cancellationToken)
        {
            try
            {
                var queue = await client.Queues.GetByNameAsync(options.QueueName, cancellationToken);
                
                // TODO :: validate additional configuration

                return HealthCheckResult.Passed($"Resource '{options.QueueName}' found");
            }
            catch (Exception ex)
            {
                // TODO :: exception handling
                return HealthCheckResult.Failed(ex.Message, ex);
            }
        }
    }
}
