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

### Adding Many-to-many and One-to-one Relationships

#### Many-to-Many

Rather than have a Samurai be in a single battle (as it is currently set up) - it should be possible for one samurai to fight in many battles and many samurais fight in a single battle.

In relational databases - you have a join table to link the Samurais and the Battles

In EF Core - you have a Join entity to link the two classes

Samurai <---> Join Entity <---> Battles

Join class - SamuraiBattle.cs:

```c#
public class SamuraiBattle
{
    public int SamuraiId { get; set; }
    public Samurai Samurai { get; set; }
    public int BattleId { get; set; }
    public Battle Battle { get; set; }
}
```

Add collection of SamuraiBattles to the Samurai class:

```c#
public class Samurai
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Quote> Quotes { get; set; }
    public List<SamuraiBattle> SamuraiBattles { get; set; }             //  Added

    public Samurai()
    {
        Quotes = new List<Quote>();
    }
}
```

And the battle class:

```c#
public class Battle
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<SamuraiBattle> SamuraiBattles { get; set; }
}
```

However, EF Core can't infer this relationship and won't be able to map to the database nor translate queries and updates

It's possible to assist EF Core in understanding our intent when it's not following its own conventions.

Using Fluent API mappings in DbContext's OnModelCreating() method that gets called internall when EF Core is working out what the data model should look like.

Tell it that it has a key composed from its SamuraiId and BattleId properties

```c#
public class SamuraiContext : DbContext
{
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Battle> Battles { get; set; }

    public SamuraiContext(DbContextOptions<SamuraiContext> options)
        : base(options)
    { }

    //  Helping out EF Core
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SamuraiBattle>()
            .HasKey(s => new { s.SamuraiId, s.BattleId });
    }
}
```

Now EF Core will be able to build SQL for queries and database updates that respect this many-to-many relationship.

#### One-to-one Relationship

Add class to keep track of Samurai's secret name:

```c#
public class SecretIdentity
{
    public int Id { get; set; }
    public string RealName { get; set; }
    public int SamuraiId { get; set; }
}
```

Add navigation property to define the secret identity:

```c#
public class Samurai
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Quote> Quotes { get; set; }
    public List<SamuraiBattle> SamuraiBattles { get; set; }
    public SecretIdentity SecretIdentity { get; set; }

    public Samurai()
    {
        Quotes = new List<Quote>();
    }
}
```

This is enough for EF Core to figure out the relationship between the entities.

##### Required vs. Optional Relationships in One-to-One

The dependent end of a one-to-one relationship is always optional as far as EF Core is concerned.

There is no way to apply that constraint in the model or the database.

If you want to require that a Samurai **always** has a secret identity - you'll have to do that in your own business logic.

(Default) Required parent - a child cannot be orphaned
Optional parent - you'll need to configure this mapping explicitly

DEPENDENT:
(Default) Optional child - EF Core (& database) will always allow a null child
Required child - you'll need to handle the requirement in your business logic

### Reverse Engineering an Existing Database

Create DbContext & classes from database

Updating model is currently not supported

Transition to migrations is not pretty - links in resources

Powershell command: scaffold-dbcontext

```bash
scaffold-dbcontext
```

## Interacting with the EF Core Data Model

### Getting EF Core to Output SQL Logs

Install Microsoft.Extensions.Logging.Console package

Configure context to always output its logs to a console window.

```c#
public class SamuraiContext : DbContext
{
    public DbSet<Samurai> Samurais { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<Battle> Battles { get; set; }

    public static readonly LoggerFactory MyConsoleLoggingFactory        //  Add logger
        = new LoggerFactory(new[]
        {
            new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
                    && level == LogLevel.Information, true)
        });

    public SamuraiContext(DbContextOptions<SamuraiContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SamuraiBattle>()
            .HasKey(s => new { s.SamuraiId, s.BattleId });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLoggerFactory(MyConsoleLoggingFactory);
    }
}
```

Adding two filters to the logger:

1. Only show SQL commands (avoid logging processing messages, connections, etc.).

2. Define the level of detail - in this case, basic information to keep out information like stack traces and error messages

### Inserting Simple Objects

Tracking: the context is tracking the samurai when you call Add() on the DbSet

```c#
private static void InsertSamurai()
{
    var samurai = new Samurai { Name = "Julie" };
    using (var context = new SamuraiContext())
    {
        context.Samurais.Add(samurai);
        context.SaveChanges();
    }
}
```

context                 Examine each tracked object
.Samurais               For each object, read the context's understanding of the state of the object
.Add(samurai);          The add method let's EF know it's knew and EF works out SQL commands

context                 
.SaveChanges();         Execute the SQL statement on the database