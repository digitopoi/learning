# ASP.NET Identity 21. with ASP.NET Web API 2.2

[Taiseer Joudeh](http://bitoftech.net/2015/01/21/asp-net-identity-2-with-asp-net-web-api-2-accounts-management/)

## Accounts Management

### NuGet Packages

```bash
Install-Package Microsoft.AspNet.Identity.Owin -Version 2.1.0
Install-Package Microsoft.AspNet.Identity.EntityFramework -Version 2.1.0
Install-Package Microsoft.Owin.Host.SystemWeb -Version 3.0.0
Install-Package Microsoft.AspNet.WebApi.Owin -Version 5.2.2
Install-Package Microsoft.Owin.Security.OAuth -Version 3.0.0
Install-Package Microsoft.Owin.Cors -Version 3.0.0
```

### Add ApplicationUser class and DbContext

**ApplicationUser** - represents a user registered in our membership system - we'll extend with additional application-specific data (FirstName, LastName, Level, JoinDate). Extended properties will be converted into columns in AspNetUsers table. Derived from Microsoft.AspNet.EntityFramework.IdentityUser

**ApplicationDbContext** - responsible for communicating with our database. Inherits from IdentityDbContext. Static Create() method will be called in Owin Startup class

### Create Database and Enable DB migrations

**Configuration file** - contains a Seed() method - allows us to insert or update/test initial data after code first creates or updates the database. Called when the database is created for the first time and everytime the database schema is updated.

### Add User Manager Class

Will be responsible for managing instances of the user class.

Derives from UserManager<T> -- T will represent our ApplicationUser class.

A set of methods will be available when derived from ApplicationUser which will facilitate managing users in our Identity system.

Examples:
  - FindByIdAsync(id) - find user object based on its unique identifier
  - Users - Returns an enumeration of the users
  - FindByNameAsync(Username) - Find a user by Username
  - CreateAsync(User, Password) - Create a new user with specified password
  - DeleteAsync(User) - Delete user
  - IsInRole(UserName, RoleName) - Check if a user belongs to a certain role
  - AddToRoleAsync(Username, RoleName) - Assign user to a specific role
  - RemoveFromRoleAsync(Username, RoleName) - Remove user from specific Role

### Add Owin "Startup" Class

The Owin Startup class is fired when the server starts. 

The Configuration() method accepts parameters of type IAppBuilder - supplied by the host at run-time.

This "app" parameter" is an interface which will be used to compose the application for our Owin server. 

- In the Startup class, we configure the server to create a fresh instance of "ApplicationDbContext" and "ApplicationUserManager" for each request and set it to the Owin context using the extension method "CreatePerOwinContext."

## Define Web API Controllers and Methods

### Create the "Accounts" Controller

**AccountsController** - responsible for managing user accounts in our Identity system

### Create the "BaseApiController" Controller

This will act as the base class which other Web API controllers will inherit from

### Create the "ModelFactory" Class

This class will contain all of the functions needed to shape the response object and control the object graph returned to the client (ex. PasswordHash not returned).

### Add Method to Create Users in AccountsController

First create a new binding model which contains the user data which will be sent from the client. Contains data annotation attributes which will validate the model before submitting it to the database. 

Notice that a property is added to this model called "RoleName"

Add a CreateUser(CreateUserBindingModel createUserModel) in the AccountsController

  - Validate the request model based on data annotation in the binding model

  - If the model is valid, we will create a new instance of "ApplicationUser"

  - Then the CreateAsync() method from the AppUserManager class is called

    - validates the username, email, and password

    - if the request is valid, it will create a new user and add it to the AspNetUsers table and return success

    - Good practice to return the resource after it is created in the location header and return 201 created