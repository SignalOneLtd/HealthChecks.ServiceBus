namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Configuration that is specific to a Queue.
    /// </summary>
    public class QueueHealthCheckOptions : HealthCheckOptions
    {
        /// <summary>
        /// The name of the queue.
        /// </summary>
        public string QueueName { get; internal set; }
    }
}
