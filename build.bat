@echo off
dotnet restore src/Fonix.Spatial/
dotnet build -c Release --no-incremental src/Fonix.Spatial/