# Functional Programming With C#

## Why Functional Programming Matters

`No matter what language you work in, programming in a function style provides benefits. You should do it whenever it is convenient, and you should think hard about the decision when it isn't convenient. --John Carmack`

When you apply the basic functional principles to your software, your resulting product will be more predictable, reliable, and maintainable in the long run.

Clean Code chapter on functions:

1. Keep functions small

2. Don't repeat yourself

3. Do one thing

4. Avoid side-effects

5. Accept no more than 3 parameters

Are OOP and functional programming so different?

`OO makes code understandable by encapsulating moving parts. FP makes code understandable by minimizing moving parts. --Michael Feathers`

OOP is typically about trying to manage the ever-changing state of a system.

FP is about trying to avoid that state all together.

Methods often depend upon and modify the data associated with the class. Functions in a functional program should depend only on their inputs and should not make changes to those values or other shared state.

Software written in a function style is generally more predictable, easier to maintain, and easier to test.

## Defining Functional Programming

**functional programming**: a paradigm that concentrates on computing results rather than performing actions.

Functional program focuses on defining functions in terms of their input and output, rather than how they change system state.

Helps us recognize the separation between data and behavior.

We may find ourselves writing more static methods than instance methods.

### Three Themes of Function Programming

#### 1. Taming Side Effects

**side effect**: any accompanying or consequential and usually detrimental effect

Anything that happens to the state of a system as a result of invoking a function.

We need our software to have some side effects. We're focused on taming side effects, rather than eliminating them.

Pure functional language like Haskell don't allow your functions to have side effects except very narrow cases.

C# is an impure language, and doesn't enforce no side effects. But, we can get this functionality by applying some discipline to our designs.

The primary way to do this is by enforcing immutability within our types.

#### 2. Expression-based

Everything produces some result.

In contrast to statement based languages like C#, where many language constructs do not produce a direct result.

Statements: define actions, executed for their side-effect

```c#
string posOrNeg;

if (value > 0)
    posOrNeg = "positive";
else
    posOrNeg = "negative";
```

Expressions: Produce results, executed for their result

```c#
var posOrNeg = 
    value > 0
        ? "positive"
        : "negative";
```

Favoring expressions over statements is that expressions are naturally composable. Because expressions return a value, they can easily be combined with other expressions or statements by using them in place of a variable or other value.

Filling in values - COMPOSABILITY:

Statements:

```c#
string posOrNeg;

if (value > 0)
    posOrNeg = "positive";
else
    posOrNeg = "negative";

var msg = $"{value} is {posOrNeg}";
```

We have to use the variable posOrNeg, even though it is probably not used outside of this context.

Expressions:

```c#
var msg = $"{value} is {(value > 0 ? "positive" : "negative")}";
```

Eliminates extraneous variable.

#### 3. Treat Functions as Data

Treating functions the same as any other data type.

C# does this through delegation. Module 3 will go in-depth into this.

Treating functions as data enables **higher-order functions**.

1. Functions which accept other functions

2. Functions which return functions

3. 

Allow us to think at different levels of abstraction than OOP.

Ultimately, higher order functions allow us to compose functionality, not through complex inheritance structures, but by substituting behavior according to a strict definition (function signature).

LINQ is built on functional principles. "Gateway to functional programming"

LINQ is a series of independent technologies including generics, anonymous types, extension methods, delegation, and expression trees. These technologies combine to allow querying data from a variety of data sources in a highly functional manner. 

We'll focus on LINQ to objects - focuses primarily on extension methods, generics, and delegation.

**generics**: generics allow us to safely operate against strongly typed sequences while still providing full visibility into the sequence.

**extension methods**: higher order functions which add functionality such as sorting, filtering, or transforming the value of any type that implements IEnumerable<T>

**delegation/lambda**: allow us to easily define how the extension methods operate against the sequence.

Traditional imperative approach:

```c#
var ix = 0;

while (ix < myList.Count)
{
    if (myList[ix] % 2 != 0)
    {
        myList.RemoveAt(ix);
    }
    else
    {
        ++ix;
    }
}

myList.Sort();
```

Presence of the while loop indicates the presence of a mutable state. Some side effect within the loop must change something external for the loop to stop. BAD.

We have another side effecting method call: myList.Sort(). How can we tell that Sort has a side effect? It's a void method. If void didn't do anything, executing it would just be silly. Because it doesn't return anything, we must be invoking it solely for its side effect.

Sort does sort the list, but it does so in place - **mutating** the list.

Almost entirely statement based. Better as a series of expressions.

Obscures patterns that functional language have used for decades.

Expression based (LINQ query syntax)

```c#
from x in myList
where x % 2 == 0
orderby x
select x;
```

The above style is rarely used - syntactic sugar that hides what's really going on.

Many LINQ extension methods aren't accessible through the contextual keywords. Those that are often have useful overloads that are accessible only through the method syntax.

When you remove the syntactic sugar - it's just generics, extension methods, and delegation!

Expression based (LINQ method syntax)

```c#
myList
    .Where(x => x % 2 == 0)
    .OrderBy(x => x);
```

We just tell the higher-order functions how to handle each element in the sequence and wait for the return values. The LINQ extension methods form a chain of higher-order functions that each operate against an instance of IEnumerable<T>.

myList is never changed here. Rather than mutating the original list, the LINQ extension methods create new sequences, where each item in the input sequence has been mapped to an item in the resulting sequence according to the corresponding lambda expression.