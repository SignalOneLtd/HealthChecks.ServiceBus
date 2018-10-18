function build($target, $version)
{
	dotnet build $target -c Release /p:Version=$version /p:PackageVersion=$version-pre
	dotnet build $target -c Release /p:SourcesPackage=true /p:Version=$version-pre /p:PackageVersion=$version-pre
}

cls

$version = "0.0.1"

#build "..\src\SignalOne.HealthChecks.ServiceBus.Amazon" $version
build "..\src\SignalOne.HealthChecks.ServiceBus.Azure" $version
#build "..\src\SignalOne.HealthChecks.ServiceBus.Kafka" $version
#build "..\src\SignalOne.HealthChecks.ServiceBus.RabbitMQ" $version