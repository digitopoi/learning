# Programming with Intent – Method Design - Shiv Kumar

`Programming with intent is a new paradigm wherein, every bit of code you write is expressing your intent very clearly, leave absolutely no ambiguities for the reader of your code of your future self. Further, because you're not programming to "what if" scenarios, your code becomes succinct, maintainable, and completely bug free.`

`In this session, we'll be introduced to various tenants of this new paradigm, with lots of code examples. This session questions many of today's common practices, punches holes in these practices and makes you think really hard about what you should be doing going forward.`

[youtube](youtube.com/matlus)

## Method Design

Methods are the foundational units of work and have a direct impact on how we reason about the code we write.

Good method design leads to good class design and thus good system/API design.

Tell a story - don't make me think, don't make me wonder, no head-scratching moments!

Naming is everything - name of classes, properties, methods (formal arguments, local variables)

If you've named things correctly - your code is more readable, understandable and maintainable.

Idiomatic code is important

If formal arguments convey business information, they should be named as per the domain

Methods should start their life as private. 

Public/internal methods should not be virtual

Methods once override should be sealed unless the class itself is sealed.

Methods should be autonomous. That is, the method should ask for all the data it needs via its formal arguments (it shouldn't rely on state).

Methods should be pure. That is, the method's implementation should operate only on the formal arguments and any local variables.

Method should be provided with the bare minimum information. **Nothing more** and nothing less.

- This makes it clear to consuming code what is actually required by the method

Methods that are "Actions" or "Commands" should return `void` (returning `bool` in an update is wrong in his opinion)

Methods that are Queries should return only the data they claim or not return at all

If a method claims to return something, this is should return that or not return at all (throw exception).

String.Empty, IsNullOrEmpty, IsNullOrWhiteSpace - bad (is it null or is it empty?)

If you must check for empty - check for `length == 0`

`Trim()`, `TrimStart()`, `TrimEnd()` - bad 

Prefer `Single()` over `First()`

Declare variables just before use - limit their scope 

Use var when you can

Avoid using `as` - favor casting

Formal arguments of a method should be of the **least derived** type possible. For collection types prefer interfaces over base classes.

Method return types should be the **most derived** type possible. The caller of the method can choose to treat more derived type as a base type if needed.

Methods should not lie

Be skeptical of methods with bool parameters (usually a check - method doing two different things)