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