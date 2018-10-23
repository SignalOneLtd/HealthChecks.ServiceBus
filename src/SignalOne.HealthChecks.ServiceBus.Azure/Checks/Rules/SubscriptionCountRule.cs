using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Collections.Generic;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules
{
    internal sealed class SubscriptionCountRule : ITopicRule
    {
        public IEnumerable<(string Key, string Error)> ValidateResource(ITopic resource, TopicHealthCheckOptions options)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (!options.SubscriptionCount.HasValue)
                yield break;

            if (resource.SubscriptionCount != options.SubscriptionCount.Value)
                yield return (nameof(options.SubscriptionCount), $"Expected Value: '{options.SubscriptionCount}', Actual Value: '{resource.SubscriptionCount}'");
        }
    }
}
