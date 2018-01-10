# Building Your First API with ASP.NET Core

## The ASP.NET Core Request Pipeline & Middleware

When an HTTP request comes in, we need to respond: request, response pipeline

We can configure the request pipeline by adding **middleware**- software components that are assembled into an application pipeline to handle requests and responses.

In older version of ASP.NET - modules and handlers did this for us.

In ASP.NET Core, the modules and handlers have been taken over by middleware.

Each piece in the middleware pipeline decides whether to pass it on to the next component or not.

The order middleware comes in the pipeline is important. 

If authentication middleware finds that the request isn't authorized it will not pass it on to the next piece of middleware, but it will immediately return an Unauthorized response.

### Configuring the ASP.NET Request Pipeline

Startup.cs

```c#
                                               //   IHostingEnvironment service allows you to programmaticaly 
                                               //   access the environment value
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
{
    loggerFactory.AddConsole();

    //  Makes it so that the DeveloperException middleware will only be added when we're running in the development environment
    if (env.IsDevelopment())
    {
        //  This configures the request pipeline by adding the developer exception page middleware to the request pipeline
        //  When an exception is thrown, this piece of middleware will handle it.
        app.UseDeveloperExceptionPage();
    }

    app.Run(async (context)) =>
    {
        await context.Response.WriteAsync("Hello World!")
    }
}
```

### Development Environments

Right click on project properties - debug tab allows you to set the development environment

Three types of development by convention:

1. development
2. staging
3. production

## Creating an API and Returning Resources

### Middleware for Building an API

ASP.NET WEB API (http services)

ASP.NET MVC (client facing web applications)

ASP.NET Web API and ASP.NET MVC are unified in ASP.NET Core MVC now

#### Clarifying the MVC Pattern

Architectural pattern

Loose coupling, separation of concerns: testability, reuse

NOT the full application architecture (just presentation)

Controllers depend on the view and the model - The view depends on the model

The consumer of the API sends requests that are received by the controller

### Adding the ASP.NET Core MVC Middleware

To add the MVC framework services, we need to add the AddMvc extension method in the ConfigureServices method

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
}
```

If we run now we will get a blank page - 404 error - we added MVC middleware, it doesn't do anything yet.

### ASP.NET Core 2 Metapackages and Runtime Store

Microsoft.AspNetCore.All metapackage

ASP.NET is modular - comes in many pieces - but, modularity comes with a set of potential issues (not always easy to find the functionality you need, keeping track of version numbers can be cumbersome)

The metapackages helps with this - all ASP.NET Core 2.0 application reference this by default

Metapackages add references to a list of packages

The runtime store is a location on disk containing these (and other) packages

    - faster deployment
    - lower disk space use
    - improved startup performance

This is optional - you can still add the packages you need instead of the metapackage

### Returning resources part 1

JsonResult class to return JSON data

```c#
public JsonResult GetCities()
{
    return new JsonResult();
}
```

### Routing

Routing matches request URI to the controller method

Convention-based and attribute-based routing

#### Convention based routing

Passed to the UseMvc extension method:
```c#
app.UseMvc(config => {
    config.MapRoute(
        name: "Default",
        template: "{controller}/{action}/{id?}",
        defaults: new { controller="Home", action="Index" }
    );
});
```

These are usually used with the MVC framework for navigating views.

Not advised for APIs

#### Attribute-based routing

These are better for APIs

Attributes at controller & action level, including an (optional) template

The URI is matched to a specific action on a controller, depending on the attributes

| HTTP Method   | Attribute   | Level       | Sample Uri                  |
|---------------|-------------|-------------|---------------------------- |
| GET           | HttpGet     | Action      | /api/cities, /api/cities/1  |
| POST          | HttpPost    | Action      | /api/cities                 |
| PUT           | HttpPut     | Action      | /api/cities/1               |
| PATCH         | HttpPatch   | Action      | /api/cities/1               |
| DELETE        | HttpDelete  | Action      | /api/cities/1               |
| ---           | Route       | Controller  | /api/cities/1               |

[Route] attribute is used at the controller level. [Route("api/cities")]

## Adding DTOs

Important to note: what is returned from or accepted by an API is not the same as the entities used by the underlying datastore.

The underlying datastore will simply work on the DTO classes we're creating.

## The Importance of Status Codes

Part of the response

Provide information on:

- whether or not the request worked out as expected
- what is responsible for a failed request

### Level 200 - SUCCESS
200: Ok
201: Created
204: No Content

### Level 400 - Client Error
400: Bad Request
401: Unauthorized
403: Forbidden
404: Not found
409: Conflicts

### Level 500 - Server Error

500: Internal Server Error

## Returning Child Resources

ex. api/cities/PointsOfInterest

## Working With Serializer Settings

Configure services so Json properties are capitalized instead of the default lowercase (if upgrading old Web API project to Core for consistency)

```c#
//  ConfigureServices()
services.AddMvc()
    .AddJsonOptions(o =>
        if (o.SerializerSettings.ContractResolver != null)
        {
            var castedResolver = o.SerializerSettings.ContractResolver 
                as DefaultContractResolver;

            castedResolver.NamingStrategy = null;
        }
    );
```

For most new applications, the default (camelCase) will be fine.

## Formatters and Content Negotiation

**content negotiation** - selecting the best representation for a given response when there are multiple representations available.

If you're building an API for one client - you might be ok only returning JSON.

If you're building an API for multiple clients, some of which you have no control over, chances are that not all of these clients can easily consume JSON.

The consumer can request a specific format by passing in the requested media type through the Accept header.

- application/json
- application/xml

If no accept header is available, or it doesn't support the requested formatted, it reverts to default (JSON in most cases today).

**Output formatter** - deals with outputs. The consumer can request a specific type of output by setting the accept header.

**Input formatter** - deals with input. The body of a post request, for example. Media type: content-type header. 

```c#
//  ConfigureServices()
services.AddMvc()
    .AddMvcOptions(o => o.OutputFormatters.Add(
        new XmlDataContractSerializerOutputFormatter())
    );

```

## Manipulating Resources

Creating a Resource

```c#
[HttpPost("{cityId}/PointsOfInterest")]
public IActionResult CreatePointOfInterest(int cityId,
    [FromBody] PointOfInterestForCreationDto pointOfInterest)
{
    if (pointOfInterest == null)
    {
        return BadRequest();
    }

    var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

    if (city == null)
    {
        return NotFound();
    }

    //  demo purposes, to be improved
    //  get highest current point of interest id
    var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(
        c => c.PointsOfInterest).Max(p => p.Id);

    // we have to map the dtos
    var finalPointOfInterest = new PointOfInterestDto()
    {
        Id = ++maxPointOfInterestId,
        Name = pointOfInterest.Name,
        Description = pointOfInterest.Description
    };

    city.PointsOfInterest.Add(finalPointOfInterest);

    return CreatedAtRoute("GetPointOfInterest", new
    { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
}
```

## Validating Input

Add attributes to DTO

```c#
public class PointOfInterestForCreationDto
{
    [Required(ErrorMessage = "You should provide a name value")]
    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }
}
```

ModelState is a dictionary.

It contains both the state of the model and model-binding validation. It represents a collection of name and value pairs that were submitted to our API, one for each property. It also contains a collection of error messages for each value submitted.

```c#
//  ...
if (!ModelState.IsValid)
{
    return BadRequest(ModelState);
}
//  ...
```

Adding custom validation

```c#
if (pointOfInterest.Description == pointOfInterest.Name)
{
    ModelState.AddError("Description", "The provided description should be different from the name.);
}
```

## Patching a Resource / Partial Update

PATCH Http method is for partial updates

Standard format: Json Patch (RFC 6902)

Defines a JSON document structure for expressing a sequence of operations to apply to a JSON document.

The consumer of the API will create a JsonPatch document as the body of the patch request, adhering to this standard.

example:

```js
[
    {
        "op": "replace",
        "path": "/name",
        "value": "new name"
    },
    {
        "op": "replace",
        "path": "/description",
        "value": "new description"
    }
]
```

**JsonPatch document is essentially a list of operations like, at, remove, replace, etc. that have to be applied to the resource allowing for partial updates**

## Working With Services and Dependency Injection

### Inversion of Control and Dependency Injection

If a class is dependent upon a service, logger, etc. - it has to change when a dependency changes.

It also makes it difficult to test in isolation.

The class manages the lifetime of the dependency. 

**Inversion of Control** - delegates the function of selecting a concrete implementation type for a class's dependencies to an external component. 

**Dependency Injection** - a specialization of the Inversion of Control pattern. The Dependency Injection pattern uses an object - the container - to initialize objects and prove the required dependencies to the object.

```c#
public class PointsOfInterestController : Controller 
{
    private ILogger<PointsOfInterestController> _logger;                                //  INTERFACE - not concrete implementation

    public PointsOfInterestController(ILogger<PointsOfInterestController> logger)       //  Constructor Injection
    {
        _logger = logger
    }
}
```

The controller is now decoupled from a concrete implementation of ILogger. 

The dependencies can be replaced or updated with very few or no changes to the code in our class.

The controller can easily be tested because those dependencies can be mocked by providing a mock version of ILogger.

Previously, versions of ASP.NET MVC did not include a dependency injection container built in. 

Some services are built in and registered with the container by default, like the logger. Others can be added.

ConfigureServices() is used to register services with the built-in container.

You could request an ILoggerFactory and create an instance of a logger, but there's another way:

The container can also directly provide us with an ILogger<T> instance:

```c#
private ILogger<PointsOfInterestController> _logger;

public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
{
    _logger = logger;
}
```

### Logging to a Database or a File

ASP.Net Core does not contain a built in logger to a database or a file. It does contain one for logging to the event log

There are different logging libraries supported (Serilog, elmah.io, Loggr, NLog, etc.).

The integration of an external provider into ASP.Net Core's logging system is the same. 

### Implementing and Using a Custom Service

1. Transient Lifetime Services - created each time they are requested. Works best for lightweight, stateless services.

2. Scoped Lifetime Services - created once per request. 

3. Singleton Lifetime Services - created the first time they are requested or if you specify an instance when ConfigureServices() is run. Every subsequent request will use the same instance.

```c#
public interface IMailService
{
    void Send(string subject, string message);
}
```

```c#
public class LocalMailService : IMailService
{
    private string _mailTo = "admin@mycompany.com";
    private string _mailFrom = "noreply@mycompany.com";

    public void Send(string subject, string message)
    {
        //  send mail - output to debug window
        Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailService.");
        Debug.WriteLine($"Subject: {subject}");
        Debug.WriteLine($"Message: {message}");
    }
}
```

```c#
public void ConfigureServices(IServiceCollection services)
{
    //  ...
    services.AddTransient<IMailService, LocalMailService>();
}
```

### Working With Configuration Files

Almost all applications require some way of keeping and reading configuration values.

ASP.Net Core can work with a variety of config files (JSON, XML, in-memory settings, command-line arguments, or environment variables).

appSettings.json
```js
{
    "mailSettings": {
        "mailToAddress": "admin@mycompany.com",
        "mailFromAddress": "noreply@mycompany.com"
    }
}
```

Now, we need to tell ASP.Net Core to read these settings from this file.

Startup.cs

```c#
public class Startup
{
    public static IConfiguration Configuration { get; private set; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
```

```c#
public class LocalMailService : IMailService
{
    private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
    private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];
```

### Scoping Configuration to Environments

We can scope configuration files to a specific environment by providing different configuration sources, for which part of the name is the name of the environment.

Adding an appsettings file for the production environment:

appsettings.production.json
```js
{
  "mailSettings": {
    "mailToAddress": "admin@mycompany.com",
  }
}
```

appsettings.json
```js
{
  "mailSettings": {
    "mailToAddress": "developer@mycompany.com",
    "mailFromAddress": "noreply@mycompany.com"
  }
}
```