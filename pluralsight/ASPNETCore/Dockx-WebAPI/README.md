#Building Your First API with ASP.NET Core

## The ASP.NET Core Request Pipeline & Middleware

When an HTTP request comes in, we need to respond - request, response pipeline

We can configure the request pipeline by adding **middleware**- software components that are assembled into an application pipeline to handle requests and responses.

In older version of ASP.NET - modules and handlers did this for us. 

In ASP.NET Core, the modules and handlers have been taken over by middleware.

Each piece in the middleware pipeline decides whether to pass it on to the next component or not. The order middleware comes in the pipeline is important. If authentication middleware finds that the request isn't authorized it will not pass it on to the next piece of middleware, but it will immediately return an Unauthorized response.

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



