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
    internal sealed class TopicHealthCheck : HealthCheckBase<TopicHealthCheckOptions>
    {
        private readonly IEnumerable<ITopicRule> _topicRules;

        public TopicHealthCheck(IOptionsSnapshot<TopicHealthCheckOptions> optionsSnapshot,
            IServiceBusManagementClientFactory managementClientFactory,
            IEnumerable<ITopicRule> topicRules)
            : base(optionsSnapshot, managementClientFactory)
        {
            _topicRules = topicRules ?? throw new ArgumentNullException(nameof(topicRules));
        }

        protected override async Task<HealthCheckResult> ExecuteHealthCheckAsync(HealthCheckContext context, TopicHealthCheckOptions options, IServiceBusNamespace client, CancellationToken cancellationToken)
        {
            try
            {
                var topic = await client.Topics.GetByNameAsync(options.TopicName, cancellationToken);

                IReadOnlyDictionary<string, object> errors = _topicRules.SelectMany(x => x.ValidateResource(topic, options)).ToDictionary(x => x.Key, x => (object)x.Error);

                if (errors.Count > 0)
                    return HealthCheckResult.Failed($"Resource '{options.TopicName}' was found but failed to validate. See the data properties for the list of errors.", data: errors);

                return HealthCheckResult.Passed($"Resource '{options.TopicName}' was found and validated successfully");
            }
            catch (Exception ex)
            {
                // TODO :: exception handling
                return HealthCheckResult.Failed(ex.Message, ex);
            }
        }
    }
}
