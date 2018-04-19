# Unit Testing for C# Developers

## Introduction

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