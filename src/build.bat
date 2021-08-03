@echo off
echo Starting the building script.
echo ----------------------
Rem Installs the deb and rpm package from 'Packaging utilities for .NET Core'.
dotnet tool install --global dotnet-rpm
dotnet tool install --global dotnet-deb
echo ----------------------
echo Installing the dependencies to the local src folder.
echo ----------------------
Rem Adds the packages to the csproj file.
dotnet rpm install
dotnet deb install
echo ----------------------
echo Building the DEB and RPM files.
echo ----------------------
dotnet-deb
dotnet-rpm
echo ----------------------
echo Finished
pause