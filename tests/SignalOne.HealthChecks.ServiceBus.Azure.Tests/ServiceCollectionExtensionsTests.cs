using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Linq;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        public class AddAzureServiceBusDefaultServices
        {
            [Fact]
            public void WhenServiceCollectionIsNull_ThrowArgumentNullException()
            {
                IServiceCollection services = null;

                Action act = () => services.AddAzureServiceBusDefaultServices();

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("services");
            }

            [Fact]
            public void ShouldRegisterIPostConfigureOptions_QueueHealthCheckOptions()
            {
                var services = new ServiceCollection().AddAzureServiceBusDefaultServices();

                services.Count(x => x.ServiceType == typeof(IPostConfigureOptions<QueueHealthCheckOptions>)).Should().Be(1);
            }

            [Fact]
            public void ShouldRegisterIPostConfigureOptions_QueueHealthCheckOptions_OnlyOnce()
            {
                var services = new ServiceCollection().AddAzureServiceBusDefaultServices().AddAzureServiceBusDefaultServices();

                services.Count(x => x.ServiceType == typeof(IPostConfigureOptions<QueueHealthCheckOptions>)).Should().Be(1);
            }

            [Fact]
            public void ShouldRegisterIPostConfigureOptions_TopicHealthCheckOptions()
            {
                var services = new ServiceCollection().AddAzureServiceBusDefaultServices();

                services.Count(x => x.ServiceType == typeof(IPostConfigureOptions<TopicHealthCheckOptions>)).Should().Be(1);
            }

            [Fact]
            public void ShouldRegisterIPostConfigureOptions_TopicHealthCheckOptions_OnlyOnce()
            {
                var services = new ServiceCollection().AddAzureServiceBusDefaultServices().AddAzureServiceBusDefaultServices();

                services.Count(x => x.ServiceType == typeof(IPostConfigureOptions<TopicHealthCheckOptions>)).Should().Be(1);
            }

            [Fact]
            public void ShouldRegisterIPostConfigureOptions_SubscriptionHealthCheckOptions()
            {
                var services = new ServiceCollection().AddAzureServiceBusDefaultServices();

                services.Count(x => x.ServiceType == typeof(IPostConfigureOptions<SubscriptionHealthCheckOptions>)).Should().Be(1);
            }

            [Fact]
            public void ShouldRegisterIPostConfigureOptions_SubscriptionHealthCheckOptions_OnlyOnce()
            {
                var services = new ServiceCollection().AddAzureServiceBusDefaultServices().AddAzureServiceBusDefaultServices();

                services.Count(x => x.ServiceType == typeof(IPostConfigureOptions<SubscriptionHealthCheckOptions>)).Should().Be(1);
            }
        }
    }
}
