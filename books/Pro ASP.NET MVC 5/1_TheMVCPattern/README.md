# The MVC Pattern

## The History of MVC

MVC has been used since the 1970s - Xerox PARC - for organizing GUI applications

Still applicable to modern development - especially web development

Interactions with MVC application follow a natural cycle of user actions and view updates, where the view is assumed to be stateless.

Works nicely with HTTP requests and responses.

MVC forces a **separation of concerns**

  - the domain model and controller logic are decoupled from the user interface
  - (HTML kept apart from the rest of the application)

Ruby on Rails led to the resurgence in MVC for web applications

## Understanding the MVC Pattern

The MVC pattern means that the MVC Application will be split into three pieces:

### 1. Models
  
  - contain or represent the data that users work with

  - can be simple **view models** (represent data being passed between views and controllers)

  - can be **domain models** contain the data in a business domain as well as the operations, transformations, and rules for manipulating that data.

### 2. Views

  - used to render some part of the model as a user interface

  - they have no direct awareness of the model and do not communicate with the model

### 3. Controllers

  - process incoming requests

  - perform operations on the model

  - select views to render to the user

  - bridge between models and views

## Understanding the Domain Model

Most important part of the application

The single, authoritative definition of the business data and processes within the application

For ASP.NET, the domain model is a set of C# types (classes, structs, etc.) - **domain types**

**domain object** - when en an instance of a domain type is created to represent a specific piece of data

One option to ensure your domain model is separate is to put it in a separate C# assembly (create references **to** the model, but not in reverse)

## The ASP.NET Implementation of MVC

Controllers are classes derived from the System.Web.MVC.Controller class. 

Each public method derived from a controller is an **action method**

Action methods are associated with a configurable URL through the ASP.NET routing system.

When a request is sent to the URL associated with an action method, the statements in the controller class are executed in order to perform some operation on the domain model and then select a view to display to the client. 

![image](../images/1.png)

ASP.NET MVC Framework uses the Razor view engine to process a view in order to generate a response to the browser. 

## Building Loosely Coupled Components

One of the most important features of the MVC pattern is that it enables separation of concerns. 

**loose coupling**- each component knows nothing about any other component and only deals with other areas of the application through **abstract interfaces**

### Example:

A component called MyEmailSender to send emails

I would implement an interface that defines all of the public functions required to send the email (IEmailSender)

Any other component that needs to send an email by referring only to the methods in the interface. 

No direct dependency between the two components

![image](../images/2.png)

There is no direct dependency between PasswordResetHelper and MyEmailSender. 

MyEmailSender could be replaced with another email provider (or a mock implementation for testing) without making changes to PasswordResetHelper

## Using Dependency Injection

C# doesn't provide a built-in way to easily create objects that implement interfaces (except creating an instance of the concrete component with the *new* keyword):

```c#
public class PasswordResetHelper 
{
    public void ResetPassword()
    {
        IEmailSender mySender = new MyEmailSender(); // <====

        //  ... call interface methods to configure e-mail details ...

        mySender.SendEmail();
    }
}
```

This prevents PasswordHelper from being loosely coupled from MyEmailSender - it's dependent on this instance of MyEmailSender

PasswordResetHelper now depends on MyEmailSender **AND** IEmailSender

**Dependency Injection** provides a way to implement an interface *without* having to create the object directly. (also known as **inversion of control IoC)

### Two parts to the DI pattern:

#### 1. Remove any dependencies on concrete classes from my component

Create a class constructor that accepts implementations of the interfaces needed as arguments:

```c#
public class PasswordResetHelper 
{
    private IEmailSender emailSender;

    public PasswordResetHelper(IEmailSender emailSenderParam)   // <====
    {
        emailSender = _emailSenderParam;
    }
}
```

The constructor is now said to **declare a dependency** on the IEmailSender interface
  - it can't be created and used unless it receives an object that implements the IEmailSender interface.
  - PasswordResetHelper no longer has any knowledge of MyEmailSender, it only depends upon the IEmailSender interface.

#### 2. Injecting Dependencies

**Inject** the dependencies declared by the PasswordResetHalper class when instances of it are created.

Need to decide which class that implements IEmailSender interface I am going to use

Create an object of that class

Pass that object as an argument to the PasswordResetHelper constructor

The dependencies are injected at runtime

## Using a Dependency Injection Container

How do you instantiate the concrete implementation of interfaces without creating dependencies somewhere else in the application?

**dependency injection container** - component that acts as a broker between the dependencies that a class like PasswordResetHelper declares as the classes that can be used to resolve those dependencies, like MyEmailSender

I register the set of interfaces or abstract classes that my application uses with the DI container, and specify which implementation classes should be instantiated to satisfy dependencies.

Register the IEmailSender interfaces with the container

Specify that an instance of MyEmailSender should be created whenever an implementation of IEmailSender is required.

Whenever a PasswordResetHelper object is needed in the application, ask the DI container to create one.

**important** - these objects aren't created in the application with the new keyword any longer. Instead, you go to the DI container and request the objects you need

In this tutorial, we will use the Ninject DI container

Microsoft has its own DI container called Unity

## Features of DI containers

### 1. Dependency Chain Resolution

If you request a component that has its own dependencies, the container will satisfy those dependencies as well.

### 2. Object Lifecycle Management

Lets you configure the lifecycle of a component, allowing you to select from predefined options, including:
  1. **singleton** (the same instance each time)
  2. **transient** (a new instance each time)
  3. **instance-per-thread** 
  4. **instance-per-HTTP**
  5. etc.

## Getting Started with Automated Testing

ASP.NET MVC is designed to make it as easy as possible to set up automated tests and use **Test Driven Development** (TDD)

### Two primary kinds:
  1. **unit testing** - a way to specify and verify the behavior of individual classes (or other small units of code) in isolation from the rest of the application.
  
  2. **integration testing** - a way to specify and verify the behavior of multiple components working together, including the entire application

### Understanding Unit Testing

You create a separate test project in your Visual Studio solution to hold test fixtures

**test fixtures**- C# class that defines a set of test methods - one method for each behavior you want to verify.

EXAMPLE:
```c#
using System.Web.Mvc;

namespace TestingDemo
{
    public class AdminController : Controller
    {
        private IUserRepository repository;

        public AdminController(IUserRepository repo)
        {
            repository = repo;
        }

        public ActionResult ChangeLoginName(string oldName, string newName)
        {
            User user = respository.FetchByLoginName(oldName);
            user.LoginName = newName;
            respository.SubmitChanges();
            //  render some view to show the result
            return View();
        }
    }
}
```

```c#
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingDemo.Tests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void CanChangeLoginName() 
        {
            //  ARRANGE (set up a scenario)
            User user = new User() { LoginName = "Bob" };
            FakeRepository respositoryParam = new FakeRepository();
            repositoryParam.Add(user);
            AdminController target = new AdminController(repositoryParam);
            string oldLoginParam = user.LoginName;
            string newLoginParam = "Joe";

            //  ACT (attempt the operation)
            target.ChangeLoginName(oldLoginParam, newLoginParam);

            //  ASSERT (verify the result)
            Assert.AreEqual(newLoginParam, user.LoginName);
            Assert.IsTrue(repositoryParam.DidSubmitChanges);
        } 
    }

    class FakeRepository : IUserRepository
    {
        public List<User> Users = new List<User>();
        public bool DidSubmitChanges = false;

        public void Add(User user)
        {
            Users.Add(user);
        }

        public User FetchByLoginName(string loginName)
        {
            return Users.First(m => m.LoginName == loginName)
        }

        public void SubmitChanges()
        {
            DidSubmitChanges = true;
        }
    }
}
```

### Arrange, Act, Assert
  1. Arrange - setting up conditions for the test

  2. Act - performing the test

  3. Assert - verifying that the result was the one that was required

### Using TDD and the Red-Green-Refactor Workflow

  - Determine that you need to add a new feature or method to your application

  - Write the test that will validate the behavior of the new feature when it is written.

  - Run the test and get a red light.

  - Write the code that implements the new feature

  - Run the test again and correct the code until you get a green light

  - Refactor the code if required. For example, reorganize the statements, rename the variables, and so on.

  - Run the test to confirm that your changes have not changed the behavior of your additions.

### Understanding Integration Testing

The most common approach to integration testing for web applications is UI automation

**UI automation**- simulating a Web browser to exercise the application's entire technology stack by reproducing the actions that a user would perform (pressing buttons, following links, submitting forms, etc.)

#### Two best known browser automation options used by .NET developers:
  1. Selenium
  2. WatiN

