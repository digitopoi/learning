# PHP with MySQL Essential Training: 1 The Basics

## Blueprinting

Blueprinting is highly recommended as a first step.

Draw diagrams or write up notes on the structure.

Clarifies the work ahead.

Detects obstacles and points of confusion early.

Unclutters your brain.

Makes it easier to develop section-by-section.

Blueprint provides a constant reference for how sections fit into the whole project.

### Outline

Two areas:

#### Public Area

Simple.

Site Pages

- navigation

- page content

- read only

#### Admin Area

Login Page

- form

  - username

  - password

Admin Menu

- manage content

- manage admins

- logout

Manage Content

- subjects

- pages

Manage admins

- admins

Logout

- do logout

- back to login

## Structure of Work Area

### Basic Project Template:

basic_project_template
    public/
        index.php
        images/
        stylesheets/
    private/
        initialize.php
        functions.php
        shared/

### One way to organize

globe_bank/public/staff
    index.php
    page_edit.php
    page_delete.php
    page_list.php
    page_new.php
    page_show.php
    subject_edit.php
    subject_delete.php
    subject_list.php
    subject_new.php
    subject_show.php

But, it doesn't scale well for a large application

### Better way to organize:

globe_bank/public/staff/
    index.php
    pages/
        edit.php
        delete.php
        index.php
        new.php
        show.php
    subjects/
        edit.php
        delete.php
        index.php
        new.php
        show.php

## Include and Require Files

If we define a function that we want to use on one webpage of our site, and we need to use it again on another page, we don't want to copy and paste that a second time.

It's much better if we can put that function in a single file, and then load it into both PHP pages, so that they're using the same version of the function all the time.

```php
<?php include("functions.php"); ?>
```

### Types of files

- Libraries and functions

- Layout sections: header, footer, navigations, sidebar

- Reusable sections of code (HTML, PHP, Javascript)

### Commands

- include()

- require()

  - works like include(), but raises an error if it's not found and able to be loaded.

  - use require() if the file is essential to the operation of the page

- include_once()

  - keeps track when the file is loaded, will skip if it's already loaded

  - important for functions - you don't want to define a function more than once

- require_once()

```php
<?php

    require_once('functions.php');

?>
```

**Use static strings inside include() and require()**

Don't use dynamic data - creates security issues

## Make Page Assets Reusable

How we can use variables in PHP to work with these included and required files.

```php
<?php $page_title = 'Staff Menu'; ?>
```

```php
<title>GBI - <?php echo $page_title; ?></title>
```

## Links and URLs

Instead of:

```html
<a href="index.php">Menu</a>
```

It's:

```php
<a href="<?php echo 'index.php'; ?>">Menu</a>
```

## URL Parameters

```php
somepage.php?page=2
```

URL/query parameters are the part of the URL that comes after the question mark.

[nameOfParameter]=[valueOfParameter]

Generally modify the behavior of the code used for the response.

Can return more than one parameter by using an ampersand between them.

```php
somepage.php?category=7&page=3
```

Allow us to pass data from one page to another via links.

### Super global variable

When a new page request is received with URL parameters, PHP is automatically going to take all of those URL parameters that were sent and put them into an associative array where they can be accessed.

**Super global variable:** $_GET

super global variables will always be all-caps with an underscore

Super global variables are **always** available in **all** variable scopes

Assigned by PHP before processing page code.

```php
<?php

$page = $_GET['page'];

?>
```

**values that you retrieve from super globals are always going to be strings**

You have to explicitly convert it if you want to change it:

```php
<?php

//  Values are always strings

$page = $_GET['page'];
echo gettype($page);
//  'string'

$page_as_int = (int) $_GET['page'];
echo gettype($page_as_int);
//  'integer'

?>
```

## Default Values for URL Parameters

If page wasn't sent in the URL, and we had code requesting that value - the associative array wouldn't have the value and we'd either get a warning or a notice to us telling us that the index can't be found. 

Can configure PHP to not show the warning - but, better to deal with the problem:

```php
$page = $_GET['page'];

if (isset($_GET['page'])) {
    $page = $_GET['page'];
} else {
    $page = '1';
}
```

More clean to use ternary conjuction:

```php
$page = isset($_GET['page']) ? $_GET['page'] : '1';
```

PHP 7 uses the null coalescing operator:

```php
$page = $_GET['page'] ?? '1';
```

Checks to see if value is present to the left of ??.

If there is, it uses it. If not, it uses the second value as the default.

## Encode URL Parameters

How to send data that could include certain special characters that require extra attention.

Example:

Imagine we have a link that we want to use that passes along a query parameter that is the name of the company.

```php
<a href="page.php?company=Widgets&More">Link</a>
```

The & has special meaning to the URL. (joining together more than one URL parameter)

We want the & to be seen as being data.

### Reseved Characters in URLs

! = %21

\# = %23

$ = %24

% = %25

& = %26

' = %27

( = %28

) = %29

\* = %2A

\+ = %2B

, = %2C

/ = %2F

: = %3A

; = %3B

= = %3D

? = %3F

@ = %40

[ = %5B

] = %5D

Reserved characters need to be converted to their equivalent

Once it's encoded, it will no longer have its special meaning in the URL

Then, when the page is processed by PHP, we can decode these values to restore the original character.

### Two functions to perform this encoding:

Both of these functions will allow letters, numbers, underscore and dash - but, any reserved characters are encoded.

1. urlencode($string)

  - spaces become '+'

  - what you want to use in the query string (after the '?')

2. rawurlencode($string)
 
  - spaces become '%20'

  - better to use in the path (before the '?')

  - rarely used - because the path isn't something that's usually dynamically generated

### Two functions to perform the decoding:

You're **rarely** going to need these.

PHP automatically calls the decode functions when it receives a URL.

1. urldecode($string)

2. rawurldecode($string)

### Write a function to make it easier

Encodings are used a lot - so, it may be better to write a function in your functions.php file:

```php
function u($string="") {
    return urlencode($string);
}
```

## Encode for HTML

Most of the time, we have complete control over what HTML goes on the page.

Once we start building pages using dynamic data, we're going to be inserting data that may come from the user, from the database, from cookies, or from other sources, and we must take care that when we do that, we don't lose control of the HTML at the same time.

```php
<?php $username = "Kevin"; ?>

<div id="name"><?php echo $username; ?></div>
```

Someone could enter a username with HTML tags (with scripts!)

### Reserved Characters in HTML

< = '\&lt;'

\> = '\&gt;'

& = '\&amp;'

" = '\&quot;'

**htmlspecialchars($string)**

### Cross-Site Scripting

XSS

Attacker tricks a web page into outputting JavaScript

Code is trusted by the browser and executed.

Anytime we're using dynamic data, we want to make sure that when we output the data, that we use htmlspecialchars() on it.

## Challenge: Add a Page

- link from /staff/index.php to /staff/pages/index.php

- add HTML and PHP for /staff/pages/index.php

  - loads initialize.php

  - includes correct header and footer

  - list all of the different pages as a table

    - use a $pages array to hold a list of pages

  - Use /staff/subjects as a reference - but, do not copy/paste

  - link each page to /staff/pages/show.php - include the page ID as a URL parameter

  - retrieve and display the page ID

  - use the file path techniques for including files

  - use the url techniques for creating links

  - encode dynamic data used for links and HTML

  - set $page_title for all pages

## Modify Headers

### Example of an HTTP Header

HTTP/1.1 200 OK
Date: Thu, 28 Feb 2017 03:21:16 GMT
Server: Apache/2.4.23
Content-Type: text/html; charset=utf-8
Content-Length: 52839
Connection: close

We can use PHP to give instructions to the web server on how to construct the header.

```php
header($string);
```

```php
header("Content-Type: application/pdf");
```

```php
header("HTTP/1.1 404 Not Found");
header("HTTP/1.1 500 Internal Server Error");
```

You can also use headers for more advanced techniques like sending attachments and setting page cache controls.

### Modify Headers

**Headers are sent before page data**

```php
function error_404() {
    header($_SERVER["SERVER_PROTOCOL"] . " 404 Not Found");
    exit();             // Could render 404 page instead
}
```

## Page Redirection

In http a webserver can tell a web browser that it ought to go to a new URL by something known as a "302 redirect."

A 303 redirect has two parts to it, both of which are in the header information.

1. Status Code 302 Found

2. Location attribute indicating the new URL that the browser ought to try instead

PHP is smart enough to know that if we're setting the location then we also want to set the status code to 302 at the same time.

```php
header("Location: login.php");
```

### Example

User submits username/password on a login page.

PHP code checks the user credentials.

If correct, the user is sent to a success page.

If incorrect, the user is sent to a failure page.

### Caveat: Page redirects are sent in headers

Headers are sent **before** page data. 

Header changes must be made before any HTML output.

Page redirects must be sent before any HTML output.

## Output Buffering

Data in output buffer is editable

Headers can be changed.

Whitespace can be sent before header edits and redirects.

### Two ways to turn on output buffering

1. In php.ini file

2. In PHP code

```php
ob_start()
ob_end_flush()
```

ob_start(_) has to come before any content, the same as headers.

Best to turn output buffering on in php.ini file.

Use ob_start() when code may be ported to other servers.

Don't necessarily have to call ob_end_flush() - when you get to the end of the PHP code - it will automatically be called for you.

## Build Forms

Basic HTML Form:

```html
<form action="register.php" method="post">
    <input type="text" name="city" value="" />
    <input type="text" name="state" value="" />
    <input type="text" name="zipcode" value="" />
    <input type="submit" value="Submit" />
</form>
```

### Using Form Parameters

PHP takes the form values and automatically assigns them to an associative array where we can access them.

That array is stored in a super global variable called $_POST

Unlike $_GET, we don't need to do any ecoding of the form parameters - no reserved characters to worry about.

```php
$name = $_POST['name'];

//  Good practice to make sure the value exists
$name = isset($_POST['name']) ? $_POST['name'] : '';

$name = $_POST['name'] ?? '';
```

### Detect Form Submission

Right now our create.php handles post requests and displays form submission data. But, a user could still make a get request to that url.

Perhaps we want to redirect to another page? To do that - we need to detect whether the form has been submitted or not.

#### Three main techniques:

1. Test if a key parameter has a value

```php
if (isset($_POST['username'])) {
    // process form
}
```

2. Test if submit parameter was sent

Adding name to submit input - so that it is included in the form data

```html
<input type="submit" name="submit" value="Submit" />
```

```php
if (isset($_POST['submit'])) {
    //  process form
}
```

3. Test if request method was a POST

```php
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    //  process form
}
```

### Single-Page Form Processing

Having a single page which contains the form and the code to process the form (form that submits to itself).

All logic related to the form is in one file.

Redisplaying the form on errors.

Return error messages.

Populate the fields with previous values.

## MySQL Introduction

Check if MySQL is installed:

```bash
$ mysql --version
```

login

```bash
$ mysql -u root -p 
```

### Four Main Commands

1. SHOW DATABASES;

2. CREATE DATABASE db_name;

3. USE db_name;

4. DROP DATABASE db_name;

### Users

Generally, login as user

We could log in from our web application as the root user as well - but, better to create a new MySQL user and assign it a password - and, then grant access to our database to that user.

```bash
GRANT ALL PRIVALEGES ON db_name.*
TO 'username'@'localhost'
IDENTIFIED BY 'password';
```

View access rights for a username:

```bash
SHOW GRANTS FOR 'username'@'localhost';
```

bankdev

### Create a Table

```bash
CREATE TABLE table_name (
    column_name1 definition,
    column_name2 definition,
    column_name3 definition,
    options
);
```

```bash
CREATE TABLE subjects (
    id INT(11) NOT NULL AUTO_INCREMENT,
    menu_name VARCHAR(255),
    position INT(3),
    visible TINYINT(1),
    PRIMARY KEY (id)
);
```

Show tables:

```bash
SHOW TABLES;
```

Show columns:

```bash
SHOW COLUMNS FROM table_name;
```

Drop table:

```bash
DROP TABLE table_name;
```

### CRUD in MySQL

#### Read

SELECT

```bash
SELECT *
FROM table
WHERE column1 = 'some_text'
ORDER BY column1 ASC;
```

#### Create

INSERT

```bash
INSERT INTO table (col1, col2, col3)
VALUES (val1, val2, val3);
```

#### Update

UPDATE

```bash
UPDATE table
SET col1 = 'this', col2 = 'that'
WHERE id = 1;
```

#### DELETE

DELETE

```bash
DELETE FROM table
WHERE id = 1;
```

LIMIT 1; - good practice, but not strictly necessary

```bash
DELETE FROM subjects WHERE id=4 LIMIT 1;
```

### Relation Database Tables

#### One-to-Many Relationships

ex. One subject and many pages

table: subjects
id: 3
menu_name: 'About Globe Bank'
position: 1
visible: TRUE

table: pages
id: 12
subject_id: 3                   // FOREIGN KEY
menu_name: 'History'
position: 2
visible: TRUE
content: 'Founded in 1950...'

The Foreign Key always belongs on the 'child' record - the one that 'belongs' to the other.

Foreign keys ought to have an index on them as well. You can create the index at the same time that you create the table - or you can add it afterwards with ALTER

```bash
ALTER TABLE table
ADD INDEX index_name (column);
```

Some peopl like to call it fk_name for foreign key.

