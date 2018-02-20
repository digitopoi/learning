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

### Adding models

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

### Adding Entity Framework Core NuGet Package

Save time by bringing in the provider - which downloads dependencies as well

```bash
Install-Package Microsoft.EntityFrameworkCore.SqlServer
```

### Creating the Data Model with EF Core

The DbContext will provide all the logic that EF is going to be using to its change tracking and database interaction tasks.

A DbContext needs to expose DbSet<>s which become wrappers to the different types that you'll interact with when using the context.

How you define your DbContext is important to how EFCore treats your data at runtime as well as how it's able to interact with your database - and how you use your model when you're coding.

Based on the relationships EF discovers in the classes, it will use its own conventions for how to infer the relationships.

One-to-many relationship between Samurai and Quote.
One-to-many relationshpi between Battle and Samurai.

Also affects how EF Core infers the database schema - whether you're working with an existing database or you're going to let EF create the database based on the models.

EF Core will presume the DbSet property names will match the table names.

SamuraiContext.cs

```c#
public class SamuraiContext : DbContext
{
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Battle> Battles { get; set; }
}
```

### Specifying the Data Provider and Connection String

**No More Database Magic:** you must specify data provider and connection string

Different ways to do this - one way is directly in the DbContext class (ok for demos and examples, not for real software):

The first time EF Core instantiates this DbContext at runtime, it will trigger the OnConfiguring() method, learn that it should be using the SqlServer provider, and be aware of the connection string.

```c#
public class SamuraiContext : DbContext
{
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Battle> Battles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server = (localdb)\\mssqllocaldb; Database = SamuraiAppData; Trusted_Connection = True; ");
    }
}
```

### Understanding EF Core Migrations

If you evolve your data model - EF Core's interpretation of the translation to the database will change.

Important to make sure that what EF Core **thinks** the database looks like is actually what the database looks like.

#### Migrations Workflow

1. Define Model Change

2. Create a Migration File

3. Apply Migration to DB or Script

### Adding Your First Migration

In order to create and execute migrations, we'll need access to the migration commands and to the migrations logic.

Commands: EntityFrameworkCore.Tools

Migrations Engine: EntityFrameworkCore.Design


```bash
Add-Migration Initial
```

### Inspecting Your First Migration

ModelSnapshot - where EF keeps track of the current state of the model (when you add a new migration - EF reads the snapshot and compares it to the new version to determine what needs to be changed).

Some notes on the migrations file:

#### Up()

Migrations read the configuration information that we supplied and knew that we targeted SQL Server:

```c#
.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
```

Creating tables and columns, specifying primary keys and foreign keys and constraints between the keys:

```c#
migrationBuilder.CreateTable(
    name: "Battles",
    columns: table => new
    {
        Id = table.Column<int>(nullable: false)
            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
        EndDate = table.Column<DateTime>(nullable: false),
        Name = table.Column<string>(nullable: true),
        StartDate = table.Column<DateTime>(nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Battles", x => x.Id);
    })
```

Specifies indexes - by convention, creates Index for every Foreign Key found in the model:

```c#
migrationBuilder.CreateIndex(
    name: "IX_Quotes_SamuraiId",
    table: "Quotes",
    column: "SamuraiId");

migrationBuilder.CreateIndex(
    name: "IX_Samurais_BattleId",
    table: "Samurais",
    column: "BattleId");
```

#### Down()

Used if we ever want to "unwind" a migration

### Using Migrations to Script or Directly Create the Database

#### Generate Script from Migration

More commmon on production database - generate script, let resident DB expert take care of applying it to production database.

```bash
Script-Migration
```

```sql
IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Battles] (
    [Id] int NOT NULL IDENTITY,
    [EndDate] datetime2 NOT NULL,
    [Name] nvarchar(max) NULL,
    [StartDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Battles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Samurais] (
    [Id] int NOT NULL IDENTITY,
    [BattleId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Samurais] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Samurais_Battles_BattleId] FOREIGN KEY ([BattleId]) REFERENCES [Battles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Quotes] (
    [Id] int NOT NULL IDENTITY,
    [SamuraiId] int NOT NULL,
    [Text] nvarchar(max) NULL,
    CONSTRAINT [PK_Quotes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Quotes_Samurais_SamuraiId] FOREIGN KEY ([SamuraiId]) REFERENCES [Samurais] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Quotes_SamuraiId] ON [Quotes] ([SamuraiId]);

GO

CREATE INDEX [IX_Samurais_BattleId] ON [Samurais] ([BattleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180219234455_Initial', N'2.0.1-rtm-125');

GO
```

#### Update Database from Migration

Notice in the Migration, there's no code to create the database - handled by the internal code inside of migrations - first checks to see if the database exists or not. If it doesn't exist, EF will create it. If you've created a script - you'll be responsible for creating the database yourself.

The verbose flag lets you see everything the update-database command is doing.

```bash
Update-Database -verbose
```
