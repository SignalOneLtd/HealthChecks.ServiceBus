$sn = "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\sn.exe"

$assemblies = @("SignalOne.HealthChecks.ServiceBus.snk")

$assemblies | % { 
   & $sn -k $_
}