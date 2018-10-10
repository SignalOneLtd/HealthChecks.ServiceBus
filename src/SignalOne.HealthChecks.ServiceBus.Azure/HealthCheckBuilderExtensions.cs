using SignalOne.HealthChecks.ServiceBus.Azure.Checks;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions for the ASP.Net core diagnostics health check builder
    /// </summary>
    public static class HealthCheckBuilderExtensions
    {
        /// <summary>
        /// Configures the default options when they are not supplied during the setup of each health check.
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder.</param>
        /// <param name="defaultSetup">The action which sets up the default options.</param>
        public static IHealthChecksBuilder AddAzureServiceBusDefaults(this IHealthChecksBuilder builder, Action<HealthCheckOptions> defaultSetup)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (defaultSetup == null)
                throw new ArgumentNullException(nameof(defaultSetup));

            builder.Services.Configure(defaultSetup);

            return builder;
        }

        /// <summary>
        /// Adds a health check to see whether the specified queue exists, and optionally, checking configuration details.
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder.</param>
        /// <param name="queueName">The queue to be checked.</param>
        /// <param name="requiredConfiguration">Setup for the options specific to this queue.</param>
        public static IHealthChecksBuilder AddAzureServiceBusQueueCheck(this IHealthChecksBuilder builder, string queueName, Action<QueueHealthCheckOptions> requiredConfiguration = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(queueName))
                throw new ArgumentNullException(nameof(queueName));

            builder.Services.Configure<QueueHealthCheckOptions>(queueName, config => // TODO :: Convert others to this style & verify tests...
            {
                config.QueueName = queueName;
                requiredConfiguration?.Invoke(config);
            }); // TODO :: support queues across namespaces

            return builder.AddCheck<QueueHealthCheck>(queueName);
        }

        /// <summary>
        /// Adds a health check to see whether the specified topic exists, and optionally, checking configuration details.
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder.</param>
        /// <param name="topicName">The topic to be checked.</param>
        /// <param name="requiredConfiguration">Setup for the options specific to this topic.</param>
        public static IHealthChecksBuilder AddAzureServiceBusTopicCheck(this IHealthChecksBuilder builder, string topicName, Action<TopicHealthCheckOptions> requiredConfiguration = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(topicName))
                throw new ArgumentNullException(nameof(topicName));

            if (requiredConfiguration != null)
                builder.Services.Configure(topicName, requiredConfiguration); // TODO :: support topics across namespaces

            return builder.AddCheck<TopicHealthCheck>(topicName);
        }

        /// <summary>
        /// Adds a health check to see whether the specified subscription exists for the specified topic, and optionally, checking configuration details.
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder.</param>
        /// <param name="topicName">The topic to be checked.</param>
        /// <param name="subscriptionName">The subscription to be checked.</param>
        /// <param name="requiredConfiguration">Setup for the options specific to this topic &amp; subscription.</param>
        public static IHealthChecksBuilder AddAzureServiceBusSubscriptionCheck(this IHealthChecksBuilder builder, string topicName, string subscriptionName, Action<SubscriptionHealthCheckOptions> requiredConfiguration = null)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(topicName))
                throw new ArgumentNullException(nameof(topicName));

            if (string.IsNullOrWhiteSpace(subscriptionName))
                throw new ArgumentNullException(nameof(subscriptionName));

            var topicSubscriptionName = $"{topicName}/{subscriptionName}";

            if (requiredConfiguration != null)
                builder.Services.Configure(topicSubscriptionName, requiredConfiguration); // TODO :: support topics across namespaces

            return builder.AddCheck<SubscriptionHealthCheck>(topicSubscriptionName);
        }
    }
}
