# Demystifying Dependency Injection - Brent Stewart

`What is Dependency Injection (DI) and why should you care? Come learn what DI is and how you can use it to keep your apps loosely coupled and test friendly. We will examine why you should avoid tight coupling and how you can avoid it using different methods of DI. After looking at the basic patterns, we'll take a look at a few different DI containers and see how they do their magic to make us better programmers.`

[Github](github.com/brentestewart)

## What is dependency injection?

separation of dependencies and behavior - loosely coupled - to follow dependency inversion and single responsibility

dependencies should be separate from what we're trying to do

### SOLID

- single responsibility principle (classes should be in charge of one thing and do one thing)

- open for extension, closed for modification

- liskov substitution principle - any derived class should be able to substituted for its parent class (if you implement an interface - you have to implement the whole interface - ie. no 'not implemented exceptions')

- interface segregation principle - lots of small interfaces instead of large interfaces

- dependency inversion principle - your higher level objects should not depend on lower level objects - should program to interfaces instead of concrete classes

### Why do we want loosely coupled code?

- code maintenance

- easier to write tests

- flexible configuration - config in a single place

- teams can work in parallel - if we agree to a contract/interface we can code against it separately before it is implemented

### Consequences of using DI

- more difficult to trace bugs (added complexity)

- takes extra effort (at least at first)

- explosion of types - more related to programming against interfaces

- can create a dependency on a DI framework (wrap the container so you aren't tied to it)

### Types of Dependency Injection

1. Constructor Injection - 99% of the time - "passing in parameters"

2. Property injection

3. method injection

4. Service locator - controversial technique? - asking object to resolve the interface/dependency for you

Is the Service locator an anti-pattern? Breaking encapsulation?

## What is a DI / IOC Container?

understanding our configuration and creating objects / resolving dependencies

Ninject calls its container a kernel

Configuration to tell the container how to inject dependency / interface

