using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Configuration settings that apply across all entities.
    /// </summary>
    public class HealthCheckOptions
    {
        /// <summary>
        /// Credentials needed for the client to connect to the Azure Namespace.
        /// </summary>
        public AzureCredentials ServiceCredentials { get; set; }
        /// <summary>
        /// The name or id of the service bus namespace
        /// </summary>
        public string Namespace { get; set; }
        /// <summary>
        /// The subscription id to use when connected to Azure. Defaults your default Azure subscription if not supplied.
        /// </summary>
        public string SubscriptionId { get; set; }
        /// <summary>
        /// The level of logging to have
        /// </summary>
        public HttpLoggingDelegatingHandler.Level? LogLevel { get; set; }
        /// <summary>
        /// The product to be used in the user agent string. Must be combined with Version.
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// The version to be used in the user agent string. Must be combined with Product.
        /// </summary>
        public string Version { get; set; }
    }
}
