# Dna Framework
A cross-platform base framework useful for all projects that use .Net Core

# Installing Dna Framework

You can install `Dna.Framework` from your Visual Studio projects `Manage NuGet Packages` dialog and search for `Dna.Framework`

Alternatively from **Package Manager** you can do `Install-Package Dna.Framework`

From **.Net CLI** you can do `dotnet add package Dna.Framework`

From **Paket CLI** you can do `paket add Dna.Framework`

# Publishing New Package

To publish a new package:

- Update the `Project > Properties > Package > Package Version` 
- Change `Configuration` to `Release`
- Right-click project and select `Pack`
- Go to output folder `bin\Release` and you should see a `Dna.Framework.x.x.x.nupkg`
- Start a `cmd` in that folder and type: `dotnet nuget push Dna.Framework.x.x.x.nupkg -k yournugetkey -s https://api.nuget.org/v3/index.json`