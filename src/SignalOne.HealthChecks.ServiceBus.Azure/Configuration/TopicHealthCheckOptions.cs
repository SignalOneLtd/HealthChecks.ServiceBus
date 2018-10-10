namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Configuration that is specific to a Topic.
    /// </summary>
    public class TopicHealthCheckOptions : HealthCheckOptions
    {
        /// <summary>
        /// The name of the topic.
        /// </summary>
        public string TopicName { get; internal set; }
    }
}
