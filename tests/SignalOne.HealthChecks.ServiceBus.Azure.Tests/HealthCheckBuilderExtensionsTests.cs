using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests
{
    public class HealthCheckBuilderExtensionsTests
    {

        public class AddAzureServiceBusQueueCheck : HealthCheckBuilderTestSuite
        {
            private const string QueueName = "testing";

            [Fact]
            public void WhenHealthCheckBuilderIsNull_ThrowArgumentNullException()
            {
                IHealthChecksBuilder builder = null;

                Action act = () => builder.AddAzureServiceBusQueueCheck(QueueName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("builder");
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void WhenQueueNameIsNull_ThrowArgumentNullException(string queueName)
            {
                Action act = () => Act(builder => builder.AddAzureServiceBusQueueCheck(queueName));

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("queueName");
            }

            [Fact]
            public void WhenQueueNameIsValid_EnsureItWasRegisteredInTheBuilder()
            {
                SetupVerifibleHealthCheckRegistration(check => check.Name == QueueName);

                Act(builder => builder.AddAzureServiceBusQueueCheck(QueueName));

                VerifyBuilder();
            }

            [Fact]
            public void WhenQueueNameIsValid_AndConfigurationSupplied_SetupNamedOptionsForQueueHealthCheck()
            {
                SetupVerifibleOptions<QueueHealthCheckOptions>();
                SetupVerifibleHealthCheckRegistration(check => check.Name == QueueName);

                Act(builder => builder.AddAzureServiceBusQueueCheck(QueueName, config => { }));

                VerifyAll();
            }

            //TODO : Review due to flakey test
            //[Fact]
            //public void WhenQueueNameIsValid_AndConfigurationIsNotSupplied_SetupNamedOptionsForQueueHealthCheck()
            //{
            //    // Arrange
            //    var services = new Mock<IServiceCollection>();

            //    _builder.Setup(x => x.Add(It.Is<HealthCheckRegistration>(obj => obj.Name == QueueName))).Returns(_builder.Object).Verifiable();
            //    _builder.Setup(x => x.Services).Returns(services.Object);

            //    // Act
            //    _builder.Object.AddAzureServiceBusQueueCheck(QueueName);

            //    // Verify
            //    services.Verify(x => x.Add(It.Is<ServiceDescriptor>(sd => sd.ServiceType.Equals(typeof(IConfigureOptions<QueueHealthCheckOptions>)) && sd.Lifetime == ServiceLifetime.Singleton)), Times.Never);
            //}
        }

        public class AddAzureServiceBusTopicCheck : HealthCheckBuilderTestSuite
        {
            private const string TopicName = "testing";

            [Fact]
            public void WhenHealthCheckBuilderIsNull_ThrowArgumentNullException()
            {
                IHealthChecksBuilder builder = null;

                Action act = () => builder.AddAzureServiceBusTopicCheck(TopicName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("builder");
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void WhenTopicNameIsNull_ThrowArgumentNullException(string topicName)
            {
                Action act = () => Builder.Object.AddAzureServiceBusTopicCheck(topicName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("topicName");
            }

            [Fact]
            public void WhenTopicNameIsValid_EnsureItWasRegisteredInTheBuilder()
            {
                SetupVerifibleHealthCheckRegistration(check => check.Name == TopicName);

                Act(builder => builder.AddAzureServiceBusTopicCheck(TopicName));

                VerifyBuilder();
            }

            [Fact]
            public void WhenTopicNameIsValid_AndConfigurationSupplied_SetupNamedOptionsForTopicHealthCheck()
            {
                SetupVerifibleOptions<TopicHealthCheckOptions>();
                SetupVerifibleHealthCheckRegistration(check => check.Name == TopicName);

                Act(builder => builder.AddAzureServiceBusTopicCheck(TopicName, config => { }));

                VerifyAll();
            }

            //TODO : Review due to flakey test
            //[Fact]
            //public void WhenTopicNameIsValid_AndConfigurationIsNotSupplied_SetupNamedOptionsForTopicHealthCheck()
            //{
            //    // Arrange
            //    var services = new Mock<IServiceCollection>();

            //    _builder.Setup(x => x.Add(It.Is<HealthCheckRegistration>(obj => obj.Name == TopicName))).Returns(_builder.Object).Verifiable();
            //    _builder.Setup(x => x.Services).Returns(services.Object);

            //    // Act
            //    _builder.Object.AddAzureServiceBusTopicCheck(TopicName);

            //    // Verify
            //    services.Verify(x => x.Add(It.Is<ServiceDescriptor>(sd => sd.ServiceType.Equals(typeof(IConfigureOptions<TopicHealthCheckOptions>)) && sd.Lifetime == ServiceLifetime.Singleton)), Times.Never);
            //}

        }

        public class AddAzureServiceBusSubscriptionCheck : HealthCheckBuilderTestSuite
        {
            private const string TopicName = "testing";
            private const string SubscriptionName = "testing";

            [Fact]
            public void WhenHealthCheckBuilderIsNull_ThrowArgumentNullException()
            {
                IHealthChecksBuilder builder = null;

                Action act = () => builder.AddAzureServiceBusSubscriptionCheck(TopicName, SubscriptionName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("builder");
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void WhenTopicNameIsNull_ThrowArgumentNullException(string topicName)
            {
                Action act = () => Builder.Object.AddAzureServiceBusSubscriptionCheck(topicName, SubscriptionName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("topicName");
            }
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void WhenSubscriptionNameIsNull_ThrowArgumentNullException(string subscriptionName)
            {
                Action act = () => Builder.Object.AddAzureServiceBusSubscriptionCheck(TopicName, subscriptionName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("subscriptionName");
            }


            [Fact]
            public void WhenTopicNameAndSubscriptionNameIsValid_EnsureItWasRegisteredInTheBuilder()
            {
                SetupVerifibleHealthCheckRegistration(check => check.Name == $"{TopicName}/{SubscriptionName}");

                Act(builder => builder.AddAzureServiceBusSubscriptionCheck(TopicName, SubscriptionName));

                VerifyBuilder();
            }

            [Fact]
            public void WhenTopicNameAndSubscriptionNameIsValid_AndConfigurationSupplied_SetupNamedOptionsForSubscriptionHealthCheck()
            {
                SetupVerifibleOptions<SubscriptionHealthCheckOptions>();
                SetupVerifibleHealthCheckRegistration(check => check.Name == $"{TopicName}/{SubscriptionName}");

                Act(builder => builder.AddAzureServiceBusSubscriptionCheck(TopicName, SubscriptionName, config => { }));

                VerifyAll();
            }
        }

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
}
