using FluentAssertions;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using System.Linq;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests.Checks.Rules
{
    public class SubscriptionCountRuleTests
    {
        public class WhenTopic
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new SubscriptionCountRule();

                Action act = () => target.ValidateResource(default, new TopicHealthCheckOptions()).ToList();

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new SubscriptionCountRule();

                Action act = () => target.ValidateResource(new Mock<ITopic>().Object, default).ToList();

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new SubscriptionCountRule();
                var topic = new Mock<ITopic>();

                target.ValidateResource(topic.Object, new TopicHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new SubscriptionCountRule();
                var topic = new Mock<ITopic>();
                var count = 1;
                topic.Setup(x => x.SubscriptionCount).Returns(() => count).Verifiable();

                target.ValidateResource(topic.Object, new TopicHealthCheckOptions { SubscriptionCount = count }).Should().HaveCount(0);

                topic.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new SubscriptionCountRule();
                var topic = new Mock<ITopic>();
                topic.Setup(x => x.SubscriptionCount).Returns(() => 1).Verifiable();

                target.ValidateResource(topic.Object, new TopicHealthCheckOptions { SubscriptionCount = 2 }).Should().HaveCount(1);

                topic.Verify();
            }
        }
    }
}
