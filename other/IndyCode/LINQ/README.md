# Giving Clarity to LINQ Queries by Extending Expressions - Ed Charbeneau

`In this session we’ll learn about .Net Expression trees by discovering how they work and applying the knowledge to LINQ using the pipes and filters pattern. LINQ and Entity Framework are both commonly used in the .Net ecosystem, but even well-written applications can have LINQ queries that are difficult to understand. Because LINQ is so flexible, it can be written in ways that fail to communicate the developer’s intent. Well-written LINQ should be so clear as to be self-documenting. To write clear LINQ, it helps to understand the details of a few LINQ components that improve LINQ’s readability.`

`We’ll be showing how to use a pipe and filter pattern to make LINQ queries easier to comprehend. We will take a deep dive into expression trees to understand how they work, and how to manipulate them for maximum re-usability.`

[GitHub](https://github.com/EdCharbeneau/FiltersAndRules)

[Article](goo.gl/kAM05P)

### Entity Framework and LINQ

```cs
People.Where(p => p.Title == p.Name)
```

```cs
Func<TResult, TSource>
```

Expresions in C#

Representation of code as data

Meta-programming - analyze, rewrite, and translate the code at runtime.

Same syntax for executable code as data representation - homoiconicity (?) - one way of writing that does two diffrent things

```cs
//  Executable representation
Func<int, int> plusOne = n => n + 1;

//  Data representation
Expression<Func<int, int>> plusOne = n => n + 1;
```

### Expression Factory

Can't new up an expression. Have to use factory:

Not executable - representation of executable

```cs
//  new Expression() invalid
var x = Expression.Contant(1);

var y = Expression.Contant(1);

var sum = Expression.Add(x, y);

Console.WriteLine(sum);             //  (1 + 1)
```

```cs
Expression<Func<int, int>> plusOne = n => n + 1;

//  To string = n => (n + 1)
```

### Runtime Modification - Expression visitor

used to traverse or rewrite expression trees

Abstract class (inherit and override)

`.Visit(expression)` - recursively walks the tree, returns an expression

### How is this useful?

Entity Framework uses this to translate your C# to SQL

### Pipes and Filters

Clean-coding and maintainability.

`dbContext.Where(rule).Where(rule)`

```cs
postRepository.GetAll()
    .Where(post => post.IsPublished && post.PostedOn >= Today)
```

#### Distinction between IEnumerable and IQueryable

```cs
//IEnumerable
Func<T, bool>

//IQueryable
Expression<Func<T, bool>
```

### Custom Filters

