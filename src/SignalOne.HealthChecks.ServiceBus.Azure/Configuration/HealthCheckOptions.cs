using Microsoft.Rest;
using System;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Configuration
{
    /// <summary>
    /// Configure Global Options
    /// </summary>
    public class HealthCheckOptions
    {
        /// <summary>
        /// The base Azure Uri.
        /// </summary>
        public Uri BaseUri { get; set; }
        /// <summary>
        /// Credentials needed for the client to connect to Azure.
        /// </summary>
        public ServiceClientCredentials ServiceCredentials { get; set; }
    }
}
