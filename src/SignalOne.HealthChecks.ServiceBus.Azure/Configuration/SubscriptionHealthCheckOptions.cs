using Microsoft.Azure.Management.ServiceBus.Fluent.Models;
using System;

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
        /// <summary>
        /// Gets the maximum number of a message delivery before marking it as dead-lettered.
        /// </summary>
        public int? MaxDeliveryCountBeforeDeadLetteringMessage { get; set; }
        /// <summary>
        ///     Gets indicates whether this subscription has dead letter support when a message expires.
        /// </summary>
        public bool? IsDeadLetteringEnabledForExpiredMessages { get; set; }
        /// <summary>
        ///     Gets the duration after which the message expires, starting from when the message is sent to subscription.
        /// </summary>
        public TimeSpan? DefaultMessageTtlDuration { get; set; }
        /// <summary>
        ///     Gets the idle duration after which the subscription is automatically deleted.
        /// </summary>
        public long? DeleteOnIdleDurationInMinutes { get; set; }
        /// <summary>
        ///     Gets indicates whether the subscription supports sessions.
        /// </summary>
        public bool? IsSessionEnabled { get; set; }
        /// <summary>
        ///     Gets indicates whether server-side batched operations are enabled.
        /// </summary>
        public bool? IsBatchedOperationsEnabled { get; set; }
        /// <summary>
        ///     Gets the current status of the subscription.
        /// </summary>
        public EntityStatus? Status { get; set; }
        /// <summary>
        ///     Gets the duration of peek-lock which is the amount of time that the message is locked for other receivers.
        /// </summary>
        public long? LockDurationInSeconds { get; set; }
        /// <summary>
        ///     Gets indicates whether subscription has dead letter support on filter evaluation exceptions.
        /// </summary>
        public bool? IsDeadLetteringEnabledForFilterEvaluationFailedMessages { get; set; }
    }
}
