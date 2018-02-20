# ASP.NET Core MVC Testing Fundamentals

Jason Roberts

## Introduction

### The Benefits of Software Quality

Happier end users - accomplish tasks with fewer problems

Happier development team - less time spent fixing production errors (more time to develop new features, increased confidence making changes, increased system understanding, less unnecessary stress)

Happier business owners - reduced cost / increased revenue, increased business reputation, reduced team member turnover

### Why Automated Testing Over Manual Testing?

Less error prone

Free to run as often as required

Find problems sooner (quick)

Test code is source control - tests stay in sync

Automated testing usually doesn't replace all manual human testing

### Automated Testing in Context

Automated testing fits into a broader context of quality and sit alongside other techniques:

- Code Reviews

- Pair programming

- Good management

- Motivated development team

- Well-understood requirements

- Good environment management / DevOps

### Creating a Balanced Test Portfolio

**test portfolio** - mix of different types of tests and the different parts of the system that are being tested.

Categories:

1. Functional UI - operate the user interface of the system as if a real end user was using it

2. Subcutaneous - operate just under the user interface (ex. APIs that our frontend calls) - not testing UI logic

3. Integration - a number of classes working together and also perhaps working with a real database or file system

4. Unit Tests - test a class in isolation

A well-balanced test portfolio verifies as much of the system as is required, with as little cost as possible (both creation and maintenance costs) while providing feedback as quickly as possible.

### The Testing Pyramid

Different types of tests in a hierarchy

                                UI
                            SUBCUTANEOUS
                        INTEGRATION TESTING
                    --------UNIT TESTING -------

Implies that we have more unit tests than integration tests, more integration tests than subcutaneous tests, and the smallest number of UI tests

The closer we get to the top of the pyramid, the slower our tests become - more complex to set up, more brittle to changes in the system, and the more broad they are in terms of the areas of the system that they exercise.

What we can test in ASP.NET Core MVC:

- View Rendering UU

- Controller UUUII

- Model UUUUUUI

- HTTP API SSSS

- HTML UI Ui Ui

- Infrastructure UIII

Different parts may have a different looking pyramid

Have to access value and risk in the individual application - where is it most complicated and prone to breaking? If you create tests that don't add value - you're adding a cost to create and maintain them

## Testing Model Classes with xUnit.NET

### Overview of xUnit.net

[xUnit.net is a free,open source, community-focused unit testing tool for the .NET Framework. Written by the original inventor of NUnit v2, xUnit.net is the latest techonology for unit testing in C#, F#, VB.NET and other .NET languages](https://xunit.github.io)

**testing framework** - provides all features for us to be able to write automated tests without having to worry too much about the plumbing.

**test runner** - looks through our test code, it understands the testing framework that we're using and once it's executed our tests, provides the outcome - passed or failed.

Features:

- Attributes
  - [Fact]
  - [Theory]
  - [InlineData]

- Assertion Methods - to check whether the result of the code is as expected
  - Assert.Equal()
  - Assert.False()
  - Assert.True()
  - Assert.Throws()

- Test Execution Lifecycle Management
  - Test setup
  - Test cleanup
  - Shared test context/data/instances

#### xUnit.net Test Runners

Visual Studio Test Explorer

Command line runner

3rd Party runners (eg ReSharper)

Continuous Integration (eg TeamCity)

#### Example Test

```c#
[Theory]
[InlineData(20)]
[InlineData(19)]
public void ReferYoungApplicantsWhoAreNotHighIncome(int age)
{
    var sut = new CreditCardApplicationEvaluator();

    var application = new CreditCardApplication
    {
        GrossAnnualIncome = ExpectedIncomeThreshold - 1,
        Age = age
    };

    Assert.Equal(CreditCardApplicationDecision.ReferredToHuman, sut.Evaluate(application));
}
```