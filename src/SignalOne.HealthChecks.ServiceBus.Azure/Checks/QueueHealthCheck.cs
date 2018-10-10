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

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, QueueHealthCheckOptions options, IServiceBusManagementClient managementClient, CancellationToken cancellationToken)
        {
            try
            {
                var queue = await managementClient.Queues.GetAsync(options.ResourceGroupName, options.Namespace, options.QueueName, cancellationToken); // TODO :: expose first two properties

                // TODO :: validate additional configuration

                return HealthCheckResult.Passed("");
            }
            catch (Exception ex)
            {
                // TODO :: exception handling
                return HealthCheckResult.Failed("", ex);
            }
        }
    }
}
