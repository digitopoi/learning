# Razor Syntax

**view engine**- processes ASP.NET content and looks for instructions, typically to insert dynamic content into the output sent to a browser. 

Razor is the name of the MVC Framework view engine.

Razor statements start with the @ character.

First line in the view:
```c#
@model Razor.Models.Product
```

The @model statement declares the type of the model object that will be passed to the view from the action method. 

Allows reference to the methods, fields, and properties of the view model object.

```c#
<body>
    <div>
        @Model.Name
    </div>
</body>
```

**important**- declare the model (@model) with a lowercase m and access the property (@Model) with an Upper Case M. 

@model also allows IntelliSense to offer suggestions and Visual Studio to flag errors

## Working with Layouts

```c#
@{
    Layout = null;
}
```

This is an example of a Razor **code block**

This code block sets the value of the Layout property to null. 

Razor views are compiled into C# classes in an MVC application and the base class that is used defines the Layout property.

Setting the Layout property to null tells the MVC Framework that the view is self-contained and will render all of the content required for the client.

Layouts are templates that contain markup that you use to create consistency across the app (instead of using self contained templates for every page).

## Applying a Layout

```c#
@ {
    Layout = "~/Views/_BasicLayout.cshtml";
}
```

Adding a layout allows you to focus on presenting data from the view model object to the user - simpler markup, and no duplicate common elements in every view that I create.

**view start file**- when it renders a view, the MVC framework will look for a file called _ViewStart.cshtml. 

## Using Razor Expressions

| Component       | Does Do                                                    | Doesn't Do                          |
| --------------- | ---------------------------------------------------------- | ----------------------------------- |
| Action Method   | Pass a view model to the view                              | Pass formatted data to the view     |
| View            | Use the view model object to present content to the user   | Pass formatted data to the view     |

To get the best from MVC, you need to respect and enforce the separation between different parts of the app.

You can do quite a lot with Razor using C# statements, but you should not use Razor to perform business logic or manipulate your domain model objects in any way.

You should not format the data that your action method passes to the view. Instead, let the view figure out data it needs to display.

```c#
public ActionResult NameAndPrice()
{
    return View(myProduct)
}
```

```c#
The product name is @Model.Name and it costs $@Model.Price
```

## Processing Versus Formatting Data

Views **format** data

Controllers **process** data

Best practice is to err on the side of caution and not push anything but the most simple of expressions out of the view and into the controller.

## Inserting Data Values

Adding the data to ViewBag:

```c#
public ActionResult DemoExpression() 
{
    ViewBag.ProductCount = 1;
    ViewBag.ExpressShip = true;
    ViewBag.ApplyDiscount = false;
    ViewBag.Supplier = null;

    return View(myProduct);
}
```

```c#
@{
    ViewBag.Title = "DemoExpression";
}

<table>
    <thead>
        <tr><th>Property</th><th>Value</th></tr>
    </thead>
    <tr><td>Name</td><td>@Model.Name</td></tr>
    <tr><td>Price</td><td>@Model.Price</td></tr>
    <tr><td>Stock</td><td>@Model.ProductCount</td></tr>
</table>
```
## Setting Attribute Values

You can also use Razor syntax to set the value of element **attributes**

```c#
<div data-discount="@ViewBag.ApplyDiscount" data-express="@ViewBag.ExpressShip"
     data-supplier="@ViewBag.Supplier">
     The containing element has data attributes
</div>

Discount: <input type="checkbox" checked="@ViewBag.ApplyDiscount" />
Express: <input type="checkbox" checked="@ViewBag.ExpressShip" />
Supplier: <input type="checkbox" checked="@ViewBag.Supplier" />
```

The above example uses basic Razor expressions to set the value for some data attributes on a div element

When rendered by HTML:

```c#
<div data-discount="False" data-express="True" data-supplier="">
```

## Using Conditional Statements

Conditional statements allow you to create complex and fluid layouts that are still reasonably simple to read and maintain.

```c#
<tr>
    <td>Stock Level</td>
    <td>
    @switch ((int)ViewBag.ProductCount)
    {
        case 0:
            @: Out of Stock
            break;
        case 1:
            <b>Stock (@ViewBag.ProductCount)</b>
            break;
        default:
            @ViewBag.ProductCount
            break;
    }
    </td>
</tr>
```

To start a conditional statement, you place an @ character in front of the C# conditional keyword which is *switch* in this example.

You close the code block with a } as you would a regular C# code block. 

Conditional statements allow content to be varied based on the data values that the view receives from the action method.

```c#
<td>
    @if (ViewBag.ProductCount == 0)
    {
        @:Out of Stock
    }
    else if (ViewBag.ProductCount == 1)
    {
        <b>Low Stock (@ViewBag.ProductCount)</b>
    }
    else
    {
        @ViewBag.ProductCount
    }
</td>
```

## Enumerating Arrays and Collections

```c#
public ActionResult DemoArray()
{
    Product[] array = 
    {
        new Product { Name = "Kayak", Price = 275M },
        new Product { Name = "Lifejacket", Price = 48.95M },
        new Product { Name = "Soccer ball", Price = 19.50M },
        new Product { Name = "Corner flag", Price = 34.95M },
    };

    return View(array);
}
```

Visual Studio scaffold feature won't allow you to specify an array as a model type (**is this still true?**)

To create a view for an action method that passes an array, the best approach is to create a variable without a model and then manually add the @model expression after the file has been created.

```c#
@model Razor.Models.Product[]

@{
    ViewBag.Title = "DemoArray";
    Layout = "~/Views/_BasicLayout.html";
}

@if (Model.Length > 0)
{
    <table>
        <thead><tr><th>Product</th><th>Price</th></tr></thead>
        <tbody>
            @foreach (Razor.Models.Product p in Model)
            {
                <tr>
                    <td>@p.Name</td>
                    <td>@p.Price</td>
                </tr>
            }
        </tbody>
    </table>
}
else 
{
    <h2>No product data</h2>
}
```

## Dealing with Namespaces

In the above example, the Product class had to be referred to by its fully qualified name in the foreach loop (Razor.Models.Product)

Can be difficult in a complex view, with many references to view model and other classes

Can be made tidier by applying the @using expression to bring a namespace into context for a view

```c#
@using Razor.Models
@model Product[]

...

<tbody>
    @foreach (Product p in Model)   //  <====
    {
        <tr>
            <td>@p.Name</td>
            <td>@p.Price</td>
        </tr>
    }
</tbody>
```

A view can contain multiple @using expressions
