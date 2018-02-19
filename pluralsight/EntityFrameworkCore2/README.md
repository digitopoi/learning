# Entity Framework Core 2: Getting Started

[Julie Lerman](https://app.pluralsight.com/library/courses/entity-framework-core-2-getting-started/table-of-contents)

## How EF Core Works

What EF does for you:

1. Keep track of the state of objects that it's aware of.

2. Determine SQL that needs to be run on the database

3. Create objects from the query results

### Model Creation Options

EF has a set of rules to infer what the database schema will look like and what objects coming back from the database should look like.

For example, it assumes that property names should match column names.

Table name pluralization

### Basic Workflow

1. Define the model

2. Express and execute query: (from p in people select p).ToList()

3. EF determines & executes SQL: (SELECT * from people)

4. EF transforms results into your types

5. User modifies the data

6. Code triggers save (DbContext.SaveChanges())

7. EF determines & executes SQL

### New Features in EF Core 2.0

#### Querying

EF.Functions.Like()

Improved LINQ translation

Global query filters (per type)

Explicitly compiles queries

GroupJoin SQL is better

#### Mapping

Scalar UDF mapping

Type Configuration

Owned Entities (replaces EF6 Complex type/value object support)

Table splitting

#### Performance and more

DbContext pooling

String interpolation in raw SQL

Consolidate logging and diagnostics

Easier service replacement

## Creating a Data Model and Database with EF Core 2

Samurai.cs

```c#
public class Samurai
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Quote> Quotes { get; set; }
    public int BattleId { get; set; }

    public Samurai()
    {
        Quotes = new List<Quote>();
    }
}
```

Battle.cs

```c#
public class Battle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Samurai> Samurais { get; set; }
}
```

Quote.cs

```cs
public class Quote
{
    public int Id { get; set; }
    public string Text { get; set; }
    public Samurai Samurai { get; set; }
    public int SamuraiId { get; set; }
}
```

## Adding Entity Framework Core NuGet Package

Save time by bringing in the provider - which downloads dependencies as well

```bash
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

