using FluentAssertions;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests.Checks.Rules
{
    public class LockDurationInSecondsRuleTests
    {
        public class WhenQueue
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new LockDurationInSecondsRule();

                Action act = () => target.ValidateResource(default, new QueueHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new LockDurationInSecondsRule();

                Action act = () => target.ValidateResource(new Mock<IQueue>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new LockDurationInSecondsRule();
                var queue = new Mock<IQueue>();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new LockDurationInSecondsRule();
                var queue = new Mock<IQueue>();
                var time = 1;
                queue.Setup(x => x.LockDurationInSeconds).Returns(() => time).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { LockDurationInSeconds = time }).Should().HaveCount(0);

                queue.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new LockDurationInSecondsRule();
                var queue = new Mock<IQueue>();
                queue.Setup(x => x.LockDurationInSeconds).Returns(() => 1).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { LockDurationInSeconds = 2 }).Should().HaveCount(1);

                queue.Verify();
            }
        }
        
        public class WhenSubscription
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new LockDurationInSecondsRule();

                Action act = () => target.ValidateResource(default, new SubscriptionHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new LockDurationInSecondsRule();

                Action act = () => target.ValidateResource(new Mock<ISubscription>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new LockDurationInSecondsRule();
                var subscription = new Mock<ISubscription>();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new LockDurationInSecondsRule();
                var subscription = new Mock<ISubscription>();
                var time = 1;
                subscription.Setup(x => x.LockDurationInSeconds).Returns(() => time).Verifiable();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions { LockDurationInSeconds = time }).Should().HaveCount(0);

                subscription.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new LockDurationInSecondsRule();
                var subscription = new Mock<ISubscription>();
                subscription.Setup(x => x.LockDurationInSeconds).Returns(() => 1).Verifiable();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions { LockDurationInSeconds = 2 }).Should().HaveCount(1);

                subscription.Verify();
            }
        }
    }
}
