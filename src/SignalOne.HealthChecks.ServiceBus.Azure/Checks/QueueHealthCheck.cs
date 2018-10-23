using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using SignalOne.HealthChecks.ServiceBus.Azure.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks
{
    internal sealed class QueueHealthCheck : HealthCheckBase<QueueHealthCheckOptions>
    {
        private readonly IEnumerable<IQueueRule> _queueRules;

        public QueueHealthCheck(IOptionsSnapshot<QueueHealthCheckOptions> optionsSnapshot,
            IServiceBusManagementClientFactory managementClientFactory,
            IEnumerable<IQueueRule> queueRules)
            : base(optionsSnapshot, managementClientFactory)
        {
            _queueRules = queueRules ?? throw new ArgumentNullException(nameof(queueRules));
        }

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, QueueHealthCheckOptions options, IServiceBusNamespace client, CancellationToken cancellationToken)
        {
            try
            {
                var queue = await client.Queues.GetByNameAsync(options.QueueName, cancellationToken);

                IReadOnlyDictionary<string, object> errors = _queueRules.SelectMany(x => x.ValidateResource(queue, options)).ToDictionary(x => x.Key, x => (object)x.Error);

                if (errors.Count > 0)
                    return HealthCheckResult.Failed($"Resource '{options.QueueName}' was found but failed to validate. See the data properties for the list of errors.", data: errors);

                return HealthCheckResult.Passed($"Resource '{options.QueueName}' was found and validated successfully");
            }
            catch (Exception ex)
            {
                // TODO :: exception handling
                return HealthCheckResult.Failed(ex.Message, ex);
            }
        }
    }
}
