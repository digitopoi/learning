# Raising the Value of Your Unit Tests - Richard Taylor

`For any non-trivial application created today, software engineers must not only deliver on application features but also include associated unit tests to improve efficiency, to reduce risk when making code changes, and to improve quality by identifying bugs as early as possible. One may think having unit tests to increase code coverage is the goal but that is missing the mark. Software engineers should be focused on delivering unit tests that have meaningful value to support current and future software development. In this session, you will learn several unit test styles, how identify weaknesses in unit tests, and how increase the value of the unit tests you create.`

[Github](github.com/rightincode/Xamarin-Forms-ToDo)

Unit testing - how to write testable code and why it matters - toptal.com

Pluralsight - Building a pragmatic unit test suite, writing highly maintainable unit tests, advanced unit tests

### Styles of Unit Tests

Output Verification - highest value

State Verification

Collaborative Verification

### What can cause unit tests to lose value?

Test names that do not effectively describe the test

Complicated unit test code

Testing more than a single unit of work

Brittle to system under test (SUT) changes

Difficult to maintain

Unreliable

### Hot to make your unit tests more effective?

Clear, simple, and readable

High value - critical business processes, rules, calculations, etc.

#### Make Test Names Consistent and Meaningful

  - Utilize a naming convention

  - ie, three point naming convention `UnitOfWork_InitialCondition_ExpectedResult` (easy to scan/search)

  - group tests together for the same Unit of Work

  - Provides some insight into the business rules

#### Test suite should be organized

  - DRY

  - DAMP - descriptive and meaningful phrases

  - Follow distinct pattern in your test (Setup/Arrange, Action, Assert)

#### Focus on high precision
 
  - Test one expectation per test

  - Multiple assertions on a object is okay - but, be careful. (if you have asserts on multiple objects - maybe think about reworking test or your code)

  - Test should point to a precise location of a problem

#### High Value

  Focus on testable code

  - Use dependency injection to provide dependencies

  - Avoid using 'new' - it creates dependencies

  - Avoid global state

  - Be careful with static methods

  Use Seams

  - 'New' the dependency but provide the ability to override it and use that ability to unit test

  Favor composition over inheritance - providing dependencies at runtime

  Apply SOLID principles

  Test code that has the highest value (complex workflows, calculations, minimal 'what if' scenarios)

  Cover **all** the business rules - isolate business rules and test them effectively

  Cover happy paths and not happy paths

  Avoid testing things that the compiler would catch (types etc.) and text

#### Flexible

  - Maximum one mock per test

  - Fewer than 10% of your tests with mocks

  - Test by scenario rather than method

  - Do not test private methods - should be covered by testing other public methods

