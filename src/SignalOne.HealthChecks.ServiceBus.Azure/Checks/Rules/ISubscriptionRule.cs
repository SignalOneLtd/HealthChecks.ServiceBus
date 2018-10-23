using Microsoft.Azure.Management.ServiceBus.Fluent;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules
{
    internal interface ISubscriptionRule : IRule<ISubscription, SubscriptionHealthCheckOptions>
    {
    }
}
