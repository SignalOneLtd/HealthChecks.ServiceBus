namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Options specific to a queue
    /// </summary>
    public class QueueHealthCheckOptions : HealthCheckOptions
    {
        /// <summary>
        /// The queue name that these settings refer to...
        /// </summary>
        public string QueueName { get; set; }
    }
}
