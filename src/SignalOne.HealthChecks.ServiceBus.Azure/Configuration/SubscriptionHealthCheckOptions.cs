namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Configuration that is specific to a Subscription.
    /// </summary>
    public class SubscriptionHealthCheckOptions : HealthCheckOptions
    {
        /// <summary>
        /// The name of the topic to which the subscription belongs.
        /// </summary>
        public string TopicName { get; internal set; }
        /// <summary>
        /// The name of the subscription.
        /// </summary>
        public string SubscriptionName { get; internal set; }
    }
}
