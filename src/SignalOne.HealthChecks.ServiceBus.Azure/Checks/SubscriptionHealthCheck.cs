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
    internal sealed class SubscriptionHealthCheck : HealthCheckBase<SubscriptionHealthCheckOptions>
    {
        public SubscriptionHealthCheck(IOptionsSnapshot<SubscriptionHealthCheckOptions> optionsSnapshot, IServiceBusManagementClientFactory managementClientFactory)
            : base(optionsSnapshot, managementClientFactory)
        {
        }

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, SubscriptionHealthCheckOptions options, IServiceBusNamespace client, CancellationToken cancellationToken)
        {
            try
            {
                var subscription = await (await client.Topics.GetByNameAsync(options.TopicName, cancellationToken)).Subscriptions.GetByNameAsync(options.SubscriptionName, cancellationToken);

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
