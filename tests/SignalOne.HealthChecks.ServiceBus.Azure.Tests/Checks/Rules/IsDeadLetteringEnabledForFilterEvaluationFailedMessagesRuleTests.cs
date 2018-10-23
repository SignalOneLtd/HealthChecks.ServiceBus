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
    public class IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRuleTests
    {
        public class WhenSubscription
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRule();

                Action act = () => target.ValidateResource(default, new SubscriptionHealthCheckOptions()).ToList();

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRule();

                Action act = () => target.ValidateResource(new Mock<ISubscription>().Object, default).ToList();

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRule();
                var subscription = new Mock<ISubscription>();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRule();
                var subscription = new Mock<ISubscription>();
                var enabled = true;
                subscription.Setup(x => x.IsDeadLetteringEnabledForFilterEvaluationFailedMessages).Returns(() => enabled).Verifiable();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions { IsDeadLetteringEnabledForFilterEvaluationFailedMessages = enabled }).Should().HaveCount(0);

                subscription.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new IsDeadLetteringEnabledForFilterEvaluationFailedMessagesRule();
                var subscription = new Mock<ISubscription>();
                subscription.Setup(x => x.IsDeadLetteringEnabledForFilterEvaluationFailedMessages).Returns(() => true).Verifiable();

                target.ValidateResource(subscription.Object, new SubscriptionHealthCheckOptions { IsDeadLetteringEnabledForFilterEvaluationFailedMessages = false }).Should().HaveCount(1);

                subscription.Verify();
            }
        }
    }
}
