// See https://aka.ms/new-console-template for more information
using learcsharp;

Exercise ex = new Exercise();
ex.Run();

public class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
        Console.WriteLine($"[Person] Bonjour {Name}");
    }
}

// Classe dérivée
public class Student : Person
{
    public int Id { get; set; }
    public string School { get; set; }

    public Student(string name, int id, string school) : base(name)
    {
        Id = id;
        School = school;
        Console.WriteLine($"[Student] Étudiant {Name}, Id: {Id}, École: {School}");
    }
}

// Classe contenant la logique de l'exercice
public class Exercise
{
    public void Run()
    {
        // Instanciation de deux étudiants
        Student s1 = new Student("Alice", 101, "Université de Paris");
        Student s2 = new Student("Bob", 102, "École Polytechnique");
    }
}
