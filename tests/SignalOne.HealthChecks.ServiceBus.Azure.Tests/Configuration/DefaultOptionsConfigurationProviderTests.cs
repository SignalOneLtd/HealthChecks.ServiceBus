using FluentAssertions;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Extensions.Options;
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
                Namespace = "NSP",
                ServiceCredentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.AppService), AzureEnvironment.AzureGlobalCloud),
                LogLevel = Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler.Level.Basic,
                Product = "PRO",
                Version = "VER",
                SubscriptionId = "SUB"
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
            public void WhenOptionsAreValid_AndServiceCredentialsIsNull_SetServiceCredentials()
            {
                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().BeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsAreNotNull_IgnoreServiceCredentials()
            {
                _source.ServiceCredentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.AppService), AzureEnvironment.AzureGlobalCloud);

                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().NotBeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndNamespaceIsNull_SetNamespace()
            {
                _target.PostConfigure("default", _source);

                _source.Namespace.Should().BeSameAs(_defaults.Namespace);
            }

            [Fact]
            public void WhenOptionsAreValid_AndNamespaceIsNotNull_IgnoreNamespace()
            {
                _source.Namespace = "test";

                _target.PostConfigure("default", _source);

                _source.Namespace.Should().NotBeSameAs(_defaults.Namespace);
            }

            [Fact]
            public void WhenOptionsAreValid_AndSubscriptionIdIsNull_SetSubscriptionId()
            {
                _target.PostConfigure("default", _source);

                _source.SubscriptionId.Should().BeSameAs(_defaults.SubscriptionId);
            }

            [Fact]
            public void WhenOptionsAreValid_AndSubscriptionIdIsNotNull_IgnoreSubscriptionId()
            {
                _source.SubscriptionId = "test";

                _target.PostConfigure("default", _source);

                _source.SubscriptionId.Should().NotBeSameAs(_defaults.SubscriptionId);
            }

            [Fact]
            public void WhenOptionsAreValid_AndLogLevelIsNull_SetLogLevel()
            {
                _target.PostConfigure("default", _source);

                _source.LogLevel.Should().Be(_defaults.LogLevel);
            }

            [Fact]
            public void WhenOptionsAreValid_AndLogLevelIsNotNull_IgnoreLogLevel()
            {
                _source.LogLevel = Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler.Level.Basic;

                _target.PostConfigure("default", _source);

                _source.LogLevel.Should().NotBeSameAs(_defaults.LogLevel);
            }

            [Fact]
            public void WhenOptionsAreValid_AndProductIsNull_SetProduct()
            {
                _target.PostConfigure("default", _source);

                _source.Product.Should().BeSameAs(_defaults.Product);
            }

            [Fact]
            public void WhenOptionsAreValid_AndProductIsNotNull_IgnoreProduct()
            {
                _source.Product = "test";

                _target.PostConfigure("default", _source);

                _source.Product.Should().NotBeSameAs(_defaults.Product);
            }

            [Fact]
            public void WhenOptionsAreValid_AndVersionIsNull_SetVersion()
            {
                _target.PostConfigure("default", _source);

                _source.Version.Should().BeSameAs(_defaults.Version);
            }

            [Fact]
            public void WhenOptionsAreValid_AndVersionIsNotNull_IgnoreVersion()
            {
                _source.Version = "test";

                _target.PostConfigure("default", _source);

                _source.Version.Should().NotBeSameAs(_defaults.Version);
            }
        }

        public class Configure_TopicHealthCheckOptions
        {
            private readonly TopicHealthCheckOptions _source = new TopicHealthCheckOptions();
            private readonly HealthCheckOptions _defaults = new HealthCheckOptions
            {
                Namespace = "NSP",
                ServiceCredentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.AppService), AzureEnvironment.AzureGlobalCloud),
                LogLevel = Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler.Level.Basic,
                Product = "PRO",
                Version = "VER",
                SubscriptionId = "SUB"
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

                Action act = () => target.PostConfigure("default", default(TopicHealthCheckOptions));

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsIsNull_SetServiceCredentials()
            {
                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().BeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsAreNotNull_IgnoreServiceCredentials()
            {
                _source.ServiceCredentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.AppService), AzureEnvironment.AzureGlobalCloud);

                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().NotBeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndNamespaceIsNull_SetNamespace()
            {
                _target.PostConfigure("default", _source);

                _source.Namespace.Should().BeSameAs(_defaults.Namespace);
            }

            [Fact]
            public void WhenOptionsAreValid_AndNamespaceIsNotNull_IgnoreNamespace()
            {
                _source.Namespace = "test";

                _target.PostConfigure("default", _source);

                _source.Namespace.Should().NotBeSameAs(_defaults.Namespace);
            }

            [Fact]
            public void WhenOptionsAreValid_AndSubscriptionIdIsNull_SetSubscriptionId()
            {
                _target.PostConfigure("default", _source);

                _source.SubscriptionId.Should().BeSameAs(_defaults.SubscriptionId);
            }

            [Fact]
            public void WhenOptionsAreValid_AndSubscriptionIdIsNotNull_IgnoreSubscriptionId()
            {
                _source.SubscriptionId = "test";

                _target.PostConfigure("default", _source);

                _source.SubscriptionId.Should().NotBeSameAs(_defaults.SubscriptionId);
            }

            [Fact]
            public void WhenOptionsAreValid_AndLogLevelIsNull_SetLogLevel()
            {
                _target.PostConfigure("default", _source);

                _source.LogLevel.Should().Be(_defaults.LogLevel);
            }

            [Fact]
            public void WhenOptionsAreValid_AndLogLevelIsNotNull_IgnoreLogLevel()
            {
                _source.LogLevel = Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler.Level.Basic;

                _target.PostConfigure("default", _source);

                _source.LogLevel.Should().NotBeSameAs(_defaults.LogLevel);
            }

            [Fact]
            public void WhenOptionsAreValid_AndProductIsNull_SetProduct()
            {
                _target.PostConfigure("default", _source);

                _source.Product.Should().BeSameAs(_defaults.Product);
            }

            [Fact]
            public void WhenOptionsAreValid_AndProductIsNotNull_IgnoreProduct()
            {
                _source.Product = "test";

                _target.PostConfigure("default", _source);

                _source.Product.Should().NotBeSameAs(_defaults.Product);
            }

            [Fact]
            public void WhenOptionsAreValid_AndVersionIsNull_SetVersion()
            {
                _target.PostConfigure("default", _source);

                _source.Version.Should().BeSameAs(_defaults.Version);
            }

            [Fact]
            public void WhenOptionsAreValid_AndVersionIsNotNull_IgnoreVersion()
            {
                _source.Version = "test";

                _target.PostConfigure("default", _source);

                _source.Version.Should().NotBeSameAs(_defaults.Version);
            }
        }

        public class Configure_SubscriptionHealthCheckOptions
        {
            private readonly SubscriptionHealthCheckOptions _source = new SubscriptionHealthCheckOptions();
            private readonly HealthCheckOptions _defaults = new HealthCheckOptions
            {
                Namespace = "NSP",
                ServiceCredentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.AppService), AzureEnvironment.AzureGlobalCloud),
                LogLevel = Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler.Level.Basic,
                Product = "PRO",
                Version = "VER",
                SubscriptionId = "SUB"
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

                Action act = () => target.PostConfigure("default", default(SubscriptionHealthCheckOptions));

                act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("options");
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsIsNull_SetServiceCredentials()
            {
                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().BeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndServiceCredentialsAreNotNull_IgnoreServiceCredentials()
            {
                _source.ServiceCredentials = new AzureCredentials(new MSILoginInformation(MSIResourceType.AppService), AzureEnvironment.AzureGlobalCloud);

                _target.PostConfigure("default", _source);

                _source.ServiceCredentials.Should().NotBeSameAs(_defaults.ServiceCredentials);
            }

            [Fact]
            public void WhenOptionsAreValid_AndNamespaceIsNull_SetNamespace()
            {
                _target.PostConfigure("default", _source);

                _source.Namespace.Should().BeSameAs(_defaults.Namespace);
            }

            [Fact]
            public void WhenOptionsAreValid_AndNamespaceIsNotNull_IgnoreNamespace()
            {
                _source.Namespace = "test";

                _target.PostConfigure("default", _source);

                _source.Namespace.Should().NotBeSameAs(_defaults.Namespace);
            }

            [Fact]
            public void WhenOptionsAreValid_AndSubscriptionIdIsNull_SetSubscriptionId()
            {
                _target.PostConfigure("default", _source);

                _source.SubscriptionId.Should().BeSameAs(_defaults.SubscriptionId);
            }

            [Fact]
            public void WhenOptionsAreValid_AndSubscriptionIdIsNotNull_IgnoreSubscriptionId()
            {
                _source.SubscriptionId = "test";

                _target.PostConfigure("default", _source);

                _source.SubscriptionId.Should().NotBeSameAs(_defaults.SubscriptionId);
            }

            [Fact]
            public void WhenOptionsAreValid_AndLogLevelIsNull_SetLogLevel()
            {
                _target.PostConfigure("default", _source);

                _source.LogLevel.Should().Be(_defaults.LogLevel);
            }

            [Fact]
            public void WhenOptionsAreValid_AndLogLevelIsNotNull_IgnoreLogLevel()
            {
                _source.LogLevel = Microsoft.Azure.Management.ResourceManager.Fluent.Core.HttpLoggingDelegatingHandler.Level.Basic;

                _target.PostConfigure("default", _source);

                _source.LogLevel.Should().NotBeSameAs(_defaults.LogLevel);
            }

            [Fact]
            public void WhenOptionsAreValid_AndProductIsNull_SetProduct()
            {
                _target.PostConfigure("default", _source);

                _source.Product.Should().BeSameAs(_defaults.Product);
            }

            [Fact]
            public void WhenOptionsAreValid_AndProductIsNotNull_IgnoreProduct()
            {
                _source.Product = "test";

                _target.PostConfigure("default", _source);

                _source.Product.Should().NotBeSameAs(_defaults.Product);
            }

            [Fact]
            public void WhenOptionsAreValid_AndVersionIsNull_SetVersion()
            {
                _target.PostConfigure("default", _source);

                _source.Version.Should().BeSameAs(_defaults.Version);
            }

            [Fact]
            public void WhenOptionsAreValid_AndVersionIsNotNull_IgnoreVersion()
            {
                _source.Version = "test";

                _target.PostConfigure("default", _source);

                _source.Version.Should().NotBeSameAs(_defaults.Version);
            }
        }
    }
}
