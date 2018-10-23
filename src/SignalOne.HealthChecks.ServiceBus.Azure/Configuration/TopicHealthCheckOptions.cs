using Microsoft.Azure.Management.ServiceBus.Fluent.Models;
using System;

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
        /// <summary>
        ///     Gets the duration after which the message expires, starting from when the message is sent to topic.
        /// </summary>
        public TimeSpan? DefaultMessageTtlDuration { get; set; }
        /// <summary>
        ///     Gets the idle duration after which the topic is automatically deleted.
        /// </summary>
        public long? DeleteOnIdleDurationInMinutes { get; set; }
        /// <summary>
        ///     Gets indicates whether the topic is to be partitioned across multiple message brokers.
        /// </summary>
        public bool? IsPartitioningEnabled { get; set; }
        /// <summary>
        ///     Gets number of subscriptions for the topic.
        /// </summary>
        public int? SubscriptionCount { get; set; }
        /// <summary>
        ///     Gets indicates if this topic requires duplicate detection.
        /// </summary>
        public bool? IsDuplicateDetectionEnabled { get; set; }
        /// <summary>
        ///     Gets the maximum size of memory allocated for the topic in megabytes.
        /// </summary>
        public long? MaxSizeInMB { get; set; }
        /// <summary>
        ///     Gets indicates whether server-side batched operations are enabled.
        /// </summary>
        public bool? IsBatchedOperationsEnabled { get; set; }
        /// <summary>
        ///     Gets the current status of the topic.
        /// </summary>
        public EntityStatus? Status { get; set; }
        /// <summary>
        ///     Gets indicates whether express entities are enabled.
        /// </summary>
        public bool? IsExpressEnabled { get; set; }
        /// <summary>
        ///     Gets the duration of the duplicate detection history.
        /// </summary>
        public TimeSpan? DuplicateMessageDetectionHistoryDuration { get; set; }
    }
}
