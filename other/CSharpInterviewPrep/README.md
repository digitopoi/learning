# C# Interview Prep

[link](https://www.youtube.com/watch?v=-1NUQNSVVmk&list=PL6n9fhu94yhWlAv3hnHzOaMSeggILsZFs)

## Can you store different types in an array in C#?

Yes, you can create an object array.

Since all types inherit (directly or indirectly) from the object type, we can add any type to the object array, including complex types (ex. Customer, Employee, etc.). ToString() should be overwritten if you want meaningful output.

```c#
//  ERROR:
int[] array = new int[2];
array[0] = 1;
array[1] = "string";

//  WORKS:
object[] array = new object[3];
array[0] = 1;
array[1] = "string";
var c = new Customer();
array[2] = c;
```

```c#
class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return this.Name;
    }
}
```

## What is a jagged array?

A jagged array is an array of arrays. 

Mark has a bachelors, masters, and doctorate
Matt has a bachelors
John has a bachelor and masters

```c#
string[] employeeNames = new string[3];
employeeNames[0] = "Mark";
employeeNames[1] = "Matt";
employeeNames[2] = "John";

string[][] jaggedArray = new string[3][];

jaggedArray[0] = new string[3];
jaggedArray[1] = new string[1];
jaggedArray[2] = new string[2];

jaggedArray[0] = new string[3];
jaggedArray[1] = new string[1];
jaggedArray[2] = new string[2];

jaggedArray[0][0] = "Bachelor";
jaggedArray[0][1] = "Master";
jaggedArray[0][2] = "Doctorate";

jaggedArray[1][] = "Bachelor";

jaggedArray[2][0] = "Bachelor";
jaggedArray[2][1] = "Master";

for (int i = 0; i < jaggedArray.Length; i++)
{
    Console.WriteLine(employeeNames[i]);
    Console.WriteLine(----------------);
    string[] innerArray = jaggedArray[i];

    for (int j = 0; j < innerArray.Length; j++)
    {
        Console.WriteLine(innerArray[j]);
    }

    Console.WriteLine();
}
```

## Why use abstract classes

Imagine the case of having two similar classes:

```c#
public class FullTimeEmployee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AnnualSalary { get; set; }

    public string GetFullName()
    {
        return this.FirstName + " " + LastName;
    }

    public int GetMonthlySalary()
    {
        return this.AnnualSalary / 12;
    }
}
```

```c#
public class ContractEmployee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int HourlyPay { get; set; }
    public int TotalHoursWorked { get; set; }

    public string GetFullName()
    {
        return this.FirstName + " " + LastName;
    }

    public int GetMonthlySalary()
    {
        return this.HourlyPay * this.TotalHoursWorked;
    }
}
```

Both of these classes are very similar - only difference is AnnualSalary property in FullTimeEmployee vs HourlyPay and TotalHoursWorked for ContractEmployee and the implementation of GetMonthlySalary().

Move common functionality into a base class.

Should we designed the new class as an abstract class or a concrete class?

It depends on whether you want to instantiate and use the base class. In this case, we would only really want to instantiate and use the derived classes.

```c#
public abstract class BaseEmployee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string GetFullName()
    {
        return this.FirstName + " " + LastName;
    }

    public abstract int GetMonthlySalary();
}
```

```c#
public class FullTimeEmployee : BaseEmployee
{
    public int AnnualSalary { get; set; }

    public override int GetMonthlySalary()
    {
        return this.AnnualSalary / 12;
    }
}
```

```c#
public class ContractEmployee : BaseEmployee
{
    public int HourlyPay { get; set; }
    public int TotalHoursWorked { get; set; }

    public override int GetMonthlySalary()
    {
        return this.HourlyPay * this.TotalHoursWorked;
    }
}
```

In short: create an abstract class when we want to move the common functionality of 2 or more related classes into a base class and when we don't want that base class to be instantiated.
