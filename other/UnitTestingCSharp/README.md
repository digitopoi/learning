# Unit Testing for C# Developers

## INTRODUCTION

### What is Automated Testing

**automated testing**: the practice of writing code to test our code, and then running those tests in an automated fashion.

Example:

```cs
public float CalculateTax(int input)
{
    if (x) return ...;
    if (y) return ...;
    return ...;
}
```

If you want to test manually - you may have to launch the application in the browser, login, navigate, fill out a form, submit it, verify the result on the screen. And, then - you have to repeat these steps each time using different values in the form.

You also won't just have this one method - you'll have tens or hundreds of similar methods. As the application grows in complexity, the time needed to manually test increases exponentially.

With automated testing, you write code and directly call the function with different inputs and verify that the function returns the appropriate output. Automated tests are repeatable - whenever you make a change elsewhere in the application, you can verify it doesn't break what should work.

### Benefits of Automated Testing

Test code frequently, in less time.

Catch bugs **before** deploying.

Deploy with confidence.

Helps you to focus more on the quality of the methods you're writing.

Refactor with confidence (changing the structure of the code without changing its behavior).

Tests don't mean bug-free software - but, you can reduce the number of bugs and improve the quality.

### Types of Tests

1. Unit Tests - tests a single unit of an application **without** its external dependencies. Cheap to write, execute fast. Don't give a lot of confidence.

2. Integration Tests - tests the application **with** its external dependencies. Take longer to execute. Give you more confidence.

3. End-to-end Tests - drives an application through its UI. Give you the greatest confidence. But, very slow and brittle.

### Testing Pyramid

![TestingPyramid](https://cdn-images-1.medium.com/max/960/1*6M7_pT_2HJR-o-AXgkHU0g.jpeg

Only test happy path in E2E tests - leave edge cases for unit tests.

The pyramid is just a generality - the actual ratio between types of tests depends on your project.

Unit tests are great for quickly testing the logic like conditional statements and loops.

If you have an application without complex logic that simply reads/writes data to a database - you may rely more on integration tests.

### The Tooling

NUnit, MSTest, xUnit.

All of these frameworks give you a utility library to write your tests and a test runner which runs your tests and gives you a report of passing and failing tests.

Which framework is better? It depends who you ask and what their definition of better is.

Don't get hung up on the tooling - focus on the fundamentals.

This course starts with MSTest (built into Visual Studio). Then it will look at NUnit (and will be the majority of the course).

Resharper has a faster and more powerful test runner than the one built into Visual Studio.

### Writing Your First Unit Tests

Separate Unit Test and Integration Test projects. Run unit tests frequently and integration tests before committing to a repository.

**Naming Convention**: `SystemUnderTest_Scenario_ExpectedBehavior()`

We want to test **all scenarios / execution paths**:

Example:

```cs
public bool CanBeCancelled(User user)
{
    if (user.IsAdmin)
        return true

    if (MadeBay == user)
        return true;

    return false;
}
```

Three scenarios above:

1. When the user is an admin

2. When the user is the same as the one who made the reservation

3. When someone else tries to cancel the reservation

Three parts to a unit test:

1. Arrange - where we initialize our objects.

2. Act - where we act on the object - usually means we'll call the method we're going to test.

3. Assert - verify that the act step behaves correctly

Tests for the three scenarios in `CanBeCancelledBy()`:

```cs
public void CanBeCancelledBy_AdminCancels_ReturnsTrue()
{
    var reservation = new Reservation();

    var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

    Assert.IsTrue(result);
}

[TestMethod]
public void CanBeCancelledBy_UserOwnerCancels_ReturnsTrue()
{
    var user = new User();
    var reservation = new Reservation { MadeBy = user };

    var result = reservation.CanBeCancelledBy(user);

    Assert.IsTrue(result);
}

[TestMethod]
public void CanBeCancelledBy_OtherUserCancels_ReturnsFalse()
{
    var reservation = new Reservation { MadeBy = new User() };

    var result = reservation.CanBeCancelledBy(new User { IsAdmin = false });

    Assert.IsFalse(result);
}
```

Tests also provide **documentation** - you should be able to look at the tests for a given method and see what it should do.

### Refactoring With Confidence

With unit tests, we can now refactor the method with confidence:

```cs
public bool CanBeCancelledBy(User user)
{
    return (user.IsAdmin || MadeBy == user);
}
```

### Using NUnit In Visual Studio

Install NuGet packages:

```cmd
Install-Package NUnit
Install-Package NUnit3TestAdapter
```

The Test Adapter is to run NUnit tests inside Visual Studio.

```cs
[TestClass]                         //  MSTest
[TestFixture]                       //  NUnit

[TestMethod]                        //  MSTest
[Test]                              //  NUnit

Assert.IsTrue(result)               //  MSTest, NUnit
Assert.That(result, Is.True);       //  NUnit alternative (more readable)
Assert.That(result == true);        //  NUnit alternative
```

### Test Driven Development (TDD)

With TDD you write your tests before writing the production code

1. Write a failing test - fails because you don't have any application code to make it pass

2. Write the simplest code to make the test pass. (You don't want to over-engineer here)

3. Refactor the code if necessary

Repeat these steps until you build a complete feature.

#### Benefits of TDD

- Ensures your source code is testable immediately

- Full coverage by tests - refactor and deploy with confidence

- Simpler implementation, less chance of over-engineering or making the solution more complex than necessary

Sometimes, in practice, TDD may get really complex and slow you down.

In this course, our focus will be on code first so we can master the fundamentals.

## FUNDAMENTALS OF UNIT TESTING

### Characteristics of Good Unit Tests

#### Trustworthy

Covered in detail later in the tutorial. A test we can rely upon.

#### Unit tests are first-class citizens

They're as important as the production code.

#### All of the best practices apply to unit tests (clean, readable, and maintainable).

Each test should have a single responsibility and, ideally, be less than 10 lines of code.

One reason people fail with unit tests is because their tests are messy - large, fat test methods that are unmaintainable.

Refactor unit tests as necessary.

#### No logic

No conditional statements, loops, foreach, etc.

If you write logic in your tests - it's possible you will make a mistake - and you will think the mistake is in the source code.

#### Isolated

Each test should be written as if it's the only test in the project.

Test methods should not call each other and they should not assume any state created by another test.

#### Not Too Specific or General

If they are too general - they may not give you much confidence your source code is working.

### What To Test

Testable code is clean - clean code is testable.

Test the **outcome** of a method.

We generally have two types of methods:

1. Queries - return some value

2. Commands - performs an action - often changes the state of an object in memory, writing to a database, calling a web service, sending a message, etc. May return a value as well.

For a query method - your unit test should verify that the method is returning the right value. You should test all of the execution paths.

For a command method - if chaning state of an object - you should check if the given object is now in the right state. If talking to a resource like a database or web service - you should verify that the class under test is making the right call to the external dependencies.

### What Not To Test

#### Don't test language features

If you have a class that is mainly a property bag - no need to test the properties.

#### Don't test 3rd-party code

You should assume they're properly tested and you should only test your code.

### Organizing Tests

Have a separate project to store the unit tests. Separate unit tests and integration tests.

ex. `TestNinja` --> `TestNinja.UnitTests`

Inside of the unit test project - you will often have a separate test class for each class in your source code.

ex. `Reservation` --> `ReservationTests`

For each method in the class you're testing, you should have one or more test methods.

#### How many tests do you need?

It depends on what you're testing.

Generally: `Number of Tests >= Number of Execution Paths`

### Naming Tests

The name of your tests should clearly specify the business rule you're testing.

`[MethodName]_[Scenario]_[ExpectedBehavior]`

If your method has many tests - you can extract it into a separate test class.

`CanBeCancelledBy()` -> `Reservation_CanBeCancelledByTests

### Writing a Simple Unit Test

Method:

```cs
public int Add(int a, int b)
{
    return a + b;
}
```

Only one execution path that needs to be tested. Because there is only one execution path - only one unit test and we can name the scenario generically (`WhenCalled`):

```cs
[Test]
public void Add_WhenCalled_ReturnTheSumOfArguments()
{
    var math = new Math();

    var result = math.Add(1, 2);

    Assert.That(result, Is.EqualTo(3));
}
```

### Black Box Testing

Method:

```cs
public int Max(int a, int b)
{
    return (a > b) ? a : b;
}
```

There are two execution paths here - dependent upon `(a > b)`.

We started by thinking about the existing implementation of the method - good approach to get started - but, it's not enough.

When you write your tests based on the existing implementation, it is possible that the existing implementation has problems.

The best way to write tests is to think of them as a black box. Let's imagine that we don't know what is inside the `Max()` method.

There is a third execution path that is not considered by the implementation - `a` and `b` are equal.

Write out tests without implementation to make sure you don't forget what you need to implement:

```cs
[Test]
public void Max_FirstArgIsGreater_ReturnTheFirstArgument()
{
}

[Test]
public void Max_SecondArgIsGreater_ReturnTheSecondArgument()
{
}

[Test]
public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
{
}
```

Implement the tests:

```cs
[Test]
public void Max_FirstArgIsGreater_ReturnTheFirstArgument()
{
    var math = new Math();

    var result = math.Max(2, 1);

    Assert.That(result, Is.EqualTo(2));
}

[Test]
public void Max_SecondArgIsGreater_ReturnTheSecondArgument()
{
    var math = new Math();

    var result = math.Max(1, 2);

    Assert.That(result, Is.EqualTo(2));
}

[Test]
public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
{
    var math = new Math();

    var result = math.Max(1, 1);

    Assert.That(result, Is.EqualTo(1));
}
```

### Set Up and Tear Down

In our above tests - each test is in isolation - each method is relying on a new instance of the `Math` class. This is extremely important.

In each test, you want to start with a fresh, clean state.

You want to ensure another test method doesn't change the state and leak into another test.

However, it is a bit redundant code-wise. In this case, it's not a big deal because our tests are small and it's only a single line of code.

In `NUnit` - there are two special attributes: `SetUp` and `TearDown`.

We can create a method and decorate it with the `SetUp` attribute - the NUnit test runner will call that method before running each test.

NUnit will call the method decorated with the `TearDown` attribute after the test executes. We won't use the `TearDown` attribute - because, this is often used with integration tests (you may create some data in your database, for example).

```cs
private Math _math;

[SetUp]
public void SetUp()
{
    _math = new Math();
}
```

And, refactor the tests:

```cs
[Test]
public void Add_WhenCalled_ReturnTheSumOfArguments()
{
    var result = _math.Add(1, 2);

    Assert.That(result, Is.EqualTo(3));
}

//  etc.
```

### Parameterized Methods

Our tests are still pretty similar - the main difference is the values we're using.

In NUnit, we have a concept called parameterized tests. Instead of separate tests with different values - we can have one method that takes parameters - and supply different arguments to that test.

Combining 3 tests to one test. Has generic scenario and values are supplied as parameters with `TestCase`s.

```cs
[Test]
[TestCase(2, 1, 2)]
[TestCase(1, 2, 2)]
[TestCase(1, 1, 1)]
public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
{
    var result = _math.Max(a, b);

    Assert.That(result, Is.EqualTo(expectedResult));
}
```

### Ignoring Tests

In the real world, as you modify your code, it is possible you may break one or more tests. When you're new to unit testing, you may think "these tests are getting in the way, slowing me down, we should delete them."

This is a bad approach - because, there was a reason for you to write those tests - so we can deploy and refactor the code with more confidence.

But, this will show you how to temporarily disable a test so you can focus on some work somewhere else and then get to fix that test in the future.

In NUnit we have an attribute called `Ignore` - takes an argument that is the reason we want to ignore the test.

### Writing Trustworthy Tests

One characteristic of good unit tests.

A *trustworthy test** is a test that we can rely upon - if the test passes, you know your code is working and if it fails, you know there is something wrong with the source code.

#### How can we write trustworthy tests?

1. Test-driven development

2. Playing with what the method you're testing returns. After your tests pass - go into the source code and make a small change that is supposed to make the test pass and verify that your test now fails.

## CORE UNIT TESTING TECHNIQUES

### Testing Strings

Method:

```cs
public string FormatAsBold(string content)
{
    return $"<strong>{content}</strong>";
}
```

Test stub:

```cs
[Test]
public void FormatAsBold_WhenCalled_Should_EncloseStringWithStrongElement()
{
    var formatter = new HtmlFormatter();

    var result = formatter.FormatAsBold("abc");

    //  Different ways to Assert
}
```

Assertions:

```cs
//  Specific - verifying the exact string we expect
Assert.That(result, Is.EqualTo("<strong>abc</strong>"));

//  Too general
Assert.That(result, Does.StartWith("<strong>"));

//  General, but covered:
Assert.That(result, Does.StartWith("<strong>"));
Assert.That(result, Does.EndWith("</strong>"));
Assert.That(result, Does.Contain("abc"));
```

In this case, the specific solution is the best because what comes out of the method is important. We want to make sure the string is properly formatted.

But, sometimes your message might return an error message - you don't want to write specific assertions because you might modify it in the future.

With strings, often you want your tests to be a little more general because they can break easily if they are too specific.

By default, these assertions with strings are case sensitive. If you want to disable that, you can use the `IgnoreCase` property.

```cs
Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);
```

### Testing Arrays and Collections

Method:

```cs
public IEnumerable<int> GetOddNumbers(int limit)
{
    for (var i = 0; i <= limit; i++)
        if (i % 2 != 0)
            yield return i;
}
```

Test (of one scenario):

```cs
[Test]
public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
{
    var result = _math.GetOddNumbers(5);

    //  Assertions
}
```

Assertions:

```cs
//  Most General
Assert.That(result, Is.Not.Empty);

//  More Specific
Assert.That(result.Count(), Is.EqualTo(3));

//  More Specific
Assert.That(result, Does.Contain(3));

//  Specific
Assert.That(result, DoesContain(1));
Assert.That(result, DoesContain(3));
Assert.That(result, DoesContain(5));

//  Cleaner way to write the above specific Assert
Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));
```

Could also test for order - depending on the requirements.

### Testing the Return Types of Methods

### Testing Void Methods

### Testing Methods that Throw Exceptions

### Testing Methods that Raise an Event

### Testing Private Methods

### Code Coverage

### Testing in the Real-World