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

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, SubscriptionHealthCheckOptions options, IServiceBusManagementClient managementClient, CancellationToken cancellationToken)
        {
            try
            {
                var queue = await managementClient.Subscriptions.GetAsync("", "", options.TopicName, options.SubscriptionName, cancellationToken); // TODO :: expose first two properties

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
