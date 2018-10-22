function Project-Build {
    [CmdletBinding()]
    param(
        [string] $projectName,
        [string] $version = "0.0.1",
        [string] $buildConfiguration = "Release",
        [bool]   $includeTests = $true,
        [string] $folderName = "src"
    )

    Push-Location ("{0}\{1}" -f $folderName, $projectName)
    Write-Host "Building '$projectName' ($buildConfiguration)..." -ForegroundColor Green
	try
	{
        . dotnet clean -c $buildConfiguration -v q
		. dotnet build -c $buildConfiguration -v q /p:Version=$version /p:PackageVersion=$version
        errorCheck
	}
	finally
	{
		Pop-Location
	}

    if ($includeTests)
    {
        Project-Test ("{0}.Tests" -f $projectName) -buildConfiguration $buildConfiguration
    }
}

function Project-Test {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true, Position=0)]
        [string] $projectName,
        [string] $buildConfiguration = "Release"
    )

    Push-Location "tests\$projectName"
    Write-Host "Testing '$projectName' ($buildConfiguration)..." -Foreground Green
	try
	{
		. dotnet test -v q -c $buildConfiguration
        errorCheck
	}
	finally
	{
		Pop-Location
	}
}

function Project-Package {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true, Position=0)]
        [string] $projectName,
        [Parameter(Mandatory=$true, Position=1)]
        [string] $version
    )

    Push-Location "src\$projectName"
    Write-Host "Packaging '$projectName'..." -ForegroundColor Green
	try
	{
	    . dotnet build -v q -c Release /p:GeneratePackage=true /p:SourcesPackage=true /p:Version=$version /p:PackageVersion=$version
        errorCheck
	    . dotnet build -v q -c Release /p:GeneratePackage=true /p:Version=$version /p:PackageVersion=$version
        errorCheck

	}
	finally
	{
		Pop-Location
	}
}

function errorCheck()
{
    if($LASTEXITCODE -ne 0)
    {
        Write-Host "Exit code: $LASTEXITCODE"
        #throw
    }
}