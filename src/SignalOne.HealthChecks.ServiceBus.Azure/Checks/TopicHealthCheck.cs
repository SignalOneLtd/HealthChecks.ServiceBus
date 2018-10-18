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
    internal sealed class TopicHealthCheck : HealthCheckBase<TopicHealthCheckOptions>
    {
        public TopicHealthCheck(IOptionsSnapshot<TopicHealthCheckOptions> optionsSnapshot, IServiceBusManagementClientFactory managementClientFactory)
            : base(optionsSnapshot, managementClientFactory)
        {
        }

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, TopicHealthCheckOptions options, IServiceBusNamespace client, CancellationToken cancellationToken)
        {
            try
            {
                var topic = await client.Topics.GetByNameAsync(options.TopicName, cancellationToken);

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
