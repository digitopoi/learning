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

### Understanding Lambda Expressions

### Using Func and Action Types

### Using var for Implicit Typing

### Query Syntax versus Method Syntax

## LINQ Queries

## Filter, Ordering, and Projecting

## Joining, Grouping, and Aggregating

## LINQ to XML

## LINQ and the Entity Framework

## 