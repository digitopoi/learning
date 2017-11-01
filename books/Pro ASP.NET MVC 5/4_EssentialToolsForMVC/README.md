# Essential Tools for MVC

This chapter looks at three MVC tools:

  1. Dependency Injection Container (Ninject)

  2. A unit test framework (built-in)

  3. A mocking tool (Moq)

## Using Ninject

The idea behind **dependency injection** is to decouple the components in an MVC application with a combination of interfaces and DI container that creates instances of objects by creating implementations of the interfaces they depend on and injecting them into the constructor. 

Currently, the ShoppingCart class is tightly coupled to the LinqValueCalculator and the HomeController is tightly coupled to both ShoppingCart and LinqValueCalculator:

ShoppingCart.cs
```c#
public class ShoppingCart
{
    private LinqValueCalculator calc;

    public ShoppingCart(LinqValueCalculator calcParam)      //    <====
    {
        calc = calcParam;
    }

    public IEnumerable<Product> Products { get; set; }

    public decimal CalculateProductTotal()
    {
        return calc.ValueProducts(Products);
    }
}
```

HomeController.cs
```c#
public class HomeController : Controller
{
    private Product[] products =
    {
        new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
        new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
        new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
        new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M },
    };

    // GET: Home
    public ActionResult Index()
    {
        LinqValueCalculator calc = new LinqValueCalculator();                 //  <====

        ShoppingCart cart = new ShoppingCart(calc) { Products = products };   //  <====

        decimal totalValue = cart.CalculateProductTotal();

        return View(totalValue);
    }
}
```

If I want to replace LinqValueCalculator, I have to locate and then change the references in the class that are tightly coupled to it.

Not a problem in such a small application, but in a larger one it would cause problems (tedious and error-prone).

## Applying an Interface

Part of the problem can be solved by using a C# interface to abstract the definition of the calculator functionality from its implementation.

I can solve this by implementing a C# interface to abstract the definition of the calculator functionality from its implementation:

IValueCalculator.cs
```c#
public interface IValueCalculator
{
    decimal ValueProducts(IEnumerable<Product> products);
}
```

This interface can then be implemented in the LinqValueCalculator class:

LinqValueCalculator.cs
```c#
public class LinqValueCalculator : IValueCalculator
{
  ...
}
```

The interface allows me to break the tight coupling between ShoppingCart and LinqValueCalculator

ShoppingCart.cs
```c#
public class ShoppingCart 
{
  private IValueCalculator calc;

  public ShoppingCart(IValueCalculator calcParam)
  {
    calc = calcParam;
  }

  ...
}
```

C# still requires me to specify the implementation class for an interface during instantiation

I still have a problem in the HomeController:

HomeController.cs
```c#
public ActionResult Index()
{
  IValueCalculator calc = new LinqValueCalculator();

  ...

}
```

I now need to use Ninject to reach the point where I can specify that I want to instantiate an implementation of the IValueCalculator interface, but the details of which implementation is required are not part of the code in the HomeController.

Need to tell Ninject that LinqValueCalculator is the implementation of IValueCalculator interface and update HomeController class so that it obtains its objects via Ninject, rather than by using the *new* keyword.

## Adding Ninject to the Visual Studio Project

```bash
Install-Package Ninject -version 3.0.1.10
Install-Package Ninject.Web.Common -version 3.0.0.7
Install-Package Ninject.MVC3 -Version 3.0.0.6
```

### The first step is to prepare Ninject for use. 

1. Create an instance of Ninject **kernel**- the object that is responsible for resolving dependencies and creating new objects.

Ninject can be extended and customized to use different kinds of kernel, but usually (almost always) you only need the StandardKernel.

```c#
IKernel ninjectKernel = new StandardKernel();
```

2. Configure the Ninject kernel so that it understands which implementation objects I want to use for each interface I am working with

Ninject uses C# type parameters to create a relationship: I set the interface I want to work with as the type parameter for the Bind() method and call the To() method on the result it returns. I set the implementation class I want instantiated as the type parameter for the To() method. This tells Ninject that dependencies on the IValueCalculator interface should be resolved by creating an instance of the LinqValueCalculator class.

```c#
ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
```

3. Use Ninject to create an object - through the kernel Get() method

The type parameter for the Get() method tells Ninject which interface I am interested in and the result from this method is an instance of the implementation type I specified with the To() method

```c#
IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();
```

HomeController.cs
```c#
public ActionResult Index()
{
    IKernel ninjectKernel = new StandardKernel();                           //  [1]
    ninjectKernel.Bind<IValueCalculator>().To<LinqValueCalculator>();       //  [2]

    IValueCalculator calc = ninjectKernel.Get<IValueCalculator>();          //  [3]

    ShoppingCart cart = new ShoppingCart(calc) { Products = products };

    decimal totalValue = cart.CalculateProductTotal();

    return View(totalValue);
}
```

## Setting up MVC Dependency Injection

So far, the Ninject linkage remains in HomeController - so it is still tightly coupled to the LinqValueCalculator class.

Next step is to embed Ninject at the heart of the MVC application - will allow to simplify the controller, expand Ninject's influence so it works across the app, and move the configuration out of the controller.

## Creating the Dependency Resolver

**custom dependency resolver**- The MVC Framework uses a dependency resolver to create instances of the classes it needs to service requests. By creating a custom resolver, I can ensure that the MVC Framework uses Ninject whenever it creates an object-including instance of controllers, for example.

To set up the resolver, I need a new folder called Infrastructure - folder to put classes that do not fit the other folders in an MVC application. 

And, add a class called NinjectDependencyResolver:

```c#
public class NinjectDependencyResolver : IDependencyResolver
{
    private IKernel kernel;

    public NinjectDependencyResolver(IKernel kernelParam)
    {
        kernel = kernelParam;
        AddBindings();
    }

    public object GetService(Type serviceType)
    {
        return kernel.TryGet(serviceType);
    }

    public IEnumerable<object> GetServices(Type serviceType)
    {
        return kernel.GetAll(serviceType);
    }

    private void AddBindings()
    {
        kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
    }
}
```

The NinjectDependencyResolver class implements the IDependencyResolver interface (from System.Mvc namespace) - MVC Framework uses it to get the objects it needs.

The MVC Framework will call the GetService() or GetServices() methods when it needs an instance of a class to service an incoming request.

The job of a dependency resolver is to create that instance - performed by Ninject TryGet() and GetAll() methods. 

The TryGet() method works like the Get() method, but returns null when there is no suitable binding rather than throwing an exception.

The GetAll() method supports multiple bindings for a single type, which is used when there are several different implementation objects available.

The dependency resolver class is also where Ninject binding is set up. In the AddBindings() method, the Bind() and To() methods are used to configure the relationship between the IValueCalculator interface and the LinqValueCalculator class.

## Register the Dependency Resolver

It's not enough to simply create an implementation of the IDependencyResolver interface - also need to tell MVC Framework that I want to use it.

Ninject created a file called NinjectWebCommon.cs in the App_Start folder - defines methods called automatically when the application starts, in order to integrate into the ASP.NET request lifecycle. (This is to provide the **scopes** feature). 

In the RegisterServices() method, I add a statement that creates an instance of the NinjectDependencyResolver class to register the resolver with the MVC Framework.

NinjectWebCommon.cs
```c#
private static void RegisterServices(IKernel kernel)
{
  System.Web.Mvc.DependencyResolver.SetResolver(new
    EssentialTools.Infrastructure.NinjectDependencyResolver(kernel));
}
```

## Refactoring the Home Controller

The final step is to refactor the HomeController so that it takes advantage of the facilities set up previously.

The main change is to add a class constructor that accepts and implementation of the IValueCalculator interface. Ninject will provide an object that implements the interface when it creates an instance of the controller, using the configuration setup in the NinjectDependencyResolver class.

Finally, any mention of Ninject or the LinqValueCalculator is removed from the controller.

```c#
public class HomeController : Controller
{
    private IValueCalculator calc;                              //  <====

    private Product[] products =
    {
        new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
        new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
        new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
        new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M },
    };

    public HomeController(IValueCalculator calcParam)           //  <====              
    {
        calc = calcParam;
    }

    // GET: Home
    public ActionResult Index()
    {
        ShoppingCart cart = new ShoppingCart(calc) { Products = products };

        decimal totalValue = cart.CalculateProductTotal();

        return View(totalValue);
    }
}
```

## What happens when you run the application:

1. The MVC Framework received the request and figured out that the request is intended for the HomeController

2. The MVC Framework asked the custom dependency resolver class to create a new instance of the HomeController class, specifying the class using the Type parameter of the GetService() method.

3. The dependency resolver asked Ninject to create a new HomeController class, passing on the Type object to the TryGet() method.

4. Ninject inspected the HomeController constructor and found that it has declared a dependency on the IValueCalculator interface, for which it has a binding.

5. Ninject creates an instance of the LinqValueCalculator class and uses it to create a new instance of the HomeController class.

6. Ninject passes the HomeController instance to the custom dependency resolver, which returns it to the MVC Framework. The MVC Framework uses the controller instance to service the request.

* Now, I only have to modify the dependency resolver class when I want to replace the LinqValueCalculator with another implementation, because this is the only place where I have to specify the implementation used to satisfy dependencies on the IValueCalculator interface.

## Creating Chains of Dependency 

When you ask Ninject to create a type, it examines the dependencies that the type has declared.

It also looks to see if they rely on other types - or declare their own dependencies.

If there are additional dependencies - Ninject automatically resolved them and creates instances of all classes that are required along the chain of dependencies.

Adding another interface and class that implements it:

Discount.cs
```c#
public interface IDiscountHelper 
{
  decimal ApplyDiscount(decimal totalParam);
}

public class DefaultDiscountHelper : IDiscountHelper
{
  public decimal ApplyDiscount(decimal totalParam)
  {
    return (totalParam - (10m / 100m * totalParam));
  }
}
```

Adding dependency:

The new constructor [1] declares a dependency on the IDiscountHelper interface.

The implementation object that the constructor receives is assigned to a field and uses it in the ValueProducts method to apply a discount to the cumulative values of the Product objects. [2]

LinqValueCalculator.cs
```c#
public class LinqValueCalculator : IValueCalculator
{
  private IDiscountHelper discounter;

  public LinqValueCalculator(IDiscountHelper discountParam)         //  [1]
  {
    discounter = discountParam;
  }

  public decimal ValueProducts(IEnumerable<Product> products)
  {
    return discounter.ApplyDiscount(products.Sum(p => p.Price));    //  [2]
  }
}
```

The next step is to bind the IDiscountHelper interface to the implementation class with the Ninject kernel in the NinjectDependencyResolver class.

NinjectDependencyResolver.cs
```c#
private void AddBindings()
{
  kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
  kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();       //  <====
}
```

### Dependency chain has now been created:

- HomeController depends on the IValueCalculator interface
- Resolved using the LinqValueCalculator class
- The LinqValueCalculator class depends on the IDiscountHelper interface
- Resolved using the DefaultDiscountHelper class

## Specifying Property and Constructor Parameter Values

You can configure the objects that Ninject creates by providing details of values you want applied to properties when you bind the interface to its implementation.

To demonstrate - DefaultDiscountHelper is revised so that it defines a DiscountSize property:

Discount.cs
```c#
public interface IDiscountHelper
{
  decimal ApplyDiscount(decimal totalParam);
}

public class DefaultDiscountHelper : IDiscountHelper
{
  public decimal DiscountSize { get; set; }

  public decimal ApplyDiscount(decimal totalParam)
  {
    return (totalParam - (DiscountSize / 100m * totalParam));
  }
}
```

When telling Ninject which classes to use for an interface, you can use the WithPropertyValue() method to set the value for the DiscountSize property in the DefaultDiscountHelper class.

You don't need to change any other binding or change the way you use the Get() method to obtain an instance of the ShoppingCart class.

The property value is set following construction of the DefaultDiscountHelper class, and has the effect of halving the total value of the items.

NinjectDependencyResolver.cs
```c#
private void AddBindings()
{
  kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
  kernel.Bind<IDiscountHelper>()
    .To<DefaultDiscountHelper>().WithPropertyValue("DiscountSize", 50M);
}
```

If you have more than one property value you need to set, you can chain calls to the WithPropertyValue method to cover them all. You can also do it with constructor parameters.

Discount.cs
```c#
public class DefaultDiscountHelper : IDiscountHelper
{
  public decimal discountSize;
}

public DefaultDiscountHelper(decimal discountParam)
{
  discountSize = discountParam;
}

public decimal ApplyDiscount(decimal totalParam)
{
  return (totalParam - (discountSize / 100m * totalParam));
}
```

To bind this class to Ninject, you need to specify the value of the constructor parameter using the WithConstructorArgument() method in the AddBindings method:

```c#
private void AddBindings()
{
  kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
  kernel.Bind<IDiscountHelper>()
    .To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50M);
}
```

## Using Conditional Binding

Ninject supports conditional binding that allows you to specify which classes the kernel should use to respond for particular interfaces.

FlexibleDiscountHelper applies different discounts based on the magnitude of the total. 

FlexibleDiscountHelper.cs
```c#
public class FlexibleDiscountHelper : IDiscountHelper
{
  public decimal ApplyDiscount(decimal totalParam)
  {
    decimal discount = totalParam > 100 ? 70 : 25;
    return (totalParam - (discount / 100m * totalParam));
  }
}
```

Now I have a choice of classes that implement IDiscountHelper interface, and I can modify AddBindings() method to tell Ninject I want to use each of them:

NinjectDependencyResolver.cs
```c#
private void AddBindings()
{
  kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
  kernel.Bind<IDiscountHelper>()
    .To<DefaultDiscountHelper>().WithConstructorArgument("discountParam", 50M);
  kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>()
    .WhenInjectedInto<LinqValueCalculator>();
}
```

The new binding specifies that the Ninject kernel should use the FlexibleDiscountHelper class as the implementation of the IDiscountHelper interface when it is creating a LinqValueCalculator object.

The original binding for IDiscountHelper is still in place.

Ninject tries to find the best match and it helps to have a default binding for the same class or interface, so that Ninject has a fallback if the criteria for a conditional binding are not satisfied.

### Other Conditional Binding Methods:

Helps tailor the lifecycle of the objects that Ninject creates to match the needs of your applications.

By default, Ninject will create a new instance of every object needed to resolve every dependency each time you request an object.

Modify LinqValueCalculator output a message whenever a new instance is created:

LinqValueCalculator.cs
```c#
public class LinqValueCalculator : IValueCalculator
{
  private IDiscountHelper discounter;
  private static int counter = 0;
  
  public LinqValueCalculator(IDiscountHelper discountParam)
  {
    discounter = discountParam;

    System.Diagnostics.Debug.WriteLine(
      string.Format("Instance {0} created", ++counter));
  }

  ...
}
```

The System.Diagnostics.Debug class contains a number of methods that can be used to write out debugging messages.

[This wasn't working for me?]

## Scope

For some classes you will want to share a single instance throughout the entire application and for others you will want to create a new instance each HTTP request received.

Ninject lets you control the lifecycle of the objects you create using **scope**

Scope is expressed using a method call when setting up the binding between an interface and its implementation type.

Using request scope:

NinjectDependencyResolver.cs
```c#
...
using Ninject.Web.Common;

...

private void AddBindings()
{
  kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();

  ...
}
```

InRequestScope() tells Ninject that it should only create one instance of the LinqValueCalculator class for each HTTP request that ASP.NET receives.

Each request will get its own separate object, but have multiple dependencies resolved with the same instance of the class.

### Ninject Scope Methods

|Name                        | Effect                                      |
| -------------------------- | ------------------------------------------- |
| InTransientScope()         | This is the same as not specifying a scope and creates a new object for each dependency that is resolved |
| InSingletonScope()         | Creates a single instance which is shared throughout the application. |
| InThreadScope()            | Creates a single instance which is used to resolve dependencies for objects requested by a single thread |
| InRequestScope()            | Creates a single instance which is used to resolve dependencies for objects requested by a single HTTP request |



