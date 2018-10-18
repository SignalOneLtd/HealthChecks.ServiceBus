# SignalOne.HealthChecks.ServiceBus.Azure

## Basic Installation/Configuration

Install the following package:

    PM> Install-Package SignalOne.HealthChecks.ServiceBus.Azure

Add the services to your `IHealthChecksBuilder` instance:

        services.AddHealthChecks()
                .AddAzureServiceBusDefaults(options =>
                {
                    options.Namespace = "signalone";
                    options.ServiceCredentials = SdkContext.AzureCredentialsFactory.FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION")); // Generate this using the instructions below
                })
                .AddAzureServiceBusQueueCheck("test-queue")
                .AddAzureServiceBusTopicCheck("test-topic")
                .AddAzureServiceBusSubscriptionCheck("test-topic", "test-subscription");

## Generating Azure Credentials

Follow [this guide](https://docs.microsoft.com/en-us/dotnet/azure/dotnet-sdk-azure-authenticate?view=azure-dotnet#mgmt-auth).