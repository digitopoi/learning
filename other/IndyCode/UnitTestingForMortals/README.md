# Unit Testing For Mere Mortals - Philip Japikse

**Note: Watch his Lynda - Entity Framework Core course**

[blog](www.skimedic.com)

[github](github.com/skimedic)

`As a developer you take pride in your work (as you should). So it's frustrating when bugs happen. How do you prevent them? Well, the honest answer, is you can't - at least not 100%. But you can make sure your code does what you expect it to do! In this session, I show you TDD (Test Driven Development), BDD (Behavior Driven Development), and TED (Test Eventual Development). I will also show you their strengths and weakness, and how to decide which type (or combination of types) to use in different scenarios.`

"Legacy code" - lack of tests, what no one wants to touch.

We're good at testing code the way it's supposed to be used - QA is good at testing code the way it's not supposed to be used.

We don't do unit testing for the reason management, etc. is interested - we do it for the team. Documentation, confidence (doing what my code thinks it should, your code is doing what  you you think it should do). 

Courage to make changes.

Thinking more focused.

When you start TDD - your production will go down.

Not about testing - about driving the API. Instead of increasing complexity - doing just what you want / need (developing to requirements).

### TDD/BDD Benefits

High code coverage - higher code coverage can be a bad / gamed statistic (test might not be effective).

Measurable impact of future change

### "Code of Honor"

Tests should be **independent** of other tests

Leave it as clean as you found it or maybe a little cleaner.

### Fakes, Stubs, and Mocks

Fakes have working implementations - hardcoded, not suitable for production

Stubs are better than fakes. You tell them what to provide as an answer. 

Mocks are preprogrammed with **expectations**.

### Behavior Driven Development

Testing behavior as well. Think lego periscope example (not testing in complete isolation - but, also the system overall).

Starting at higher level specifications - as you get more specific - switching to TDD.

Red, green, refactor.

### Source code

Check in every time tests go green.

### "Handling Squirrels"

Keep lists of To-Dos - as ideas come up, write them down - tackle them in order of confidence.

Finish what you start - don't context switch

### XUnit

Works well with .NET Core

Data driven tests - different use cases in your testing

```cs
[Theory]
[InlineData(5,3,2)]
[InlineData(3,1,6)]
[InlineData(5,3,-2)]
// etc.
public void //..
```

Avoid `ExpectedException` attribute (anywhere in the test, if an exception is thrown - the test will test)

`Assert.Throw` - how to test exceptions in code

### Using Disposable

Setup in the constructor

Dispose after test for teardown.

### Mocking - Moq

### Machine.Specification MSpec

