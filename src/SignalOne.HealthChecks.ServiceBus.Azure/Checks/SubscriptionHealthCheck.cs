using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks
{
    internal sealed class SubscriptionHealthCheck : HealthCheckBase
    {
        private readonly IOptionsSnapshot<SubscriptionHealthCheckOptions> _optionsSnapshot;

        public SubscriptionHealthCheck(IOptionsSnapshot<SubscriptionHealthCheckOptions> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot ?? throw new ArgumentNullException(nameof(optionsSnapshot));
        }

        protected override Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
