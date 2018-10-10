using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SignalOne.HealthChecks.ServiceBus.TestHelpers
{
    public abstract class HealthCheckBuilderTestSuite
    {
        protected Mock<IServiceCollection> Services { get; } = new Mock<IServiceCollection>();
        protected ICollection<ServiceDescriptor> ServicesSupplied { get; } = new List<ServiceDescriptor>();
        protected Mock<IHealthChecksBuilder> Builder { get; } = new Mock<IHealthChecksBuilder>();

        public HealthCheckBuilderTestSuite()
        {
            Services.Setup(x => x.Add(It.IsAny<ServiceDescriptor>())).Callback((ServiceDescriptor sd) => ServicesSupplied.Add(sd));
            Services.Setup(x => x.GetEnumerator()).Returns(() => ServicesSupplied.GetEnumerator());
            Builder.Setup(x => x.Services).Returns(Services.Object);
        }

        protected void Act(Action<IHealthChecksBuilder> act) => act?.Invoke(Builder.Object);

        protected void SetupVerifibleHealthCheckRegistration(Expression<Func<HealthCheckRegistration, bool>> specificSetup)
        {
            if (specificSetup == null)
                throw new ArgumentNullException(nameof(specificSetup));

            Builder.Setup(x => x.Add(It.Is(specificSetup))).Returns(Builder.Object).Verifiable();
        }
        protected void SetupVerifibleOptions<TOptions>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TOptions : class
        {
            Services.Setup(x => x.Add(It.Is<ServiceDescriptor>(sd => sd.ServiceType.Equals(typeof(IConfigureOptions<TOptions>)) && sd.Lifetime == lifetime))).Verifiable();
        }

        protected void VerifyBuilder() => Builder.Verify();
        protected void VerifyServices() => Services.Verify();
        protected void VerifyAll()
        {
            VerifyBuilder();
            VerifyServices();
        }
    }
}
