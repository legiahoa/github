@echo off
echo ========================================
echo COFFEE MANAGEMENT SYSTEM - BUILD SCRIPT
echo ========================================
echo.

REM Set MSBuild path for .NET Framework
set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
if not exist %MSBUILD_PATH% (
    set MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
)
if not exist %MSBUILD_PATH% (
    set MSBUILD_PATH="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"
)

echo Trying to build with .NET Framework MSBuild...
echo MSBuild Path: %MSBUILD_PATH%
echo.

if exist %MSBUILD_PATH% (
    echo Building CoffeeManagement.csproj...
    %MSBUILD_PATH% CoffeeManagement.csproj /p:Configuration=Debug /p:Platform="Any CPU"
    if %ERRORLEVEL% EQU 0 (
        echo.
        echo ✅ BUILD SUCCESSFUL!
        echo Starting Coffee Management System...
        echo.
        start "" "bin\Debug\CoffeeManagement.exe"
    ) else (
        echo.
        echo ❌ BUILD FAILED!
        echo Check the errors above.
    )
) else (
    echo.
    echo ❌ MSBuild not found!
    echo Please install Visual Studio or .NET Framework SDK.
    echo.
    echo Alternative: Run demo version
    echo cd CoffeeDemoWinForms
    echo dotnet run
)

echo.
pause
