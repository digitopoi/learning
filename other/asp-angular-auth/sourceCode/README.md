# 
[link](https://www.pointblankdevelopment.com.au/blog/113/aspnet-core-angular-24-user-registration-and-login-tutorial-example)

## ASP.NET Core Users Controller

The users controller defines and handles all routes / endpoints for the API that relate to users. 
  
  - authentication

  - registration

  - CRUD

The controller actions are secured with JWT using the [Authorize] attribute - with the exception of the Authenticate and Register methods. 

On successful authentication the Authenticate method generates a JWT (JSON Web Token) using the JwtSecurityHandler class.

The JWT token is returned to the Angular client which then includes it in the HTTP Authorization header of subsecquent web api requests for authentication.

## DTO (Data Transfer Object)

The user DTO is a data transffer object used to send selected user data to and frm the user's api end points.

It doesn't include the PasswordHash and PasswordSalt fields of the user entity class so these fields aren't included in responses from the web api when the controller maps data from entities to user dtos.

The password property is in the DTO is only used for model binding data coming into the controller from http requests. Passwords are never included in responses from the Web API. 

## User Entity

The user entity class represents data stored in the database for users. It's used by Entity Framework Core to map relational data from the database into .NET objects for data management and CRUD. 

## AutoMapper profile

contains the mapping configuration used by the application, it enables mapping of new entities to dtos and dtos to entitites. 

## Data Context

The data context class is used for accessing application data through Entity Framework Core. 

It derives from the EFCore DbContext class and has a public Users property for accessing and managing user data.

The data context is used by services for handling all low level data operations.

## ASP.NET Core User Service

The ASP.NET Core user service is responsible for all database interaction and core business logic related to user authentication, registration and management.

```c#
User Authenticate(string username, string password);
IEnumerable<User> GetAll();
User GetById(int id);
User Create(User user, string password);
void Update(User user, string password = null);
void Delete(int id);
```

private helper methods for hashing
```c#
private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
{
    if (password == null) throw new ArgumentNullException("password");
    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

    using (var hmac = new System.Security.Cryptography.HMACSHA512())
    {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}

private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
{
    if (password == null) throw new ArgumentNullException("password");
    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
    if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
    if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

    using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
    {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != storedHash[i]) return false;
        }
    }

    return true;
}
```