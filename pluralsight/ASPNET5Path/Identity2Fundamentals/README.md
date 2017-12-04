# ASP.NET Identity 2.0 Fundamentals

Scott Brady
[link](https://app.pluralsight.com/library/courses/asp-dot-net-identity-fundamentals/table-of-contents)

## Claims Based Identity

A user within a system has two different kinds of data:

1. Identity data

  - specific to the user

  - who the user is and what they are (name, email address, DOB, etc.)

  - An identity does not necessarily need to be issued by your own application (ex. using Google or Facebook)

2. Application data

  - specific to an application

  - user permissions within that application, usage data, save documents, etc.

### What is part of a user's Identity?

Identity can be:

  - Data a user uses to authenticate (ex. username and password)

  - Personal information (name, DOB, phone number, etc.)

  - What the user is (information like role within the organization)

### Claims based Identity

**Claims**- how we represent identity data within our application

When we refer to a claim, we are talking about a single piece of a user's identity

Claims are issued by an Identity provider - your own application or an external provider like Google

### Layers

1. Identity Management - where the identity data is stored and accessed (including functionality such as registration and password verification)

2. Authentication - verifying a user, or verifying who they are (logging into a system using a username and password)

3. Authorization - verifying what a user is allowed to do. Typically requires the request to have first been authenticated so that it can be look up the user's claims and then look up permissions

## What is ASP.NET Identity

- Identity of your users - and the storage and management of those identities

- Implemented in your identity management layer and is otherwise known as an identity store or user store.

- **Identity store**- different from your actual database in that it is only concerned with identity data - no application data allowed.
  
  - Allows it to serve Identities to multiple applications or to a central authentication service

### ASP.NET Identity Components

1. Password Hashing - secure storage of passwords

2. User Lockout - provides protection against brute force attacks

3. Two Factor Authentication - allows us to add extra steps to authentication beyond username + password

4. External logins - associate locally stored identity information with that sent by an external identity provide like Google or Facebook

5. Tokenization - create and verify tokens used to reset or confirm identity information

6. Highly extensible - allows for any of these components to be switched out for your own implementation without breaking any other functionality

### Why ASP.NET Identity?

- Common functionality between almost all applications with users and a login form. Why implement it yourself over and over again?

- Not everyone's domain - high risks involved in getting it wrong

### What ASP.NET Identity is not

- It's called ASP.NET Identity - NOT Identity 2 or Identity Framework (makes it easier to search for solutions)

- It's NOT Identity Server - Identity Server is an open sourced example of an identity provider - authentication, not identity management

- Identity is NOT a general purpose database. Should be small and focused, not bloated with application data.

- It's NOT a security framework.

### Demo: Creating a User

- The connection string used here needs to be called DefaultConnection - the default Entity Framework package for ASP.NET Identity looks for this connection string by default.

- The main point of entry when using ASP.NET Identity is the UserManager class - contains all of the create, read, update, and delete methods you need for managing your identity data (methods for managing passwords, claims, and roles for a user).

- All of the implementations within ASP.NET Identity are generic, allowing for different representations of a user to be used

- In this case, we'll be using the default user entity that comes with the Entity Framework

- A UserManager has one parameter in its constructor - a user store (abstraction of the underlying storage layer, your database) 

- In this example, we will be using the existing Entity Framework implementation that we have already configured to use SQL Server (in connection string)

```c#
var userStore = new UserStore<IdentityUser>()

var userManager = new UserManager<IdentityUser>()

//  After configuring a UserManager, we can use the Create() method
//  In this example, we'll use synchronous version, afterwards: async
var creationResult = userManager.Create(new IdentityUser("scott@scottbrady91.com"), "Password123!");
```

### Demo: Add a claim to this user

- To add a claim, we simply use the AddClaim method - accepts the userId and the claim (System.Security.Claim class) as parameters

```c#
//  get userID
var user = userManager.FindByName(username);
var claimResult = userManager.AddClaim(user.Id, new Claim("given_name", "Scott"))
```

### Verifying a password

Takes user object and password to verify - hashes the password value entered and compares it to what is stored in the database

```c#
var isMatch = userManager.CheckPassword(user, password);
```

## Implementing ASP.NET Identity

### Architecture

- ASP.NET Identity has two main components;

1. Manager - how your application will interact with ASP.NET Identity

2. Store - how ASP.NET Identity will interact with your database

- Throughout all of this is a single entity - used to represent the identity being stored

- The entity must implement an interface containing some required properties - can be used in every layer of your application

- A role is just another claim

## Entity Framework Defaults 

Using Microsoft.AspNet.Identity.EntityFramework gives you:

1. IUserStore

2. IUSer

3. DbContext