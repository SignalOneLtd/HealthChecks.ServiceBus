using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core.ResourceActions;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System.Collections.Generic;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules
{
    /// <summary>
    /// A validation rule that can be executed
    /// </summary>
    public interface IRule<in TResource, in TOptions>
                where TResource : IResource, IIndexable, IHasId, IHasName
                where TOptions : HealthCheckOptions, new()
    {
        /// <summary>
        /// Validates the resource according to the specified options
        /// </summary>
        IEnumerable<(string Key, string Error)> ValidateResource(TResource resource, TOptions options);
    }
}
