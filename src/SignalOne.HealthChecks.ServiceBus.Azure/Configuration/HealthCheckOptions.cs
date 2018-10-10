using Microsoft.Rest;
using System;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Configuration settings that apply across all entities.
    /// </summary>
    public class HealthCheckOptions
    {
        /// <summary>
        /// The URI of the Azure Namespace.
        /// </summary>
        public Uri BaseUri { get; set; }
        /// <summary>
        /// Credentials needed for the client to connect to the Azure Namespace.
        /// </summary>
        public ServiceClientCredentials ServiceCredentials { get; set; }

        /// <summary>
        /// Name of the Resource group within the Azure subscription.
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// The name of the service bus namespace
        /// </summary>
        public string Namespace { get; set; }
    }
}
