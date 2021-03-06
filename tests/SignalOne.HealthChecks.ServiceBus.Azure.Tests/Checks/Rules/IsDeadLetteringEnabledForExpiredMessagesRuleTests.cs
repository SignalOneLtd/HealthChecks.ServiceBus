﻿using FluentAssertions;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests.Checks.Rules
{
    public class IsDeadLetteringEnabledForExpiredMessagesRuleTests
    {
        public class WhenQueue
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();

                Action act = () => target.ValidateResource(default, new QueueHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();

                Action act = () => target.ValidateResource(new Mock<IQueue>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();
                var queue = new Mock<IQueue>();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();
                var queue = new Mock<IQueue>();
                var enabled = true;
                queue.Setup(x => x.IsDeadLetteringEnabledForExpiredMessages).Returns(() => enabled).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { IsDeadLetteringEnabledForExpiredMessages = enabled }).Should().HaveCount(0);

                queue.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();
                var queue = new Mock<IQueue>();
                queue.Setup(x => x.IsDeadLetteringEnabledForExpiredMessages).Returns(() => true).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { IsDeadLetteringEnabledForExpiredMessages = false }).Should().HaveCount(1);

                queue.Verify();
            }
        }

        public class WhenSubscription
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();

                Action act = () => target.ValidateResource(default, new SubscriptionHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();

                Action act = () => target.ValidateResource(new Mock<ISubscription>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();
                var subscription = new Mock<ISubscription>();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();
                var subscription = new Mock<ISubscription>();
                var enabled = true;
                subscription.Setup(x => x.IsDeadLetteringEnabledForExpiredMessages).Returns(() => enabled).Verifiable();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions { IsDeadLetteringEnabledForExpiredMessages = enabled }).Should().HaveCount(0);

                subscription.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new IsDeadLetteringEnabledForExpiredMessagesRule();
                var subscription = new Mock<ISubscription>();
                subscription.Setup(x => x.IsDeadLetteringEnabledForExpiredMessages).Returns(() => true).Verifiable();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions { IsDeadLetteringEnabledForExpiredMessages = false }).Should().HaveCount(1);

                subscription.Verify();
            }
        }
    }
}
