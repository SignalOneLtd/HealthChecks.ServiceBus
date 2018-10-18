using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

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
        /// The name of the service bus namespace
        /// </summary>
        public string Namespace { get; set; }
    }
}
