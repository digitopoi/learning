# Getting Started with Razor Pages

[Link to tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages-vsc/razor-pages-start)

## Project files and folders

wwwroot - static files

Pages - Folder for Razor Pages

appsettings.json - configuration

Program.cs - Hosts the app

Startup.cs - Configures services and the request pipelines

### Pages Folder

_Layout.cshtml - contains common HTML elements and sets the layout of the application.

_ViewStart.cshtml - sets the Razor Pages Layout property to use _Layout.cshtml

_ViewImports.cshtml - contains Razor directives that are imported into each Razor Page.

_ValidationScriptsPartial.cshtml - provides a reference to jQuery validation scripts.

## Adding Entity Framework Core NuGet packages

### Method one - adding directly to .csproj file:

```xml
<DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.2" />
```

### Method two - CLI:

```bash
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

### Restore

```bash
dotnet restore
```

## Run First Migration and Update Database

```bash
dotnet ef migrations add InitialCreate
```

```bash
dotnet ef database update
```

