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
	try
	{
        Write-Host "Cleaning '$projectName' ($buildConfiguration)..." -ForegroundColor Green
        . dotnet clean -c $buildConfiguration -v q /nologo
        Write-Host "Building '$projectName' ($buildConfiguration)..." -ForegroundColor Green
		. dotnet build -c $buildConfiguration -v q /p:Version=$version /p:PackageVersion=$version /nologo
        errorCheck
	}
	finally
	{
		Pop-Location
	}

    if ($includeTests)
    {
        Project-Test ("{0}.Tests" -f $projectName) -buildConfiguration $buildConfiguration -prebuilt $true
    }
}

function Project-Test {
    [CmdletBinding()]
    param(
        [Parameter(Mandatory=$true, Position=0)]
        [string] $projectName,
        [string] $buildConfiguration = "Release",
        [bool]   $prebuilt = $false
    )

    Push-Location "tests\$projectName"
    Write-Host "Testing '$projectName' ($buildConfiguration)..." -Foreground Green
	try
	{
        if($prebuilt)
        {
		    . dotnet test -v q --no-build -c $buildConfiguration /nologo
        }
        else
        {
		    . dotnet test -v q -c $buildConfiguration /nologo
        }
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
	try
	{
        Write-Host "Packaging '$projectName'..." -ForegroundColor Green
	    . dotnet build -v q -c Release /p:GeneratePackage=true /p:SourcesPackage=true /p:Version=$version /p:PackageVersion=$version /nologo
        errorCheck
        Write-Host "Packaging '$projectName.Sources'..." -ForegroundColor Green
	    . dotnet build -v q -c Release /p:GeneratePackage=true /p:Version=$version /p:PackageVersion=$version /nologo
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