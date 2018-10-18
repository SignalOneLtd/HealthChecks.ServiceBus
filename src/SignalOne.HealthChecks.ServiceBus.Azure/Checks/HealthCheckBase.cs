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
    internal abstract class HealthCheckBase<T> : IHealthCheck
        where T : HealthCheckOptions, new()
    {
        private readonly IOptionsSnapshot<T> _optionsSnapshot;
        private readonly IServiceBusManagementClientFactory _managementClientFactory;

        public HealthCheckBase(IOptionsSnapshot<T> optionsSnapshot, IServiceBusManagementClientFactory managementClientFactory)
        {
            _optionsSnapshot = optionsSnapshot ?? throw new ArgumentNullException(nameof(optionsSnapshot));
            _managementClientFactory = managementClientFactory ?? throw new ArgumentNullException(nameof(managementClientFactory));
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            cancellationToken.ThrowIfCancellationRequested();

            var checkOptions = _optionsSnapshot.Get(context.Registration.Name);

            return ExecuteHealthCheckAsync(context, checkOptions, _managementClientFactory.CreateClient(checkOptions), cancellationToken);
        }

        protected abstract Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, T options, IServiceBusNamespace client, CancellationToken cancellationToken);
    }
}
