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

