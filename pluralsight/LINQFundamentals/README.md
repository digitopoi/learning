# LINQ FUNDAMENTALS - Scott Allen

## An Introduction

### What Is LINQ?

Before LINQ - there were separate APIs for working with object data, relational data, and XML data, for example.

LINQ gives us strongly typed compile-time checks on queries that can execute against memory data, relational data, and XML data.

You can use LINQ skills to query a large variety of data sources - there have been providers built that allow us to use LINQ to query:

- MongoDB

- CSV Files

- File System

- SQL Databases

- HL7 HML

- JSON

- etc.

### Writing a Report Without LINQ

Writing a C# program to list the five largest files inside of a directory.

```cs
class Program
{
    static void Main(string[] args)
    {
        string path = @"C:\windows";

        ShowLargeFilesWithoutLinq(path);
    }

    private static void ShowLargeFilesWithoutLinq(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles();
        Array.Sort(files, new FileInfoComparer());

        for(int i = 0; i < 5; i++)
        {
            FileInfo file = files[i];

            Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
        }
    }
}

public class FileInfoComparer : IComparer<FileInfo>
{
    public int Compare(FileInfo x, FileInfo y)
    {
        return y.Length.CompareTo(x.Length);
    }
}
```

### Writing a Report With LINQ

Query syntax:

```cs
private static void ShowLargeFilesWithLinq(string path)
{
    var query = from file in new DirectoryInfo(path).GetFiles()
                orderby file.Length descending
                select file;

    foreach (var file in query.Take(5))
    {
        Console.WriteLine($"{ file.Name, -20 } : { file.Length, 10:N0 }");
    }
}
```

Method syntax:

```cs
private static void ShowLargeFilesWithLinq(string path)
{
    var query = new DirectoryInfo(path).GetFiles()
                    .OrderByDescending(f => f.Length)
                    .Take(5);

    foreach (var file in query)
    {
        Console.WriteLine($"{ file.Name, -20 } : { file.Length, 10:N0 }");
    }
}
```

## LINQ and C#

Features of C# you should know about to use LINQ

### Evolving the Language

First, it is important to understand what LINQ is trying to do.

LINQ is designed to work against different data sources. To abstract these different data sources away we talk about a *sequence* of something - where sequence is a magical data type that doesn't exactly tell us where the data is coming from. We want it to work as well against an array as against a database.

Common query operators, like `Where` (which applies a filter), should be easy to express - and not just that you want to filter but *how* to filter.

With version 2 of the C# language, the best you could do is this:

```cs
IEnumerable<Employee> scotts = EnumerableExtensions.Where(exployees,
                                    delegate(Employee e)
                                    {
                                        return e.Name == "Scott";
                                    });
```

This code doesn't look at all like a query.

Future versions of C# allowed us to write queries with pretty syntax and type checking.

```cs
Sequence<Employee> scotts = employees.Where(Name == "Scott");

var scotts = from e in employees
             where e.Name == "Scott"
             select e;
```

By the end of the module, you'll come to understand that there is no magic - but, there is a compiler working very hard to make this LINQ dream come true!

### The Power of IEnumerable

With two different collections, we can iterate over either:

```cs
Employee[] developers = new Employee[]
{
    new Employee { Id = 1, Name = "Scott" },
    new Employee { Id = 2, Name = "Chris" }
};

List<Employee> sales = new List<Employee>()
{
    new Employee { Id = 3, Name = "Alex" }
};

foreach (var person in developers)
{
    Console.WriteLine(person.Name);
}

foreach (var person in sales)
{
    Console.WriteLine(person.Name);
}
```

This is because both of these classes have a special method called `GetEnumerator()` - because both implement the `IEnumerable<T>` interface:

```cs
sales.GetEnumerator();
```

`GetEnumerable()` is really the only method that `IEnumerable` implements.

`IEnumerable<T>` is a very important interface for LINQ - all of the query operations that I can perform using LINQ operate off of this interface or another special interface we'll look at later.

`IEnumerable<T>` can represent a wide variety of data structures.

```cs
IEnumerator<Employee> enumerator = developers.GetEnumerator();
while (enumerator.MoveNext())
{
    Console.WriteLine(enumerator.Current.Name);
}
```

This would work on either the `sales` or `developers` collection - you can hide these behind this interface.

`MoveNext()` doesn't care if it's moving through an array or a list.

LINQ works by added extension methods - which allow you to work against an interface without changing that underlying type. We don't want to change `IEnumerable` - we don't want to add these methods on the interface (that just makes it harder to implement and to change in the future).

### Creating an Extension Method

Extension methods allow us to define a static method that appears to be a member of another type - anything - even sealed classes that we can't extend using inheritance.

```cs
public string class StringExtensions
{
    static public double ToDouble(this string data)
    {
        double result = double.Parse(data);
        return result;
    }
}
```

The `this` keyword tells the compiler that this is an extension method operating on the `string` class. `ToDouble()` now appears as if it's a method on the `string` class.

LINQ makes wide use of extension methods to extend interfaces like `IEnumerable<T>` - and all of the LINQ operators are all defined as extension methods.

Building `Count()` LINQ method:

```cs
public static class MyLinq
{
    public static int Count<T>(this IEnumerable<T> sequence)
    {
        int count = 0;
        foreach (var item in sequence)
        {
            count += 1;
        }
        return count;
    }
}
```

You can't implement an extension method that already exists on the type - ex. `ToString()`

### Understanding Lambda Expressions

Without lambda expressions, it could look like this:

```cs
//  With named method
IEnumerable<string> filteredList = cities.Where(StartsWithL);

public bool StartsWithL(string name)
{
    return name.StartsWith("L");
}
```

Becomes cumbersome to write a filtering and sorting operation for every use case in an application.

Another option with a delegate:

```cs
//  With anonymous method
IEnumerable<string> filteredList = cities.Where(delegate(string s) { return s.StartsWith("L"); });
```

A lambda expression is a short, concise syntax for defining a method I can invoke.

```cs
//  With lambda expression
IEnumerable<string> filteredList = cities.Where(s => s.StartsWith("L"));
```

### Using Func and Action Types

```cs
cities.Where(s => s.StartsWith("L"));
```

With intellisense, we can see that `Where()` takes `Func<Employee, bool>` as its parameter.

`Func<>` was introduced as an easy way to work with delegates - and delegates are types that allow me to create variables that point to methods.

A `Func<>` can take from 1 - 17 types. These describe the parameters and the return type of a method. The last generic type parameter always describes the return type - the ones prior to it always describe the parameters.

A method that takes an int as a parameter and returns an int: `Func<int, int>`

```cs
Func<int, int> square = x => x*x;

Console.WriteLine(square(3));       //  9
```

```cs
Func<int, int, int> add = (x, y) => x + y;

Console.WriteLine(square(add(3, 5)));       // 64
```

`Action` isn't something that you will use a lot with LINQ. LINQ mostly uses the `Func` type - but, an `Action` method always returns `void`. The generic type parameters only describe the incoming parameters to the method.

`Action<int>` will be a method that accepts a single integer and doesn't return anything.

```cs
Action<int> write = x => Console.WriteLine(x);

write(square(add(3, 5)));                   // 64
```

Here you see how you can chain these methods together to create an expression - this is what LINQ does.

Lambda expressions are just a way to create executable code. They are a way to define a method, but that method doesn't have to be an instance of a method or a static named method on a class. It's something that can easily be in-lined in a short expression.

### Using var for Implicit Typing

`var` allows the compiler to infer the type of the variable.

```cs
var name = "Scott";
var x = 3.0;
var y = 2;
var z = x * y;

//  All lines print "True"
Console.WriteLine(name is String);
Console.WriteLine(x is double);
Console.WriteLine(y is int);
Console.WriteLine(z is double);
```

The `var` keyword **doesn't** introduce dynamic typing. The variable is still strongly typed. I can't assign an `int` to the variable `name` because name is a `string`.

You can't use `var` when defining parameters to a method or a field or property on a class.

### Query Syntax versus Method Syntax

Query syntax:

```cs
string[] cities = { "Boston", "Los Angeles", "Seattle", "London", "Hyperaband" };

var filteredCities = from city in cities
                     where city.StartsWith("L") && city.Length < 15
                     orderby city
                     select city;
```

Query syntax ends in `select` or `group`. `select` appears at the end of a query in `C#` - it appears first in an `SQL` query.

The query syntax and the method syntax are equivalent in most cases, however, not every LINQ operator is available in query syntax - so, sometimes we must use method syntax. For example: `Count()`, `Take()`, `Skip()`

## LINQ Queries

## Filter, Ordering, and Projecting

## Joining, Grouping, and Aggregating

## LINQ to XML

## LINQ and the Entity Framework

## 