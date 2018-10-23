using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Collections.Generic;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules
{
    internal sealed class IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRule : ISubscriptionRule
    {
        public IEnumerable<(string Key, string Error)> ValidateResource(ISubscription resource, SubscriptionHealthCheckOptions options)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (!options.IsDeadLetteringEnabledForFilterEvaluationFailedMessages.HasValue)
                yield break;

            if (resource.IsDeadLetteringEnabledForFilterEvaluationFailedMessages != options.IsDeadLetteringEnabledForFilterEvaluationFailedMessages.Value)
                yield return (nameof(options.IsDeadLetteringEnabledForFilterEvaluationFailedMessages), $"Expected Value: '{options.IsDeadLetteringEnabledForFilterEvaluationFailedMessages}', Actual Value: '{resource.IsDeadLetteringEnabledForFilterEvaluationFailedMessages}'");
        }
    }
}
