#ASP.NET Web API 2 - Building a REST Service Start to Finish

## ASP.NET as a Service Framework

- Recent trends in web technologies have given rise to a need for services for more than complex enterprise applications talking to each other

- More about web pages needing to get and push small amounts of data.

1. Single Page Applications

browser based "fat client" - JavaScript code is connecting from browser to a service back end.

2. Mobile Applications

similar in nature to those that support AJAX-enabled web sites.

Communicate via HTTP, send and receive small amounts of text-based data, security models tend to take a minimalist approach (less configuration and fewer headaches for users).

implementation of these services encourages more reuse across different mobile platforms.

## Highlights ASP.NET Web API

### Convention-based CRUD Actions

HTTP actions are automatically mapped to controller methods (actions)

### Built-in Content Negotiations

In MVC, controller methods returning JSON or XML have to be hard-coded to specifically return one of the types.

With ASP.NET Web API - the controller only needs to return the raw data value and it will be automatically converted to JSON or XML - just specifying an accept or content-type HTTP header

Rather than return a JsonResult, you can simply return the data object (ie IEnumerable<Product>)

### Attribute Routing and Route Prefixes

Let's you avoid relying on convention-based routing. 

Route, RoutePrefix, and Http* attributes (HttpGet, HttpPost, etc.) to explicitly define routes and associated HTTP actions for your controllers.

### Route Constraints

constraining various controller methods and their routes to specific business rules.

ex:
```c#
{id: min(10)}
{id: range(1,100)}
```

### CORS Support

EnableCors attribute allows you to allow cross-origin requests from JavaScript applications not in your service's domain

### Global Error Handling

All unhandled exceptions can be caught and handled through one central mechanism

Supports multiple exception loggers

### IHttpActionResult

Implementations of HttpResponseMessage factory interface provide a reusable, unit test-friendly way to encapsulate results of your Web API action methods.