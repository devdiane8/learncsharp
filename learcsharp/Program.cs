// See https://aka.ms/new-console-template for more information
using learcsharp;

Person p1 = new Person("Alice");
Person p2 = new Person("Bob");

// Utilisation des méthodes héritées de Object
Console.WriteLine(p1.ToString());   // Person: Alice
Console.WriteLine(p1.GetType());    // Coding.Exercise.Person
Console.WriteLine(p1.GetHashCode()); // entier unique (dépend de l'objet)
Console.WriteLine(p1.Equals(p2));   // False car Alice != Bob
Console.WriteLine(p1.Equals(p1));   // True
public class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }

    // Redéfinition de ToString (hérité de Object)
    public override string ToString()
    {
        return $"Person: {Name}";
    }
}