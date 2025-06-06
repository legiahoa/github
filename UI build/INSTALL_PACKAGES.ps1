# Coffee Management System - Package Installation Script
# This script helps resolve package dependencies for the Coffee Management System

Write-Host "Coffee Management System - Package Installation" -ForegroundColor Green
Write-Host "=================================================" -ForegroundColor Green

# Check if Visual Studio is installed
$vsInstallPath = Get-ChildItem -Path "${env:ProgramFiles(x86)}\Microsoft Visual Studio" -ErrorAction SilentlyContinue
if ($vsInstallPath) {
    Write-Host "✅ Visual Studio detected" -ForegroundColor Green
    Write-Host "Recommended: Use Visual Studio Package Manager Console" -ForegroundColor Yellow
    Write-Host "1. Open this project in Visual Studio" -ForegroundColor White
    Write-Host "2. Go to Tools > NuGet Package Manager > Package Manager Console" -ForegroundColor White
    Write-Host "3. Run: Install-Package Guna.UI2.WinForms" -ForegroundColor White
} else {
    Write-Host "❌ Visual Studio not detected" -ForegroundColor Red
}

# Check for NuGet CLI
$nugetPath = Get-Command nuget -ErrorAction SilentlyContinue
if ($nugetPath) {
    Write-Host "✅ NuGet CLI detected" -ForegroundColor Green
    Write-Host "Alternative: Use NuGet CLI" -ForegroundColor Yellow
    Write-Host "Run: nuget restore" -ForegroundColor White
} else {
    Write-Host "❌ NuGet CLI not found" -ForegroundColor Red
}

Write-Host ""
Write-Host "Package Requirements:" -ForegroundColor Cyan
Write-Host "- Guna.UI2.WinForms (v2.0.4.6 or later)" -ForegroundColor White
Write-Host "- System.Data.SqlClient (for database connectivity)" -ForegroundColor White

Write-Host ""
Write-Host "Build Instructions:" -ForegroundColor Cyan
Write-Host "1. Install required packages using one of the above methods" -ForegroundColor White
Write-Host "2. Build the project in Visual Studio (Ctrl+Shift+B)" -ForegroundColor White
Write-Host "3. Run the application (F5)" -ForegroundColor White

Write-Host ""
Write-Host "If you encounter build errors:" -ForegroundColor Yellow
Write-Host "1. Ensure .NET Framework 4.8 is installed" -ForegroundColor White
Write-Host "2. Verify GunaUI2 package is properly referenced" -ForegroundColor White
Write-Host "3. Check database connection string in App.config" -ForegroundColor White

Write-Host ""
Write-Host "For support, refer to BUILD_SUMMARY.md" -ForegroundColor Cyan
