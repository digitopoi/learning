# Basics of Unit Testing for C# Developers

Paul D. Sherriff

## Why You Need Unit Testing

### What is Unit Testing?

- Write code to test other code

  - Write methods to test methods in your class

- We want to make sure to cover all code in the method.

  - Include all if, case, do, until while, for, etc.

  - To insure that all parts of the code have been "hit" and tested

- Tests can be run over and over again

  - Regression testing

  - After adding code, running tests over and over to make sure you didn't break anything that previously worked

### Why Test Code?

- Ensure that the code, under varying circumstances, with different inputs

- Improve code quality - helps you focus on quality, add more error handling, helps break the code into smaller chunks

### How we test our code

1. QA Department

- Advantanges: different people think of different things to test, test UI code easier, less start-up time/cost

- Disadvantages: regression testing is time intensive, setup and teardown time intensive, frustrating and boring

2. Automated Testing

- Advantages: repeatable, better code coverage, regression testing is faster, automate setup and teardown

- Disadvantages: More time up-front, only as good as the developer who writes the test, harder to test UI

#### Which approach?

Combination of both - QA should work with devs to create unit tests, help with setup/teardown, help with UI testing

### Testing Tools

- Unit test framework - set of classes and tools in most edition of Visual Studio

- Code coverage - determines if any methods have not been tested. (Enterprise Edition only)

- Data-driven testing - read data from table to guide testing

- VSTest.Console - CLI to automate tests, log results

## Your First Unit Tests

### Getting Started

1. Try to think of as many tests as possible for the method you're trying to create. 
2. Write method stubs for each test
3. Assert.Inconclusive() indicates no test has been written (think of it as // TODO: <>)
4. Create a test for each method
5. Create a test for each overload of a method
6. Write tests to cover all if, else, switch, and loop statements
7. Evaluate if you have too many test methods
8. If you have too many test methods - consider refactoring original method
9. Run the tests
10. See all the inconclusive tests
11. Write the tests themselves
12. Refactor original method as needed
13. Repeat until all test methods are working

### Create FileProcess.cs class

Test method stubs:
```c#

//  Test for a good filename
[TestMethod]
public void FileNameDoesExist() 
{
    Assert.Inconclusive();
}

//  Test for a bad file name
[TestMethod]
public void FileNameDoesNotExist()
{
    Assert.Inconclusive();
}

//  Test for no file name passed
[TestMethod]
public void FileNameNullOrEmpty_ThrowsArgumentNullException()
{
    Assert.Inconclusive();
}
```

### Use AAA

1. **Arrange**- initialize variables

2. **Act**- invoke the method you are testing

3. **Assert**- verfify the Act

```c#
[TestMethod]
public void FileNameDoesExist()
{
  //  ARRANGE
  FileProcess fp = new FileProcess();
  bool fromCall;

  //  ACT
  fromCall = fp.FileExists(@"GoodFile.txt");

  //  ASSERT
  Assert.IsTrue(fromCall);
}
```

### Exception Handling

When you have a method that's going to throw an exception, we have to make sure to handle that

1. [ExpectedException] - specify typeof() exception

2. Try... catch - good when using data-driven tests

### Code Coverage

Have you covered all code with test(s)?

- Is all code tested?

- Helps you determine what else you need to test.

Tool built into Visual Studio (Enterprise Edition only)

## Avoid Hard-Coding in Unit Tests

### Best Practices

1. Don't hard code 

2. Use constants whenever possible

3. Store data in a .config file
  - add app.config to unit test project
  - add <appSettings> element
  - set field from config file
  - Use field in test
  - write code to create file
  - write code to remove the file

  ```xml
  <appSettings>

    <add key="GoodFileName"
         value="[AppPath]\TestFile.txt"/>

  </appSettings>
  ```
  - [AppPath] is a replaceable token

4. Setup and tear down as close to each unit test as possible

5. Use comments

6. Use good variable and method names

## TextContext Property

Create property 'TextContext' in all your test cases

Automatically created by the unit test framework and set before each test is run

Useful for accessing test information and in data-driven tests

### Useful properties on TextContext

**DataConnection**

**DataRow**

**DeploymentDirectory**

**TestName**

### Useful methods on TextContext

**BeginTimer**

**EndTimer**

**WriteLine**

## Initialization and Cleanup

## Attributes Help You Organize Your Unit Tests

## Assert Classes Save a Lot of Time

## Consolidate Tests By Making Them Data-Driven

## Automating Unit Tests with VS.Test.Console