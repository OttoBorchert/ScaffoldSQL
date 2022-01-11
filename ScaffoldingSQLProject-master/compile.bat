@echo off
REM Available builds: win-x86 win-x64 win-arm osx-x64 linux-x64 linux-arm
set BuildFor=win-x64 win-x86 osx-x64 linux-x64
set Releases=Admin Release

REM Check if dotnet exists
where dotnet
REM dotnet is not installed. Notify and exit
if %ErrorLevel% NEQ 0 echo ERROR: dotnet not installed & GOTO:EOF

(for %%r in (%Releases%) do (
    REM Notify what release we are compiling for
    echo Starting Release %%r
    (for %%b in (%BuildFor%) do (
        echo Building: %%b %%r
        dotnet publish ScaffoldingSQLProject.csproj -c %%r -r %%b -o bin\%%r\\%%b
    ))
))

REM Done!
echo Compile Finished for all Builds and Releases