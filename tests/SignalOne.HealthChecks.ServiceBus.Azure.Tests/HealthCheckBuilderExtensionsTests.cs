using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using SignalOne.HealthChecks.ServiceBus.TestHelpers;
using System;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests
{
    public class HealthCheckBuilderExtensionsTests
    {
        public class AddAzureServiceBusDefaults : HealthCheckBuilderTestSuite
        {
            [Fact]
            public void WhenHealthCheckBuilderIsNull_ThrowArgumentNullException()
            {
                IHealthChecksBuilder builder = null;

                Action act = () => builder.AddAzureServiceBusDefaults(options => { });

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("builder");
            }

            [Fact]
            public void WhenDefaultSetupIsNull_ThrowArgumentNullException()
            {
                Action act = () => Builder.Object.AddAzureServiceBusDefaults(null);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("defaultSetup");
            }

            [Fact]
            public void WhenDefaultSetupIsNotNull_DoesNotThrow()
            {
                Action act = () => Builder.Object.AddAzureServiceBusDefaults(options => { });

                act.Should().NotThrow();
            }
        }

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
                Action act = () => Builder.Object.AddAzureServiceBusQueueCheck(queueName);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("queueName");
            }

            [Fact]
            public void WhenQueueNameIsValid_RegisterQueueHealthCheck()
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
            public void WhenTopicNameIsValid_RegisterTopicHealthCheck()
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
            public void WhenTopicNameAndSubscriptionNameIsValid_RegisterSubscriptionHealthCheck()
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
    }
}
