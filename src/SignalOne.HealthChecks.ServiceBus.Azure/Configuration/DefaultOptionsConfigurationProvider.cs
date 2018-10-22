using Microsoft.Extensions.Options;
using System;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    internal sealed class DefaultOptionsConfigurationProvider : IPostConfigureOptions<QueueHealthCheckOptions>, IPostConfigureOptions<TopicHealthCheckOptions>, IPostConfigureOptions<SubscriptionHealthCheckOptions>
    {
        private readonly IOptions<HealthCheckOptions> _defaultOptions;

        public DefaultOptionsConfigurationProvider(IOptions<HealthCheckOptions> defaultOptions)
        {
            _defaultOptions = defaultOptions ?? throw new ArgumentNullException(nameof(defaultOptions));
        }

        public void PostConfigure(string name, SubscriptionHealthCheckOptions options)
            => ConfigureDefaults(name, options);

        public void PostConfigure(string name, TopicHealthCheckOptions options)
            => ConfigureDefaults(name, options);

        public void PostConfigure(string name, QueueHealthCheckOptions options)
            => ConfigureDefaults(name, options);

        private void ConfigureDefaults<T>(string name, T options)
            where T : HealthCheckOptions
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var defaults = _defaultOptions.Value;

            if (options.ServiceCredentials == null)
                options.ServiceCredentials = defaults.ServiceCredentials;

            if (options.Namespace == null)
                options.Namespace = defaults.Namespace;

            if (options.LogLevel == null)
                options.LogLevel = defaults.LogLevel;

            if (string.IsNullOrWhiteSpace(options.SubscriptionId))
                options.SubscriptionId = defaults.SubscriptionId;

            if (string.IsNullOrWhiteSpace(options.Version))
                options.Version = defaults.Version;

            if (string.IsNullOrWhiteSpace(options.Product))
                options.Product = defaults.Product;
        }
    }
}
