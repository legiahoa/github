# Coffee Management System - Build and Run Script
Write-Host "========================================" -ForegroundColor Green
Write-Host "COFFEE MANAGEMENT SYSTEM - BUILD SCRIPT" -ForegroundColor Green  
Write-Host "========================================" -ForegroundColor Green
Write-Host ""

# Check for MSBuild paths
$msbuildPaths = @(
    "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Program Files\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe", 
    "C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe",
    "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
)

$msbuild = $null
foreach ($path in $msbuildPaths) {
    if (Test-Path $path) {
        $msbuild = $path
        break
    }
}

if ($msbuild) {
    Write-Host "✅ Found MSBuild: $msbuild" -ForegroundColor Green
    Write-Host "Building CoffeeManagement.csproj..." -ForegroundColor Yellow
    Write-Host ""
    
    & $msbuild "CoffeeManagement.csproj" /p:Configuration=Debug /p:Platform="Any CPU"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "🎉 BUILD SUCCESSFUL!" -ForegroundColor Green
        Write-Host "Starting Coffee Management System..." -ForegroundColor Yellow
        Write-Host ""
        
        if (Test-Path "bin\Debug\CoffeeManagement.exe") {
            Start-Process "bin\Debug\CoffeeManagement.exe"
        } else {
            Write-Host "❌ Executable not found!" -ForegroundColor Red
        }
    } else {
        Write-Host ""
        Write-Host "❌ BUILD FAILED!" -ForegroundColor Red
        Write-Host "Check the errors above." -ForegroundColor Yellow
    }
} else {
    Write-Host "❌ MSBuild not found!" -ForegroundColor Red
    Write-Host "Trying alternative approach..." -ForegroundColor Yellow
    Write-Host ""
    
    # Try running the demo instead
    Write-Host "🚀 Running Coffee Management Demo..." -ForegroundColor Cyan
    Set-Location "CoffeeDemoWinForms"
    & dotnet run
}

Write-Host ""
Write-Host "Press any key to continue..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
