#Requires -Version 5.1

<#
.SYNOPSIS
    Build and publish TTV application components for deployment

.DESCRIPTION
    This script builds and publishes all TTV application components:
    - TTV.Web.Api (REST API)
    - TTV.Web.Auth (Identity Server)
    - TTV.DatabaseDeploy (Database deployment tool)
    - TTV.Web.Svelte (Frontend SPA)
    
    Creates deployment artifacts in /build/[version]/ with separate folders for each component.
    Excludes development configuration files from the output.

.PARAMETER Version
    Override version from AssemblyVersionInfo.cs. If not specified, reads from file.

.PARAMETER BuildPath
    Output directory for build artifacts. Defaults to "./build"

.PARAMETER Configuration
    Build configuration. Defaults to "Release"

.EXAMPLE
    .\publish.ps1
    Builds all components using version from AssemblyVersionInfo.cs

.EXAMPLE
    .\publish.ps1 -Version "1.6.1" -BuildPath "C:\Deployments"
    Builds with custom version and output path
#>

param(
    [string]$Version,
    [string]$BuildPath = "./build",
    [string]$Configuration = "Release"
)

# Set error action preference
$ErrorActionPreference = "Stop"

# Script variables
$ScriptRoot = $PSScriptRoot
$SolutionRoot = $ScriptRoot

Write-Host "TTV Deployment Build Script" -ForegroundColor Green
Write-Host "===========================" -ForegroundColor Green

# Function to get version from AssemblyVersionInfo.cs
function Get-VersionFromAssembly {
    $assemblyFile = Join-Path $SolutionRoot "AssemblyVersionInfo.cs"
    if (-not (Test-Path $assemblyFile)) {
        throw "AssemblyVersionInfo.cs not found at: $assemblyFile"
    }
    
    $content = Get-Content $assemblyFile
    $versionLine = $content | Where-Object { $_ -match 'AssemblyVersion\("([^"]+)"\)' }
    
    if ($versionLine -and $Matches[1]) {
        return $Matches[1]
    }
    
    throw "Could not extract version from AssemblyVersionInfo.cs"
}

# Function to get version from package.json
function Get-VersionFromPackageJson {
    $packageFile = Join-Path $SolutionRoot "TTV.Web\Svelte\package.json"
    if (-not (Test-Path $packageFile)) {
        throw "package.json not found at: $packageFile"
    }
    
    $packageContent = Get-Content $packageFile -Raw | ConvertFrom-Json
    return $packageContent.version
}

# Determine version
if (-not $Version) {
    $assemblyVersion = Get-VersionFromAssembly
    $packageVersion = Get-VersionFromPackageJson
    
    if ($assemblyVersion -ne $packageVersion) {
        Write-Warning "Version mismatch: AssemblyVersionInfo.cs ($assemblyVersion) vs package.json ($packageVersion)"
        Write-Host "Using AssemblyVersionInfo.cs version: $assemblyVersion"
    }
    
    $Version = $assemblyVersion
}

Write-Host "Building version: $Version" -ForegroundColor Cyan

# Create build directory structure
$BuildPathFull = Join-Path $SolutionRoot $BuildPath
$VersionPath = Join-Path $BuildPathFull $Version
$ApiPath = Join-Path $VersionPath "api"
$AuthPath = Join-Path $VersionPath "auth" 
$DbPath = Join-Path $VersionPath "db"
$WwwPath = Join-Path $VersionPath "www"

Write-Host "Creating build directories..." -ForegroundColor Yellow

@($ApiPath, $AuthPath, $DbPath, $WwwPath) | ForEach-Object {
    if (Test-Path $_) {
        Remove-Item $_ -Recurse -Force
    }
    New-Item $_ -ItemType Directory -Force | Out-Null
}

# Function to publish .NET project
function Publish-DotNetProject {
    param(
        [string]$ProjectPath,
        [string]$OutputPath,
        [string]$ProjectName
    )
    
    Write-Host "Publishing $ProjectName..." -ForegroundColor Yellow
    
    $projectFile = Join-Path $SolutionRoot $ProjectPath
    if (-not (Test-Path $projectFile)) {
        throw "Project file not found: $projectFile"
    }
    
    # Publish project
    $publishArgs = @(
        "publish"
        $projectFile
        "--configuration", $Configuration
        "--output", $OutputPath
        "--no-restore"
        "--verbosity", "minimal"
    )
    
    & dotnet @publishArgs
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to publish $ProjectName"
    }
    
    # Remove development config files
    $devConfigFiles = @(
        "appsettings.Development.json",
        "web.config"
    )
    
    foreach ($file in $devConfigFiles) {
        $filePath = Join-Path $OutputPath $file
        if (Test-Path $filePath) {
            Remove-Item $filePath -Force
            Write-Host "  Removed: $file" -ForegroundColor Gray
        }
    }
    
    Write-Host "  Published to: $OutputPath" -ForegroundColor Green
}

# Function to build Svelte project
function Build-SvelteProject {
    param(
        [string]$OutputPath
    )
    
    Write-Host "Building Svelte frontend..." -ForegroundColor Yellow
    
    $svelteRoot = Join-Path $SolutionRoot "TTV.Web\Svelte"
    $buildDir = Join-Path $svelteRoot "build"
    
    # Change to Svelte directory
    Push-Location $svelteRoot
    
    try {
        # Clean any existing build directory to avoid conflicts
        if (Test-Path $buildDir) {
            Remove-Item $buildDir -Recurse -Force
            Write-Host "  Cleaned existing build directory" -ForegroundColor Gray
        }
        
        # Run production build
        & npm run build:prod
        if ($LASTEXITCODE -ne 0) {
            throw "Failed to build Svelte frontend"
        }
        
        # Copy build output with more careful handling
        if (Test-Path $buildDir) {
            Write-Host "  Copying build artifacts..." -ForegroundColor Gray
            
            # Get all items in the build directory
            $buildItems = Get-ChildItem $buildDir -Force
            
            foreach ($item in $buildItems) {
                $destPath = Join-Path $OutputPath $item.Name
                
                if ($item.PSIsContainer) {
                    # Copy directory
                    if (-not (Test-Path $destPath)) {
                        New-Item $destPath -ItemType Directory -Force | Out-Null
                    }
                    Copy-Item "$($item.FullName)\*" $destPath -Recurse -Force
                    Write-Host "    Copied directory: $($item.Name)" -ForegroundColor Gray
                } else {
                    # Copy file
                    Copy-Item $item.FullName $destPath -Force
                    Write-Host "    Copied file: $($item.Name)" -ForegroundColor Gray
                }
            }
            
            Write-Host "  Built and copied to: $OutputPath" -ForegroundColor Green
        } else {
            throw "Svelte build directory not found: $buildDir"
        }
    }
    finally {
        Pop-Location
    }
}

# Start building
Write-Host ""
Write-Host "Starting build process..." -ForegroundColor Yellow

try {
    # Build .NET solution first
    Write-Host "Restoring .NET solution..." -ForegroundColor Yellow
    & dotnet restore $SolutionRoot
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to restore .NET solution"
    }
    
    Write-Host "Building .NET solution..." -ForegroundColor Yellow
    & dotnet build $SolutionRoot --configuration $Configuration --no-restore
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to build .NET solution"
    }
    
    # Publish each component
    Publish-DotNetProject "TTV.Web\Api\TTV.Web.Api.csproj" $ApiPath "TTV.Web.Api"
    Publish-DotNetProject "TTV.Web\Auth\TTV.Web.Auth.csproj" $AuthPath "TTV.Web.Auth"
    Publish-DotNetProject "TTV.DatabaseDeploy\TTV.DatabaseDeploy.csproj" $DbPath "TTV.DatabaseDeploy"
    
    # Build Svelte frontend
    Build-SvelteProject $WwwPath
    
    # Summary
    Write-Host ""
    Write-Host "Build completed successfully!" -ForegroundColor Green
    Write-Host "Version: $Version" -ForegroundColor Cyan
    Write-Host "Output: $VersionPath" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Deployment structure:" -ForegroundColor White
    Write-Host "  $VersionPath\" -ForegroundColor Gray
    Write-Host "    api\     - TTV.Web.Api" -ForegroundColor Gray
    Write-Host "    auth\    - TTV.Web.Auth" -ForegroundColor Gray
    Write-Host "    db\      - TTV.DatabaseDeploy" -ForegroundColor Gray
    Write-Host "    www\     - TTV.Web.Svelte" -ForegroundColor Gray
    
    # Show directory sizes
    Write-Host ""
    Write-Host "Component sizes:" -ForegroundColor White
    @(
        @("API", $ApiPath),
        @("Auth", $AuthPath), 
        @("Database", $DbPath),
        @("Frontend", $WwwPath)
    ) | ForEach-Object {
        $name = $_[0]
        $path = $_[1]
        if (Test-Path $path) {
            $size = (Get-ChildItem $path -Recurse | Measure-Object -Property Length -Sum).Sum
            $sizeMB = [math]::Round($size / 1MB, 2)
            Write-Host "  $name`: $sizeMB MB" -ForegroundColor Gray
        }
    }
    
} catch {
    Write-Host ""
    Write-Host "Build failed: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}