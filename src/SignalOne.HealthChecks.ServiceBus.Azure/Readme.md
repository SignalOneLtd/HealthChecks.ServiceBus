# SignalOne.HealthChecks.ServiceBus.Azure

Build Status: ...

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

## Checking properties on a Queue/Topic/Subscription

The library allows you to check the properties on a Queue/Topic/Subscription by supplying the optional overload as shown below:

        services.AddHealthChecks()
                .AddAzureServiceBusDefaults(options =>
                {
                    options.Namespace = "signalone";
                    options.ServiceCredentials = SdkContext.AzureCredentialsFactory.FromFile(Environment.GetEnvironmentVariable("AZURE_AUTH_LOCATION")); // Generate this using the instructions below
                })
                .AddAzureServiceBusQueueCheck("test-queue", check => check.IsBatchedOperationsEnabled = true)
                .AddAzureServiceBusTopicCheck("test-topic", check => check.IsBatchedOperationsEnabled = true)
                .AddAzureServiceBusSubscriptionCheck("test-topic", "test", check => check.IsBatchedOperationsEnabled = true);

Only the values that are supplied in the overload are checked. This allows you to do partial checks against the entity in question, like checking to see whether or not a queue is partioned, for example.

## Setting Default Options

By calling `AddAzureServiceBusDefaults` you can set common properties that will automatically be configured for all Queues/Topics/Subscriptions when there is no configured value. These options include:

- Service Credentials
- Subscription Id's
- Namespace

In order to use a non-defaulted option, simply specify it in the optional configuration overload.