$ErrorActionPreference = 'Stop'

if((Get-Location).Path.EndsWith("build"))
{
    Import-Module .\functions.psm1 -DisableNameChecking -Force
}
else
{
    Import-Module .\build\functions.psm1 -DisableNameChecking -Force
}

Project-Build "SignalOne.HealthChecks.ServiceBus.Sample" -includeTests $false -folderName "samples"