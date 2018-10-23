using FluentAssertions;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests.Checks.Rules
{
    public class DeleteOnIdleDurationInMinutesRuleTests
    {
        public class WhenQueue
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();

                Action act = () => target.ValidateResource(default, new QueueHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();

                Action act = () => target.ValidateResource(new Mock<IQueue>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<IQueue>();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<IQueue>();
                var time = 1;
                queue.Setup(x => x.DeleteOnIdleDurationInMinutes).Returns(() => time).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { DeleteOnIdleDurationInMinutes = time }).Should().HaveCount(0);

                queue.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<IQueue>();
                queue.Setup(x => x.DeleteOnIdleDurationInMinutes).Returns(() => 1).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { DeleteOnIdleDurationInMinutes = 2 }).Should().HaveCount(1);

                queue.Verify();
            }
        }

        public class WhenTopic
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();

                Action act = () => target.ValidateResource(default, new TopicHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();

                Action act = () => target.ValidateResource(new Mock<ITopic>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<ITopic>();

                target.ValidateResource(queue.Object, new TopicHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<ITopic>();
                var time = 1;
                queue.Setup(x => x.DeleteOnIdleDurationInMinutes).Returns(() => time).Verifiable();

                target.ValidateResource(queue.Object, new TopicHealthCheckOptions { DeleteOnIdleDurationInMinutes = time }).Should().HaveCount(0);

                queue.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<ITopic>();
                queue.Setup(x => x.DeleteOnIdleDurationInMinutes).Returns(() => 1).Verifiable();

                target.ValidateResource(queue.Object, new TopicHealthCheckOptions { DeleteOnIdleDurationInMinutes = 2 }).Should().HaveCount(1);

                queue.Verify();
            }
        }

        public class WhenSubscription
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();

                Action act = () => target.ValidateResource(default, new SubscriptionHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();

                Action act = () => target.ValidateResource(new Mock<ISubscription>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<ISubscription>();

                target.ValidateResource(queue.Object, new SubscriptionHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<ISubscription>();
                var time = 1;
                queue.Setup(x => x.DeleteOnIdleDurationInMinutes).Returns(() => time).Verifiable();

                target.ValidateResource(queue.Object, new SubscriptionHealthCheckOptions { DeleteOnIdleDurationInMinutes = time }).Should().HaveCount(0);

                queue.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new DeleteOnIdleDurationInMinutesRule();
                var queue = new Mock<ISubscription>();
                queue.Setup(x => x.DeleteOnIdleDurationInMinutes).Returns(() => 1).Verifiable();

                target.ValidateResource(queue.Object, new SubscriptionHealthCheckOptions { DeleteOnIdleDurationInMinutes = 2 }).Should().HaveCount(1);

                queue.Verify();
            }
        }
    }
}
