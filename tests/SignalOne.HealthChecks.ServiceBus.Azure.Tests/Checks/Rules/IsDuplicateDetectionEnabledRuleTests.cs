﻿using FluentAssertions;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Checks.Rules;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests.Checks.Rules
{
    public class IsDuplicateDetectionEnabledRuleTests
    {
        public class WhenQueue
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDuplicateDetectionEnabledRule();

                Action act = () => target.ValidateResource(default, new QueueHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDuplicateDetectionEnabledRule();

                Action act = () => target.ValidateResource(new Mock<IQueue>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new IsDuplicateDetectionEnabledRule();
                var queue = new Mock<IQueue>();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new IsDuplicateDetectionEnabledRule();
                var queue = new Mock<IQueue>();
                var enabled = true;
                queue.Setup(x => x.IsDuplicateDetectionEnabled).Returns(() => enabled).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { IsDuplicateDetectionEnabled = enabled }).Should().HaveCount(0);

                queue.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new IsDuplicateDetectionEnabledRule();
                var queue = new Mock<IQueue>();
                queue.Setup(x => x.IsDuplicateDetectionEnabled).Returns(() => true).Verifiable();

                target.ValidateResource(queue.Object, new QueueHealthCheckOptions { IsDuplicateDetectionEnabled = false }).Should().HaveCount(1);

                queue.Verify();
            }
        }

        public class WhenTopic
        {
            [Fact]
            public void WhenResourceIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDuplicateDetectionEnabledRule();

                Action act = () => target.ValidateResource(default, new TopicHealthCheckOptions());

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("resource");
            }

            [Fact]
            public void WhenOptionsIsNull_ThrowsArgumentNullException()
            {
                var target = new IsDuplicateDetectionEnabledRule();

                Action act = () => target.ValidateResource(new Mock<ITopic>().Object, default);

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenRuleSupplied_AndNoValueSupplied_NoErrorsAreReturned()
            {
                var target = new IsDuplicateDetectionEnabledRule();
                var topic = new Mock<ITopic>();

                target.ValidateResource(topic.Object, new TopicHealthCheckOptions()).Should().HaveCount(0);
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreEqual_NoErrorsAreReturned()
            {
                var target = new IsDuplicateDetectionEnabledRule();
                var topic = new Mock<ITopic>();
                var enabled = true;
                topic.Setup(x => x.IsDuplicateDetectionEnabled).Returns(() => enabled).Verifiable();

                target.ValidateResource(topic.Object, new TopicHealthCheckOptions { IsDuplicateDetectionEnabled = enabled }).Should().HaveCount(0);

                topic.Verify();
            }

            [Fact]
            public void WhenRuleSupplied_AndValuesAreNotEqual_SingleErrorReturned()
            {
                var target = new IsDuplicateDetectionEnabledRule();
                var topic = new Mock<ITopic>();
                topic.Setup(x => x.IsDuplicateDetectionEnabled).Returns(() => true).Verifiable();

                target.ValidateResource(topic.Object, new TopicHealthCheckOptions { IsDuplicateDetectionEnabled = false }).Should().HaveCount(1);

                topic.Verify();
            }
        }
    }
}
