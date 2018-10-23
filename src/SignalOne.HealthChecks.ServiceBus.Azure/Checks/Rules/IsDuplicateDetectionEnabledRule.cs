using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Collections.Generic;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules
{
    internal sealed class IsDuplicateDetectionEnabledRule : IQueueRule, ITopicRule
    {
        public IEnumerable<(string Key, string Error)> ValidateResource(IQueue resource, QueueHealthCheckOptions options)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return Check(options.IsDuplicateDetectionEnabled, resource.IsDuplicateDetectionEnabled);
        }

        public IEnumerable<(string Key, string Error)> ValidateResource(ITopic resource, TopicHealthCheckOptions options)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return Check(options.IsDuplicateDetectionEnabled, resource.IsDuplicateDetectionEnabled);
        }

        private static IEnumerable<(string Key, string Error)> Check(bool? expected, bool actual)
        {
            if (!expected.HasValue)
                yield break;

            if (!expected.Value.Equals(actual))
                yield return ("IsDuplicateDetectionEnabled", $"Expected Value: '{expected}', Actual Value: '{actual}'");
        }
    }
}
