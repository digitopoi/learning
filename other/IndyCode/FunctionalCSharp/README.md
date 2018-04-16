# Going All In With Fuctional C# - Ed Charbeneau

## Functional Programming

Imperative programming is dependent on state. Functional programming aims to be less dependent.

Not mutually exclusive - don't have to be 100% imperative or functional.

inputs, outputs - avoid maintaining state, separation between data and behavior.

### Why?

Multi-threaded applications relying on state and accessing at the same time - can cause problems.

Readability, maintainability

### C# Features supporting Functional

3.0 - LINQ, => expressions, extension methods (added to support LINQ)

6.0 - expression bodied members (allow us to use => in more places)

7.1 - Tuples, Pattern matching (not implemented very well yet), ++ expression bodied members

### Immutable Types

An object whose state cannot be modified after it is created, lowering the risk of side-effects.

BEFORE:

```cs
public class Rectangle
{
    public int Length { get; set; }
    public int Height { get; set; }

    public void Grow(int length, int height)
    {
        Length += length;
        Height += height;
    }
}

Rectangle r = new Rectangle();
r.Length = 5;
r.Height = 10;
r.Grow(10, 10);
//  r.Length is 15, r.Height is 20, same instance of r
```

AFTER:

```cs
public class ImmutableRectangle
{
    int Length { get; }
    int Height { get; }

    public ImmutableRectangle(int length, int height)
    {
        Length = length;
        Height = height;
    }

    public ImmutableRectangle Grow(int length, int height) =>
        new ImmutableRectangle(Length + length, Height + height)
}

ImmutableRectangle r = new ImmutableRectangle(5, 10);

r = r.Grow(10, 10);
//  r.Length is 15, r.Height is 20, is a new instance of r
```

### Func Delegates

Func Delegates encapsulate a method. When declaring a Func, input and output parameters are specified as T1-T16 and TResult.

**Func<TResult>** - matches a method that takes no arguments, and returns value of type **TResult**.

**Func<T, TResult>** - matches a method that takes an argument of type `T`, and returns value of type `TResult`.

**Func<T1, T2, TResult>** - matches a method that takes arguments of type `T1` and `T2`, and returns a value of type `TResult`.

**Func<T1, T2, ..., TResult>** - and so on up to 16 arguments, and returns a value of type `TResult`.

```cs
Func<int, int> addOne = n => n + 1;
Func<int, int, int> addNums = (x, y) => x + y;
Func<int, bool> isZero = n => n == 0;

Console.WriteLine(addOne(5));               //  6
Console.WriteLine(isZero(addNums(-5, 5)));  //  True

int[] a = { 0, 1, 0, 3, 4, 0 };
Console.WriteLine(a.Count(isZero));         //  3
```

### Higher Order Functions / Function as Data

A function that accepts another function as a parameter, or returns another function.

- all LINQ methods

### Expressions Instead of Statements

Statements define an action and are executed for their side-effect.

Expressions produce a result without mutating state.

- ex. ternary operators

STATEMENT:

```cs
public static string GetSalutation(int hour)
{
    string salutation;  //  placeholder value
    if (hour < 12)
        salutation = "Good Morning"
    else
        salutation = "Good Afternoon";

    return salutation;  //  return mutated variable
}
```

EXPRESSION:

```cs
public static string GetSalutation(int hour) =>
    hour < 12 ? "Good Morning" : "Good Afternoon";
```

### Method Chaining (~Pipelines)

C# lacks Pipeline syntax - instead, we use extension methods.

```cs
string str = new StringBuilder()
    .Append("Hello")
    .Append("World")
    .ToString()
    .TrimEnd()
    .ToUpper();

//  HELLO WORLD
```

```cs
Html.Kendo()
    .Grid(Model)
    .Name("grid")
    .Columns(columns =>
    {
        columns.Bound(product => product.ProductID);
        columns.Bound(product => product.ProductName);
        columns.Bound(product => product.UnitsInStock);
    })

//  Render HTML Data Grid
```

### Extension Methods

Extension methods are a great way to extend method chains and add functionality to a class.

```cs
//  Extends the StringBuilder class to accept a predicate
public static StringBuilder AppendWhen(this StringBuilder sb, string value, bool predicate) =>
    predicate ? sb.Append(value) : sb;

//  usage
string htmlButton = new StringBuilder()
    .Append("<button")
    .AppendWhen(" disabled", isDisabled)
    .Append(">Click me</button>")
    .ToString();
```

* Zip LINQ - lookup later

## Today's Workshop

- Score a poker game

- use imperative programming

- refactor with functional programming

- Have fun

- Workshop materials:

http://edweb.me/3s - workshop
http://edweb.me/30 - digital cheatsheet


Constructor with expression bodied member:
```cs
public Card(CardValue value, CardSuit suit) => (Value, Suit) = (value, suit);
```