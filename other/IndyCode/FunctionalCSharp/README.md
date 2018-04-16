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

**Func<T1, T2, TResult>** - 

### Higher Order Functions

A function that accepts another function as a parameter, or returns another function.

- all LINQ methods

### Expressions Instead of Statements

Statements define an action and are executed for their side-effect.

Expressions produce a result without mutating state.

- ex. ternary operators

### Method Chaining (~Pipelines)

C# lacks Pipeline syntax - instead, we use extension methods

### Extension Methods

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