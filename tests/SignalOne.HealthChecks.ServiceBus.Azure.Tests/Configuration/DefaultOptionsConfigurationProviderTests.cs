using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.Rest;
using Moq;
using SignalOne.HealthChecks.ServiceBus.Azure.Configuration;
using System;
using Xunit;

namespace SignalOne.HealthChecks.ServiceBus.Azure.Tests.Configuration
{
    public class DefaultOptionsConfigurationProviderTests
    {
        [Fact]
        public void WhenOptionsAreNull_ThrowArgumentNullException()
        {
            Action act = () => new DefaultOptionsConfigurationProvider(null);

            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("defaultOptions");
        }

        public class Configure_QueueHealthCheckOptions
        {
            private readonly QueueHealthCheckOptions _source = new QueueHealthCheckOptions();
            private readonly HealthCheckOptions _defaults = new HealthCheckOptions
            {
                BaseUri = new Uri("https://azure.com"),
                ServiceCredentials = new BasicAuthenticationCredentials { Password = "PWD", UserName = "UNAME" }
            };
            private readonly Mock<IOptions<HealthCheckOptions>> _defaultOptions;
            private readonly DefaultOptionsConfigurationProvider _target;

            public Configure_QueueHealthCheckOptions()
            {
                _defaultOptions = new Mock<IOptions<HealthCheckOptions>>();
                _defaultOptions.Setup(x => x.Value).Returns(() => _defaults);
                _target = new DefaultOptionsConfigurationProvider(_defaultOptions.Object);
            }

            [Fact]
            public void WhenOptionsAreNull_ThrowNewArgumentNullExceptions()
            {
                var target = new DefaultOptionsConfigurationProvider(new Mock<IOptions<HealthCheckOptions>>().Object);

                Action act = () => target.PostConfigure("default", default(QueueHealthCheckOptions));

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenOptionsAreValid_AndBaseUriIsNull_SetBaseUri()
            {
                _target.PostConfigure("default", _source);

                _source.BaseUri.Should().BeSameAs(_defaults.BaseUri);
            }

            [Fact]
            public void WhenOptionsAreValid_AndBaseUriIsNotNull_SetBaseUri()
            {
                _source.BaseUri = new Uri("https://google.com");

                _target.PostConfigure("default", _source);

                _source.BaseUri.Should().NotBeSameAs(_defaults.BaseUri);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsIsNull_SetBaseUri()
            {
                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().BeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsAreNotNull_SetBaseUri()
            {
                _source.ServiceCredentials = new BasicAuthenticationCredentials { UserName = "hi", Password = "ibiza" };

                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().NotBeSameAs(_defaults.ServiceCredentials);
            }
        }

        public class Configure_TopicHealthCheckOptions
        {
            private readonly TopicHealthCheckOptions _source = new TopicHealthCheckOptions();
            private readonly HealthCheckOptions _defaults = new HealthCheckOptions
            {
                BaseUri = new Uri("https://azure.com"),
                ServiceCredentials = new BasicAuthenticationCredentials { Password = "PWD", UserName = "UNAME" }
            };
            private readonly Mock<IOptions<HealthCheckOptions>> _defaultOptions;
            private readonly DefaultOptionsConfigurationProvider _target;

            public Configure_TopicHealthCheckOptions()
            {
                _defaultOptions = new Mock<IOptions<HealthCheckOptions>>();
                _defaultOptions.Setup(x => x.Value).Returns(() => _defaults);
                _target = new DefaultOptionsConfigurationProvider(_defaultOptions.Object);
            }

            [Fact]
            public void WhenOptionsAreNull_ThrowNewArgumentNullExceptions()
            {
                var target = new DefaultOptionsConfigurationProvider(new Mock<IOptions<HealthCheckOptions>>().Object);

                Action act = () => target.PostConfigure("default", default(QueueHealthCheckOptions));

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenOptionsAreValid_AndBaseUriIsNull_SetBaseUri()
            {
                _target.PostConfigure("default", _source);

                _source.BaseUri.Should().BeSameAs(_defaults.BaseUri);
            }

            [Fact]
            public void WhenOptionsAreValid_AndBaseUriIsNotNull_SetBaseUri()
            {
                _source.BaseUri = new Uri("https://google.com");

                _target.PostConfigure("default", _source);

                _source.BaseUri.Should().NotBeSameAs(_defaults.BaseUri);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsIsNull_SetBaseUri()
            {
                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().BeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsAreNotNull_SetBaseUri()
            {
                _source.ServiceCredentials = new BasicAuthenticationCredentials { UserName = "hi", Password = "ibiza" };

                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().NotBeSameAs(_defaults.ServiceCredentials);
            }
        }

        public class Configure_SubscriptionHealthCheckOptions
        {
            private readonly SubscriptionHealthCheckOptions _source = new SubscriptionHealthCheckOptions();
            private readonly HealthCheckOptions _defaults = new HealthCheckOptions
            {
                BaseUri = new Uri("https://azure.com"),
                ServiceCredentials = new BasicAuthenticationCredentials { Password = "PWD", UserName = "UNAME" }
            };
            private readonly Mock<IOptions<HealthCheckOptions>> _defaultOptions;
            private readonly DefaultOptionsConfigurationProvider _target;

            public Configure_SubscriptionHealthCheckOptions()
            {
                _defaultOptions = new Mock<IOptions<HealthCheckOptions>>();
                _defaultOptions.Setup(x => x.Value).Returns(() => _defaults);
                _target = new DefaultOptionsConfigurationProvider(_defaultOptions.Object);
            }

            [Fact]
            public void WhenOptionsAreNull_ThrowNewArgumentNullExceptions()
            {
                var target = new DefaultOptionsConfigurationProvider(new Mock<IOptions<HealthCheckOptions>>().Object);

                Action act = () => target.PostConfigure("default", default(QueueHealthCheckOptions));

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenOptionsAreValid_AndBaseUriIsNull_SetBaseUri()
            {
                _target.PostConfigure("default", _source);

                _source.BaseUri.Should().BeSameAs(_defaults.BaseUri);
            }

            [Fact]
            public void WhenOptionsAreValid_AndBaseUriIsNotNull_SetBaseUri()
            {
                _source.BaseUri = new Uri("https://google.com");

                _target.PostConfigure("default", _source);

                _source.BaseUri.Should().NotBeSameAs(_defaults.BaseUri);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsIsNull_SetBaseUri()
            {
                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().BeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsAreNotNull_SetBaseUri()
            {
                _source.ServiceCredentials = new BasicAuthenticationCredentials { UserName = "hi", Password = "ibiza" };

                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().NotBeSameAs(_defaults.ServiceCredentials);
            }
        }
    }
}
