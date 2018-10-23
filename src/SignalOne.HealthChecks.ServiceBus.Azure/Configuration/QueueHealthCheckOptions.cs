using Microsoft.Azure.Management.ServiceBus.Fluent.Models;
using System;

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
        /// <summary>
        /// Gets the maximum number of a message delivery before marking it as dead-lettered.
        /// </summary>
        public int? MaxDeliveryCountBeforeDeadLetteringMessage { get; set; }
        /// <summary>
        ///     Gets indicates whether the queue is to be partitioned across multiple message brokers.
        /// </summary>
        public bool? IsPartitioningEnabled { get; set; }
        /// <summary>
        ///     Gets the idle duration after which the queue is automatically deleted.
        /// </summary>
        public long? DeleteOnIdleDurationInMinutes { get; set; }
        /// <summary>
        ///     Gets the duration after which the message expires, starting from when the message is sent to queue.
        /// </summary>
        public TimeSpan? DefaultMessageTtlDuration { get; set; }
        /// <summary>
        ///     Gets the duration of the duplicate detection history.
        /// </summary>
        public TimeSpan? DuplicateMessageDetectionHistoryDuration { get; set; }
        /// <summary>
        ///     Gets indicates if this queue requires duplicate detection.
        /// </summary>
        public bool? IsDuplicateDetectionEnabled { get; set; }
        /// <summary>
        ///     Gets indicates whether the queue supports sessions.
        /// </summary>
        public bool? IsSessionEnabled { get; set; }
        /// <summary>
        ///     Gets the maximum size of memory allocated for the queue in megabytes.
        /// </summary>
        public long? MaxSizeInMB { get; set; }
        /// <summary>
        ///     Gets indicates whether server-side batched operations are enabled.
        /// </summary>
        public bool? IsBatchedOperationsEnabled { get; set; }
        /// <summary>
        ///     Gets the current status of the queue.
        /// </summary>
        public EntityStatus? Status { get; set; }
        /// <summary>
        ///     Gets indicates whether express entities are enabled.
        /// </summary>
        public bool? IsExpressEnabled { get; set; }
        /// <summary>
        ///     Gets the duration of peek-lock which is the amount of time that the message is locked for other receivers.
        /// </summary>
        public long? LockDurationInSeconds { get; set; }
        /// <summary>
        ///     Gets indicates whether this queue has dead letter support when a message expires.
        /// </summary>
        public bool? IsDeadLetteringEnabledForExpiredMessages { get; set; }
    }
}
