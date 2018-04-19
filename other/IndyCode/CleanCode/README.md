# Clean Code: Homicidal Maniacs Read Code, Too! - Jeremy Clark

`There's no such thing as a write-once application. The world is constantly changing, and our code needs to change with it. The problem comes when the code we need to change is incomprehensible. Why does this happen? And what can be done to fix it? We'll approach our code from the perspective that it needs to be understandable by humans. We'll learn to think in small pieces and take a look at some techniques to keep our code manageable and understandable. Think about the next developer who will maintain the code. Now, imagine he's a homicidal maniac who knows where you live.`

## What is clean code?

1. Readable (not just by senior gurus - but, other devs on team)

2. Maintainable

3. Testable

4. Elegance

## Why do we care?

There's no such thing as write-once code

- bug fixes

- business changes

- enhancements

- new functionality

## What prevents clean code?

Ignorance (easier to fix)

Stubbornness

Short-timer syndrome (think temp contractor - won't be here long, why bother?)

Arrogance

Job Security

Scheduling

NUMBER ON REASON: "I'll clean it up later"

## The trush about clean code

Clean code saves time

We can't take a short-term view of software

We need to look at the lifespan of the application

## How do you write clean code?

**Rule of thumb**: imagine that the developer who comes after you is a homicidal maniac

## The problem

Ex:
Readable (by mere mortals) - does it need to be readable by an entry level dev? customers?

Best practices - not something that should **always** be done - but, keep in mind and use when it makes sense. Have a reason not to do it.

### The DRY principle

### Intentional naming

```cs
//  not good
theList

//  better
ProductList

//  even better
ProductCatalog
```

Use nounds for variables, properties, parameters.

`indexer`, `currentUser`, `PriceFilter`

Use verbs for methods and functions

`SaveOrder()`, `getDiscounts()`, `RunPayroll()`

Pronounceable and unambiguous

- not limited in variable name size - ok to use vowels!

**naming standard** doesn't matter - just be consistent

### Comments

1. Comments lie! (not compiled, code updated or moved - but, not comments)

2. Comments don't make up for bad code (if code is so unclear - rewrite it)

3. Good use example - regex clarification, warnings or consequences, TODOs (should be temporary)

4. Don't comment out code and check it into source control. Code no longer in use should be deleted. If needed, you can always retrieve it from source control.

### Functions

Keep methods short - short fit on relative single screen size - prefer methods no longer than 10 lines

Do one thing

#### Multiple levels of Methods

1. High level - overview of functionality

2. Mid-level - more details, but not too deep

3. Detail - the 'weeds' of the functionality

#### Work in small chunks

**IF YOU AREN'T WRITING INCREMENTAL CODE, YOU'RE WRITING EXCREMENTAL CODE**

### Refactoring

Make the code better without changing the functionality

Where unit testing really comes into play - you don't really know what your code does without tests

Michael Feathers - Working Effectively With Legacy Code

1. Bring your code under test

2. Safely and confidently update the code

The Watcher / Refactoring Basics - look up on github