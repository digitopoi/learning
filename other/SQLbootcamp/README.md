# MySQL Bootcamp

## Getting Started: Overview and Installation

### What is a database?

1. A collection of data

But, think about a phonebook. 

If you have a phonebook - think about the query: Find "Ned Flander's phone number." That's easy with a phone book. 

But, think about the query: "Find all Neds in the phone book." - that's REALLY hard with just a phone book.

2. A method for accessing and manipulating that data

Going back to the phonebook example - need a way for more difficult queries than humans can easily perform

#### Database vs. Database Management Ssytem

Database Management Systems allow us to interface with the database

[Your app] ===> [DBMS] ===> [Database]

Sometimes, when people talk about a database - they're actually talking about the database AND the DBMS

PostregSQL, MySQL, Oracle, SQLite are all DBMSs

#### Synthesized definition

**database**- A structured set of computerized data with an accessible interface

### SQL vs. MySQL

#### SQL

**S**tructured **Q**uery **L**anguage

SQL is the language we use to "talk" to our database

"Find all users who are 18 or older"
```sql
SELECT * FROM Users WHERE Age >= 18;
```

#### MySQL

Working with MySQL is primarily **writing SQL**

There are slight differences in syntax when working with MySQL vs. another DBMS

There is a standard for how SQL should work. All of the DBMS implement this standard (using the strict standard will work in any DBMS). 

#### Two takeaways:

1. Once you learn SQL, it's pretty easy to switch to another DBMS that uses SQL

2. What makes DBMSs unique are the features they offer, **not** the language

### Installation Overview

#### Three options:

1. Install it on a PC

2. Install it on a Mac

3. Install it on Cloud9 <-- easiest way to get started

### Installing MySQL on Cloud9

These AREN'T general MySQL commands (specific to Cloud9!!)

1. **start** MySQL - will create an empty database on first start
```console
mysql-ctl start
```

2. **stop** MySQL
```console
mysql-ctl stop
```

3. run the MySQL interactive shell
```console
mysql-ctl cli
```

#### Exiting the shell:

exit;

quit;

\q;

or type ctrl-c

### First SQL activity

Try entering the following commands:

```console
help;                       //  help menu

show databases;             //  displays available databases

select @@hostname;          //  returns the hostname string
```

## Creating Databases and Tables

### Creating Databases

Currently we have a database server running on our Cloud9 instance (from mysql-ctl start)

1. show databases;

to see a list of preexisting databases 

2. CREATE DATABASE <name>;

- avoid spaces!

```console
CREATE DATABASE DogApp;
```

### Dropping Databases

- remember to be careful with this command - once you execute it, the database is gone!

```console
DROP DATABASE <name>;
```

### Using Databases

In MySQL there is a "USE" command

```console
USE dog_walking_app;
```

It tells MySQL which database we want to be working with

In MySQL - to see which database you're currently using:

```console
SELECT database();
```

### Introduction to Tables

The True Heart of SQL

A database is just a bunch of **tables** (in a relational database, at least)

Tables hold the data!

**table**- a collection of related data held in a structured format within a database

Databases are made up of lots and lots of tables - it's usually not just one.

#### Cat example
| Name          | Breed           | Age  |
| :-----------: | :-------------: | ---- |
| Blue          | Scottish Fold   | 1    |
| Rocket        | Persian         | 3    |
| Monty         | Tabby           | 10   |
| Sam           | Munchkin        | 5    |

#### Columns

Columns are headers

| Name          | Breed           | Age  |
| :-----------: | :-------------: | ---- |

Componenents of the data

#### Row

The actual data

### The Basic Datatypes

When you create a table, you have to specify what type of data each column will hold

There are MANY datatypes available to use

We're going to focus on two:

1. int - a whole number

2. varchar - a variable length string

| Name (varchar(100))  | Breed (varchar(100))  | Age  |
| :------------------: | :-------------------: | ---- |
| Blue                 | Scottish Fold         | 1    |
| Rocket               | Persian               | 3    |
| Monty                | Tabby                 | 10   |
| Sam                  | Munchkin              | 5    |

### Basic Datatypes Challenge

Draw a Tweets table
- The username (max 15 chars)
- The tweet content (max 140 chars)
- Number of favorites


Answers:
Username - varchar(15)
Content - varchar(140)
Favorites - int

### Creating Your Own Tables

generic example:
```sql
CREATE TABLE tablename
  (
      column_name data_type,
      column_name data_type
  );
```

[table names should be plural]

cats example:
```sql
CREATE TABLE cats 
  (
      name VARCHAR(100),
      age INT
  )
```

### How do we know it worked?

1. Just shows table names:
```sql
SHOW TABLES;
```

2. 
```sql
SHOW COLUMNS FROM <tablename>;
```
  - or (different command, but in this context, it works the same):
```sql
DESC <tablename>;
```

### Dropping Tables

```sql
DROP TABLE <tablename>;
```

### Creating Your Own Tables Challenge

1. Create a **pastries** table

2. It should include at least 2 columns: name and quantity. Name is 50 chars max.

3. Inspect your table/columns in the CLI

4. Delete your table

