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
        /// Configures the default namespace for all of the service bus checks
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder</param>
        /// <param name="baseUri">The connection string for the namespace to use as the default</param>
        public static IHealthChecksBuilder AddAzureServiceBusDefaultUri(this IHealthChecksBuilder builder, string baseUri)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            if (string.IsNullOrWhiteSpace(baseUri))
                throw new ArgumentNullException(nameof(baseUri));

            builder.Services.Configure<HealthCheckOptions>(options => options.BaseUri = new Uri(baseUri));

            return builder;
        }

        /// <summary>
        /// Adds a check to see whether the queue exists in the specified namespace
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder</param>
        /// <param name="queueName">The queue to be checked</param>
        /// <param name="requiredConfiguration">Options to fail the check</param>
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
        /// Adds a check to see whether the topic exists in the specified namespace
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder</param>
        /// <param name="topicName">The topic to be checked</param>
        /// <param name="requiredConfiguration">Options to fail the check</param>
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
        /// Adds a check to see whether the subscription exists for the specified topic in the specified namespace
        /// </summary>
        /// <param name="builder">The ASP.Net Core health check builder</param>
        /// <param name="topicName">The topic to be checked</param>
        /// <param name="subscriptionName">The subscription to be checked</param>
        /// <param name="requiredConfiguration">Options to fail the check</param>
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
