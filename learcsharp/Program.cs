// See https://aka.ms/new-console-template for more information
using learcsharp;

//creating a new list and initialise it 

List<string> fruits = new List<string> { "Apple", "Banana", "Mango", "Orange" };

Console.WriteLine("Original List:");
foreach (var fruit in fruits)
{
    Console.WriteLine(fruit);
}

// 1. Remove by value
fruits.Remove("Banana");  // Removes the first occurrence of "Banana"

// 2. Remove by index
fruits.RemoveAt(1);       // Removes the item at index 1 (here "Mango")

// 3. Remove all items that match a condition (using lambda)
fruits.RemoveAll(f => f.StartsWith("O")); // Removes "Orange"

// 4. Clear the whole list
// fruits.Clear();

Console.WriteLine("\nList after removals:");
foreach (var fruit in fruits)
{
    Console.WriteLine(fruit);
}