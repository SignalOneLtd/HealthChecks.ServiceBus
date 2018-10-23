﻿using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules
{
    /// <summary>
    /// A validation rule that is executed against a topic
    /// </summary>
    public interface ITopicRule : IRule<ITopic, TopicHealthCheckOptions>
    {
    }
}
