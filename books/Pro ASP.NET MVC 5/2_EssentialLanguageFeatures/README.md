# Essential Language Features

## Using Automatically Implemented Properties

The regular C# property feature lets you expose a piece of data from a class in a way that decouples that data from how it is set and retrieved

REGULAR PROPERTY (VERBOSE):
```c#
public class Product
{
    private string _name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}
```

Using properties is preferred to using fields because you can change the statements in the get and set blocks without needing to change the classes that depend on the property.

AUTO-IMPLEMENTED PROPERTIES:
```c#
public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
```

Bodies of the getter and setter and the fields aren't defined

C# compiler implements the getter and setter and fields behind the scenes

Using automatic properties saves typing, creates code that is easier to read, and preserves the flexibility that a property provides.

Can change the implementation of a property:

```c#
public class Product 
{
    private string _name;

    public int ProductID { get; set; }

    public string Name
    {
        get
        {
            return ProductID + name;
        }
        set 
        {
            name = value;
        }
    }

    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
```

## Using Object and Collection Initializers

Another tiresome programming task is constructing a new object and then assigning values to the properties:

```c#
public ViewResult CreateProduct()
{
    //  Create a new Product object
    Product myProduct = new Product();

    //  Set the property values
    myProduct.ProductID = 100;
    myProduct.Name = "Kayak";
    myProduct.Description = "A boat for one person";
    myProduct.Price = 275M;
    myProduct.Category = "Watersports";

    return View("Result", 
        (object)String.Format("Category: {0}", myProduct.Category));
}
```

Using **object initializer** feature:

```c#
public ViewResult CreateProduct() 
{
    //  Create and populate a new Product object
    Product myProduct = new Product
    {
        ProductID = 100, Name = "Kayak",
        Description = "A boat for one person",
        Price = 275M, Category = "Watersports"
    };

    return View("Result",
        (object)String.Format("Category: {0}", myProduct.Category));
}
```

The braces form the initializer - in which you can supply values to the parameters as part of the construction process

## Using Extension Methods

Extention methods allow you to add methods to classes you do not own and cannot modify directly. 

```c#
public class ShoppingCart
{
    public List<Product> Products { get; set; }
}
```

Perhaps the above class came from a third party, and I cannot modify it. 

I want to determine the total value of the Product object in the ShoppingCart class.

Use extension method to add the functionality:

```c#
namespace LanguageFeatures.Models
{
    public static decimal TotalPrices(this ShoppingCart cartParam)
    {
        decimal total = 0;

        foreach(Product prod in cartParam.Products)
        {
            total += prod.Price;
        }

        return total;
    }
}
```

The *this* keyword in front of the first parameter marks TotalPrices as an extension method.

The first parameter tells .NET which class the extension method can be applied to (ShoppingCart in this case)

Apply the extension:

```c#
public ViewResult UseExtension()
{
    //  Create and populate a new Product object
    ShoppingCart cart = new ShoppingCart 
    {
        Products = new List<Product>
        {
            new Product { Name = "Kayak", Price = 275M },
            new Product { Name = "Lifejacket", Price = 48.95M },
            new Product { Name = "Soccer ball", Price = 19.50M },
            new Product { Name = "Corner flag", Price = 34.95M },
        }
    };

    //  Get the total value of the products in the cart
    decimal cartTotal = cart.TotalPrices();     // <====

    return View("Result",
        (object)String.Format("Total: {0:c}", cartTotal));
    
}
```

## Applying Extension Methods to an Interface

Can also create extension methods that apply to an interface, which allows you to call the extension method on all of the classes that implement the interface.

```c#
public class ShoppingCart : IEnumerable<Product>
{
    public List<Product> Products { get; set; }

    public IEnumerator<Product> GetEnumerator() 
    {
        return Products.GetEnumerator();
    }

    IEnumerable IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
```

Can then update the previous extension method so it deals with IEnumerable<Product>:

```c#
public static class MyExtensionMethods
{
    public static decimal TotalPrices(this IEnumerable<Product> productEnum)
    {
        decimal total = 0;
        foreach (Product prod in productEnum)
        {
            total += prod.Price;
        }

        return total;
    }
}
```

The first parameter is changed to IEnumerable<Product> - the foreach loop in the method body works directly on Product objects.

Switching to the interface means that I can calculate the total value of the Product objects enumerated by any IEnumerable<Product> - which includes instances of ShoppingCart but also arrays of Products.

## Creating Filtering Extension Methods

Extension methods can be used to filter collections of objects.

An extension method that operates on an IEnumerable<T> and that also returns an IEnumerable<T> can use the *yield* keyword to apply selection criteria to items in the source data to produce a reduce set of results.

```c#
public static IEnumerable<Product> FilterByCategory(
    this IEnumerable<Product> productEnum, string categoryParam)
{
    foreach (Product prod in productEnum)
    {
        if (prod.Category == categoryParam)
        {
            yield return prod;
        }
    }
}
```

The extension method FilterByCategory() takes an additional parameter that allows me to inject a filter condition when I call the method.

The Product objects whose Category matches the parameter are returned in the result IEnumerable<Product> and those that do not match are discarded.

```c#
public ViewResult UseFilterExtensionMethod()
{
    IEnumerable<Product> products = new ShoppingCart
    {
        Products = new List<Product>
        {
            new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
            new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
            new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
            new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M },
        }
    };

    decimal total = 0;
    foreach (Product prod in products.FilterByCategory("Soccer"))   //  <====
    {
        total += prod.Price;
    }

    return View("Result", (object)String.Format("Total: {0}", total));
}
```

When FilterByCategory is called on the ShoppingCart, only those Products in the Soccer category are returned.

## Using Lamda Expressions

A delegate can make the FilterByCategory more general.

The delegate will be invoked against each Product can filter the objects in any way I choose.

Adding a Filter extension method:

```c#
public static IEnumerable<Product> Filter(
    this IEnumerable<Product> productEnum, Func<Product, bool> selectorParam)
    {
        foreach (Product prod in productEnum)
        {
            if (selectorParam(prod))
            {
                yield return prod;
            }
        }
    }
```

Func is used as the filtering parameter - which means I do not need to define the delegate as a type.

The delegate takes a Product parameter and returns a bool - will be true if that Product should be included in the results.

However, I must define a Func for each kind of filtering needed (not ideal). 

```c#
Func<Product, bool> categoryFilter = delegate(Product prod)
{
    return prod.Category == "Soccer";
} 
```

The less verbose option is to use a lamda expression

```c#
Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";
```

The parameter is expressed without specifying a type, which will be inferred automatically. 

The => characters are read aloud as "goes to" and links the parameter to the result of the lamda expressions. 

In the above example, a Product parameter called prod goes to a bool result, which will be true if the Category parameter is equal to Soccer.

The syntax can be made even tighter by removing the Func entirely:

```c#
foreach (Product prod in products.Filter(prod => prod.Category == "Soccer"))
{
    total += prod.Price;
}
```

Can also combine multiple filters by extending the result as part of the lamda expression:

```c#
foreach (Product prod in products
    .Filter(prod => prod.Category == "Soccer" || prod.Price > 20))
{
    total += Price;
}
```

The above lamda expression will match Product objects that are in the Soccer category **or** whose Price property is greater than 20.

## Using Automatic Type Inference

**type inference (or implicity typing)** - the C# *var* keyword allows you to define a local variable without explicitly specifying the variable type

```c#
var myVariable = new Product { Name = "Kayak", Category="Watersports", Price = 275M };

string name = myVariable.Name;      //  legal
int count = myVariable.Count;       //  compiler error
```

It's not the case that myVariable doesn't have a type - it's that it is inferred by the compiler.

## Using Anonymous Types

By combining object initializers and type inference, simple data-storage objects can be created without needed to define the corresponding class or struct.

```c#
var myAnonType = new
{
    Name = "MVC",
    Category = "Pattern"
};
```

myAnonType is an anonymously typed object.

It does not mean it is dynamic like a JavaScript object - only that the type definition will be created automatically by the compiler. **Strong typing is still enforced.**

The C# compiler generates the class based on the name and type of the parameters in the initializer.

Two anonymously typed objects that have the same property names and types will be assigned to the same automatically generated class.

Arrays of anonymously typed objects can be created:

```c#
var oddsAndEnds = new[] 
{
    new { Name = "MVC", Category = "Pattern" },
    new { Name = "Hat", Category = "Clothing" },
    new { Name = "Apple", Category = "Fruit" }
};

StringBuilder result = new StringBuilder();

foreach (var item in oddsAndEnds)
{
    result.Append(item.Name).Append(" ");
}

return View("Result", (object)result.ToString());
```

Important to use *var* to declare the variable array - no type to specify

Even though no class for any of these objects has not been defined - the contents of the array can be read and the value of the Name property can be read from each of them.

## Performing Language Integrated Queries

LINQ is a SQL-like syntax for querying data in classes.

```c#
var foundProducts = from match in products
                    orderby match.Price descending
                    select new { match.Name, match.Price };
```

The Product objects are ordered in descending order and use the select keyword to return an anonymous type that contains just the Name and Price properties.

This style of LINQ is called **query syntax**

However, it returns one anonymously typed object for every Product in the array - so the results need to be tweeked to get the first three and print out the details.

The alternative is to forgo the simplicity of the query syntax with **dot-notation syntax**

```c#
var foundProducts = products.OrderByDescending(e => e.Price)
                            .Take(3)
                            .Select(e => new { e.Name, e.Price });
```

For serious LINQ queries, it's necessary to switch to using extension methods.

Each of the LINQ extension methods in the above example is applied to an IEnumerable<T> and returns an IEnumerable<T>, which allows you to chain the methods together to form complex queries.

All of the LINQ extension methods are in the System.Linq namespace and must be brought into scope with a using statement before you make queries.

The OrderByDescending method rearranges the items in the data source. The lamda expression returns the value needed for comparisons. 

The Take() method returns a specified number of items from the front of the results (can't be done using query syntax).

The Select() method allows projecting the results and specify the structure (in this case an anonymous object with Name and Price properties)

Some useful LINQ Extension methods:

| Extension Method | Description                                                                              | Deferred
| ---------------- | ---------------------------------------------------------------------------------------- | :------: |
| All              | Returns true if all the items in the source data match the predicate                     | No       |
| Any              | Returns true if at least one of the items in the source data match the predicate         | No       |
| Contains         | Returns true if data source contains a specific item or value                            | No       |
| Count            | Returns the number of items in the data source                                           | No       |
| First            | Returns the first item from the data source                                              | No       |
| FirstOrDefault   | Returns the first item from the data source or the default value if there are no items   | No       |
| Last             | Returns the last item in the data source                                                 | No       |
| LastOrDefault    | Returns the last item in the data source or the default value if there are no items      | No       |
| LastOrDefault    | Returns the last item in the data source or the default value if there are no items      | No       |
| Max              | Returns the largest value specified by a lambda expression                               | No       |
| Min              | Returns the smallest value specified by a lambda expression                              | No       |
| OrderBy          | Sorts the source data based on the value returned by the lamda expression                | Yes      |
| Reverse          | Reverses the order of the items in the data source                                       | Yes      |
| Select           | Projects a result from a query                                                           | Yes      |
| SelectMany       | Projects each data item into a sequence and concatenates them                            | Yes      |
| Single           | Returns the first item from the data source or throws exception if there are multiple    | No       |
| SingleOrDefault  | Returns the first item from the data source or default value, exception if multiple      | No       |
| Skip             | Skips over a specified number of elements                                                | Yes      |
| SkipWhile        | Skips over a elements while the predicate matches                                        | Yes      |
| Sum              | Totals the values selected by the predicate                                              | No       |
| Take             | Selects a specified number of elements from the data source                              | Yes      |
| TakeWhile        | Selects items while the predicate matches                                                | Yes      |
| ToArray          | Converts the data source to an array                                                     | No       |
| ToDictionary     | Converts the data source to a dictionary                                                 | No       |
| ToList           | Converts the data source to a list                                                       | No       |
| Where            | Filters items from the data source that do not match the predicate                       | Yes      |

## Understanding Deferred LINQ Queries

A query that contains only deferred methods is not executed until the items in the result are enumerated

Queries are evaluated from scratch every time the results are enumerated, meaning that you can perform the query repeatedly as the source data changes and get results that reflect the current state of the source data.

## Using Async Methods

**asynchronous methods** - go off and do work in the background and notify you when they are complete, allowing your code to take care of other business while the background work is performed.

Asynchronous methods are an important tool in removing bottlenecks from code and allow applications to take advantage of multiple processors and processor cores to perform work in parallel.

.NET represents work that will be done asynchronously as a Task. Task objects are strongly typed based on the result that the background work produces.

**continuation** - the mechanism by which you specify what you want to happen when the background task is complete.

## Applying the async and await Keywords

Microsoft introduced two keywords to C# that are specifically intended to simplify using asynchronous methods. 

```c#
public async static Task<long?> GetPageLength()
{
    HttpClient client = new HttpClient();

    var httpMessage = await client.GetAsync("http://apress.com");

    //  we could do other things here while we are waiting
    //  for the HTTP request to complete
    return httpMessage.Content.Headers.ContentLength;
}
```

Using the *await* keyword when calling the asynchronous method tells the C# compiler that I want to wait for the result of the Task that the GetAsync() method returns and then carry on executing other statements in the same method.

The *await* keyword means I can treat the result from the GetAsync method as though it were a regular emthod and just assign the HttpResponseMessage object that it returns to a variable.

When you use the *await* keyword, you must add the *async* keyword to the method signature.



