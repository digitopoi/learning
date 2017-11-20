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

## Inserting Data (and a couple of other things)

### Inserting Data

```sql
INSERT INTO cats(name, age) VALUES ('Jetson', 7);
```

some people prefer:
```sql
INSERT INTO cats
            (NAME,
            age)
VALUES      ("Jetson",
            7);
```

* The order you write the column names matters. You can declare them in the INSERT part in any order - but, you have to follow the same order in the VALUES part.

### Super Quick Intro To SELECT

SELECT is covered in more depth in the next section - showing now so we can see what is added.

```sql
SELECT * FROM cats;
```

### Multiple INSERT

You can use INSERT to bulk-insert data

```sql
INSERT INTO cats(name, age)
VALUES ('Charlie', 10)
      ,('Sadie', 3)
      ,('Lazy Bear', 1);
```

### INSERT Challenges

Create a people table
- first_name - 20 char limit
- last_name - 20 char limit
- age - int

Insert Your 1st Person
- Tina Belcher, 13 years old

Insert Your 2nd Person (using different order)
- Bob Belcher, 42 years old

Write one multiple insert:
- Linda Belcher, 45 years old
- Phillip Frond, 38 years old
- Calvin Fischoeder, 70 years old


```sql
    CREATE TABLE people
      (
        first_name VARCHAR(20),
        last_name VARCHAR(20),
        age INT
      );

    INSERT INTO people(first_name, last_name, age)
    VALUES ('Tina', 'Belcher', 13);

    INSERT INTO people(age, last_name, first_name)
    VALUES (42, 'Belcher', 'Bob');

    INSERT INTO people(first_name, last_name, age)
    VALUES('Linda', 'Belcher', 45)
      ,('Phillip', 'Frond', 38)
      ,('Calvin', 'Fischoeder', 70);
```

### NULL and NOT NULL

**NULL** means "the value is not known"

**NULL** DOES NOT MEAN 'ZERO'!

By default, columns allow null values

Right now we could do this (not provide age):
```sql
INSERT INTO cats(name)
VALUES ('Alabama');
```

or even this:

```sql
INSERT INTO cats()
VALUES ();
```

When we define the table, we can specify that a column should not allow nulls

```sql
CREATE TABLE cats
    (
        name VARCHAR(100) NOT NULL,
        age INT NOT NULL
    );
```

### Setting Default Values

If a column is specified as NOT NULL, SQL will assign a default value to a row that is missing a value. 

If it is a string, it will insert an empty string, if it is an integer, it will insert 0.

You can also define default values as fallbacks.

```sql
CREATE TABLE cats
    (
        name VARCHAR(100) DEFAULT 'unnamed',
        age INT DEFAULT 99
    );
```

Isn't this redundant?

```sql
CREATE TABLE cats
    (
        name VARCHAR(100) NOT NULL DEFAULT 'unnamed',
        age INT NOT NULL DEFAULT 99
    );
```

No! If NOT NULL is not specified - you could still manually set something to be null.

### A Primer on Primary Keys

- Right now, we could insert identical data

- Why is this a problem?

  - we want our data to be uniquely identifiable

We need to have a new field with an Id - even if the cats have the same name and age - we can still identify them individually

**Primary key** - a unique identifier

```sql
CREATE TABLE unique_cats (cat_id INT NOT NULL
                        , name VARCHAR(100)
                        , age INT
                        , PRIMARY KEY (cat_id));
```

If you try to add an entry with a key that is already in the database, it will not add it.

Instead of manually enterring the Id when you insert new data - you can have it increment it programatically

```sql
CREATE TABLE unique_cats (cat_id INT NOT NULL AUTO_INCREMENT,
                          name VARCHAR(100),
                          age INT,
                          PRIMARY KEY (cat_id));
```

### Table Constraits Exercise

Define an Employees table with the following fields:
- id - number (automatically increments), mandatory, primary key
- last_name - text, mandatory
- first_name - text, mandatory
- middle_name - text, not mandatory
- age - number, mandatory
- current_status - text, mandatory, defaults to 'employed'

```sql
CREATE TABLE employees 
    (
        id INT AUTO_INCREMENT NOT NULL,
        first_name VARCHAR(255) NOT NULL,
        last_name VARCHAR(255) NOT NULL,
        middle_name VARCHAR(255),
        age INT NOT NULL,
        current_status VARCHAR(255) NOT NULL DEFAULT 'employed',
        PRIMARY KEY(id)
    );

--alternate way of defining a primary key:
CREATE TABLE employees2 
    (
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        first_name VARCHAR(255) NOT NULL,
        last_name VARCHAR(255) NOT NULL,
        middle_name VARCHAR(255),
        age INT NOT NULL,
        current_status VARCHAR(255) NOT NULL DEFAULT 'employed'
    );

--test insert:
INSERT INTO employees(first_name, last_name, age) 
VALUES ('Dora', 'Smith', 58);
```

## CRUD Commands

### Official Introduction to SELECT

How do we retrieve and search data in a database?

```sql
SELECT * FROM cats;
```

The '*' means to select all of the columns

#### Select Expression

What columns do you want?

```sql
SELECT name FROM cats;

SELECT age FROM cats;

SELECT cat_id FROM cats;


-- selecting multiple columns (order matters):
SELECT name, age FROM cats;
```

### Introduction to WHERE

Allows us to get more specific (rather than selecting a whole column)

We'll use WHERE all the time - not just for searching / selecting

```sql
SELECT * FROM cats WHERE age=4;
SELECT * FROM cats WHERE name="Egg";
```

with comparing two columns:
```sql
SELECT cat_id, age FROM cats WHERE cat_id=age;
```

### Introduction to Aliases

Easier to read results

```sql
SELECT cat_id AS id, name FROM cats;
```

Our data is clear and straightforward right now - so, not clear why this is useful.

Will become more useful as the data becomes more complicated (more tables, with column names that are the same, for example)

### The UPDATE command

How do we alter existing data?


Find, in the cats table, all of the cats who have a breed of 'Tabby' and change that breed to 'Shorthair'
```sql
UPDATE cats SET breed='Shorthair'
WHERE breed='Tabby';
```

```sql
UPDATE cats SET age=14
WHERE name='Misty';
```

**A good rule of thumb**- try selecting before you update (to make sure you're targeting the right data)

### Introduction to DELETE

Same syntax as SELECT (just use DELETE keyword)

Also, make sure to SELECT first to make sure you're selecting what you want.

```sql
DELETE FROM cats WHERE name='Egg';
```


