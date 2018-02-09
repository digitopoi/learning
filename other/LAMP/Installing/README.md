# Installing Apache, MySQL, and PHP

## What is an AMP Stack?

Web applications are built with multiple tiers.

### Four major tiers in a web application:

These tiers don't know about each other.

In a production environment, the database server is frequently installed on a separate server than the system that hosts Apache and PHP. 

When developing locally, all of these components are typically installed on your own machine.

1. Client (web browser)

- renders HTML, images, and CSS

- executes client-side code (JavaScript)

2. Web (HTTP server)

- receives requests from the client and returns responses 

- Apache, IIS, etc.

3. Business (Application server)

- PHP, ASP.NET, Node.js, Java EE, etc.

4. Data (Database server)

- Oracle, SQL Server, MySQL, etc.

### The AMP Stack

Apache (web tier)

PHP (Business tier)

MySQL (Data tier)

### Handling a Web Request

- request is made by the browser

- request is received by Apache

- HTTP server dispatches the request to PHP

- If database is needed, PHP sends request to MySQL

- MySQL returns data to PHP

- PHP forms an HTML response and sends it to Apache

- Apache sends HTML response back to the browser

### Commands

**Start Apache:** $ sudo apachectl start
**Stop Apache:** $ sudo apachectl stop
**Restart Apache:** $ sudo apachectl restart

**Install PHP:** $ sudo apt-get install php libapache2-mod-php php-mcrypt php-mysql

**Install MySQL:** $ sudo apt-get install mysql-server
**Install MySQL Workbench:** $ sudo apt-get install mysql-workbench


## Where to Go From Here?

### Learning PHP

PHP with MySQL Essential Training: 1 The Basics
PHP with MySQL Essential Training: 2 Build a CMS

Learning PHP (11/20/2015)

Ajax with PHP

PHP: Advanced Topics

### Linux

Linux for PHP Developers

### Learning MySQL

Up and Running with MySQL Development

MySQL Essential Training

Advanced Topics in MySQL and MariaDB

### Learning Apache

Practical Apache Web Server Administration