$sn = "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\sn.exe"

$assemblies = @("SignalOne.HealthChecks.ServiceBus.Amazon.snk","SignalOne.HealthChecks.ServiceBus.Azure.snk","SignalOne.HealthChecks.ServiceBus.Kafka.snk","SignalOne.HealthChecks.ServiceBus.RabbitMQ.snk")

$assemblies | % { 
   & $sn -k $_
}