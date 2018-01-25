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