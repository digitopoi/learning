# Events, Delegates, and Lambdas

## Introduction

Think about a tin-can and a string.

Person speaking on one end is the Event Raiser and person listening on the other end in the Event Handler.

A delegate is the wire between them and makes everything possible.

The EventArgs is the sound traveling across the wire - the data being transferred.

### The Role of Events

Events are notifications - going out to "subscribers" or objects listening for the event.

Provide a way to triger notifications from end users or from objects.

Simple example is a button:

Button --> [ Click Event ]

Events signal the occurence of an action/notification.

**Objects that raise events don't need to explicitly know the object that will handle the event.**

Events pass EventArgs (event data)

### The Role of Delegates

Without delegates, we wouldn't have a way for the event data to get from point A (event raiser) to point B (event listener/handler).

A delegate is a specialized class often called a "Function Pointer"

The glue/pipeline between an event and an event handler.

Based on the MulticastDelegate base class. This class tracks everything that's listening for the event - **invocation list**

Delegates are a pipeline.

[ EVENT ] ============ [ DELEGATE ] =============== [ EVENT HANDLER ]

Delegates route data from point A to Point B.

The Event Handler will ultimately be a function. So, we need to point the data through the pipeline to our function.

### The Role of Event Handlers

Event Handler is a method responsible for receiving and processing data from a delegate.

Think of it like an email Inbox - a dumping ground for events sent out from other people.

Normally receives two parameters:

1. Sender object

2. EventArgs - encapsulates event data

**An event handler is responsible for accessing data passed by a delegate**

```cs
public void btnSubmit_Click(object sender, EventArgs e)
{
    //  handling of button click
}
```

## Creating Delegates, Events, and EventArgs

### Creating a Delegate

Custom delegates are defined using the delegate keyword.

```cs
public delegate void WorkPerformedHandler(int hours, WorkType workType);
```

In essence, you define a blueprint for an event handler or a handler method that the data will be dumped into.

Delegate is a bit of a magic keyword - behind the scenes, when the compiler sees a delegate - it generates a class that inherits from some other .NET framework delegate classes.

Because the example above is void - it is a one-way pipeline. Data isn't returned.

The EventHandler, must have the same parameters (obviously).

Example handler method:

```cs
public void Manager_WorkPerformed(int workHours, WorkType wType)
{
    //  ...
}
```

#### Delegate Base Classes

1. Delegate

Method property - definition of the method where the data should go

Target - if you have to have an object instance where the method lives, then the target would be the actual object

GetInvocationList() - ties into MulticastDelegates

2. MulticastDelegate

- a way to hold multiple delegates - many pipelines dumping data into many methods

- references more than one delegate function

- tracks delegate references using an invocation list [ an array of delegates ]

- delegates are invoked sequentially

3. Custom Delegate - you can't inherit from Delegate or MulticastDelegate - you use the delegate keyword

#### Creating a Delegate Instance

Delegate:

```cs
public delegate void WorkPerformedHandler(int hours, WorkType workType);
```

After creating a delegate, we need somewhere for that data to go.

Handler:

```cs
static void WorkPerformed1(int hours, WorkType workType)
{
    Console.WriteLine("Work Performed1 called";)
}
```

Delegate Instance:

```cs
WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
```

Handler is passed to the constructor of the delegate.

Invoking a Delegate Instance:

```cs
del1(5, WorkType.Golf);
```

Adding to the invocation list:

```cs
WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);
```

What is we want when delegate 1 is instantiated and invoked that it invoked WorkPerformed1 *and* WorkPerformed2.

```cs
del1 += del2;
```

#### Demo

```cs
namespace DelegatesAndEvents
{
    public delegate void WorkPerformedHandler(int hours, WorkType workType);

    class Program
    {
        static void Main(string[] args)
        {
            WorkPerformedHandler del1 = new WorkPerformedHandler(WorkPerformed1);
            WorkPerformedHandler del2 = new WorkPerformedHandler(WorkPerformed2);

            // del1(5, WorkType.Golf);
            // del2(10, WorkType.GenerateReports);

            DoWork(del1);
        }

        static void DoWork(WorkPerformedHandler del)
        {
            del(5, WorkType.GoToMeetings);
        }

        static void WorkPerformed1(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPerformed1 called " + hours.ToString());
        }

        static void WorkPerformed2(int hours, WorkType workType)
        {
            Console.WriteLine("WorkPerformed2 called " + hours.ToString());
        }
    }

    public enum WorkType
    {
        GoToMeetings,
        Golf,
        GenerateReports
    }
}
```

### Defining an Event

### Raising Events

### Creating an EventArgs Class

## Handling Events