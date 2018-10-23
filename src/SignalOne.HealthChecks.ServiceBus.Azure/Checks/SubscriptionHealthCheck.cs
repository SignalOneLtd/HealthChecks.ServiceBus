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
    internal sealed class SubscriptionHealthCheck : HealthCheckBase<SubscriptionHealthCheckOptions>
    {
        private readonly IEnumerable<ISubscriptionRule> _subscriptionRules;

        public SubscriptionHealthCheck(IOptionsSnapshot<SubscriptionHealthCheckOptions> optionsSnapshot,
            IServiceBusManagementClientFactory managementClientFactory,
            IEnumerable<ISubscriptionRule> subscriptionRules)
            : base(optionsSnapshot, managementClientFactory)
        {
            _subscriptionRules = subscriptionRules ?? throw new ArgumentNullException(nameof(subscriptionRules));
        }

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, SubscriptionHealthCheckOptions options, IServiceBusNamespace client, CancellationToken cancellationToken)
        {
            try
            {
                var topic = await client.Topics.GetByNameAsync(options.TopicName, cancellationToken);
                var subscription = await topic.Subscriptions.GetByNameAsync(options.SubscriptionName, cancellationToken);

                IReadOnlyDictionary<string, object> errors = _subscriptionRules.SelectMany(x => x.ValidateResource(subscription, options)).ToDictionary(x => x.Key, x => (object)x.Error);

                if (errors.Count > 0)
                    return HealthCheckResult.Failed($"Resource '{options.TopicName}/{options.SubscriptionName}' was found but failed to validate. See the data properties for the list of errors.", data: errors);

                return HealthCheckResult.Passed($"Resource '{options.TopicName}/{options.SubscriptionName}' was found and validated successfully");
            }
            catch (Exception ex)
            {
                // TODO :: exception handling
                return HealthCheckResult.Failed(ex.Message, ex);
            }
        }
    }
}
