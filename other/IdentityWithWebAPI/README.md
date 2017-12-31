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