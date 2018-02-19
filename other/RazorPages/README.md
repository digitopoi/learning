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

## Scaffolding the Movie Model

```bash
dotnet aspnet-codegenerator razorpage -m Movie -dc MovieContext -udl -outDir Pages\Movies --referenceScriptLibraries
```

-m = the name of the model
-dc = the data context
-udl = use the default layout
-outDir = the relative output folder to create the views
--referenceScriptLibraries = adds _ValidationScriptsPartial to Edit and Create Pages

### Movie Pages

Razor Pages are derived from the PageModel - by convention <PageName>Model.

```c#
public class IndexModel : PageModel
```

When a request is made for the page, the OnGetAsync() method is called on a Razor Page to initialize the state for the page.

```c#
public async Task OnGetAsync()
{
    Movie = await _context.Movie.ToListAsync();
}
```

When OnGet() returns void or OnGetAsync() returns Task - no return method is used.

When the return type is IActionResult or Task<IActionResult> - a return statement must be provided:

```c#
public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        return Page();
    }

    _context.Movie.Add(Movie);
    await _context.SaveChangesAsync();

    return RedirectToPage("./Index");
}
```

In the cshtml, the @page Razor directive makes the file into an MVC action - meaning it can handle requests

```cs
@page
```

The DisplayNameFor HTML Helper inspects the Title property referenced in the lamda expression to determine the display name: 

```cs
@Html.DisplayFor(model => model.Movie[0].Title)
```

The @model directive specifies the type of the model passed to the Razor Page.

This makes the PageModel derived class available to the Razor Page. 

```cs
@model RazorPagesMovie.Pages.Movies.IndexModel
```

You can enclose a block of C# with @ { }

```cs
@{
    ViewData["Title"] = "Index";
}
```

The PageModel base class has a ViewData dictionary that can be used to add data that you want to pass to a View. 

Objects can be added using a key/value pattern.

In the above example, the "Title" property is added to the ViewData dictionary.