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

### Func Delegates

Func Delegates encapsulate a method. When declaring a Func, input and output parameters are specified as T1-T16 and TResult.

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