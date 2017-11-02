# Web API 2 - Beginner Guide - Ambily K K

## Topics Covered:

1. Implementing Web API
2. Web API Client Implementations - ASP.NET MVC and jQuery
3. Scaffolding with Web API - Entity Framework
4. Routing in Web API
5. Implementing Multiple Serialization Options
6. Help Page Generation

## Introduction

A framework for building HTTP services that can be accessed from various clients (browsers, mobile devices, etc.)

Introduced in MVC 4, its origins in WCF (WCF Web API)

Simple to develop

Contains common HTTP Features (Caching, Status Code, etc.)

## Basic Steps To Create Web API:

1. Create Web API project
2. Add model
3. Add Web API controller
4. Add methods to controller
Simple example:
```c#
using SampleWebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SampleWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        //  Define the products list
        List<Product> products = new List<Product>();

        /// <summary>
        /// Web API method to return a list of products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetAllProducts()
        {
            GetProducts();
            return products;
        }

        private void GetProducts()
        {
            products.Add(new Product { Id = 1, Name = "Television", Category = "Electronic", Price = 82000 });
            products.Add(new Product { Id = 2, Name = "Refrigerator", Category = "Electronic", Price = 23000 });
            products.Add(new Product { Id = 3, Name = "Mobiles", Category = "Electronic", Price = 20000 });
            products.Add(new Product { Id = 4, Name = "Laptops", Category = "Electronic", Price = 45000 });
            products.Add(new Product { Id = 5, Name = "iPads", Category = "Electronic", Price = 67000 });
            products.Add(new Product { Id = 6, Name = "Toys", Category = "Gift Items", Price = 15000 });
        }

        /// <summary>
        /// Web API method to return selected product based on the passed id
        /// </summary>
        /// <param name="selectedId"></param>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts(int selectedId)
        {
            if(products.Count() > 0)
            {
                return products.Where(p => p.Id == selectedId);
            }
            else
            {
                GetProducts();
                return products.Where(p => p.Id == selectedId);
            }
        }

    }
}

```

## Passing Complex Objects to a Web API Method

Passing a simple value is easy, but in most cases you'll be passing a complex object.

You can pass these methods by:

  - **[FromUri] attribute** 

  - **[FromBody] attribute** - reads data from the request body (can only be used once in method param list)  

Example:
```c#
public class SampleController : ApiController
{
    /// <summary>
    /// Get the time based on passed parameters
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public string GetTime(Time t) 
    {
        return string.Format("Received Time: {0}:{1}:{2}", t.Hour, t.Minute, t.Second);
    }
}

public class Time
{
    public int Hour { get; set; }
    public int Minute { get; set; }
    public int Second { get; set; }
}
```

At this point, the API method is not able to map the values properly to the parameter. 

You need to modify the code to include [FromUri] attribute so it can handle a complex object query string.

```c#
public string GetTime([FromUri] Time t)
{
    ...
}
```

