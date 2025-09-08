// See https://aka.ms/new-console-template for more information
using learcsharp;

//creating a new list and initialise it 

List<Student> students = new List<Student>();
students.Add(new Student() { Name = "Alice", Age = 20, Grade = 15.5 });
students.Add(new Student() { Name = "Bob", Age = 21, Grade = 16.5 });
students.Add(new Student() { Name = "Charlie", Age = 22, Grade = 17.5 });
students.Add(new Student() { Name = "David", Age = 23, Grade = 18.5 });
students.Add(new Student() { Name = "Eve", Age = 24, Grade = 19.5 });

// a) Sort() → âge croissant (in-place)
var byAgeAsc = students.ToList(); // copie
byAgeAsc.Sort((a, b) => a.Age.CompareTo(b.Age));
Console.WriteLine("Tri ascendant par âge (Sort):");
Display(byAgeAsc);

// b) Sort() + Reverse() → âge décroissant (in-place)
var byAgeDesc = students.ToList(); // copie
byAgeDesc.Sort((a, b) => a.Age.CompareTo(b.Age));
byAgeDesc.Reverse();
Console.WriteLine("Tri descendant par âge (Sort + Reverse):");
Display(byAgeDesc);

// c) LINQ OrderBy → nom alphabétique (retourne une nouvelle séquence)

var bynameAsc = students.OrderBy(s => s.Name).ToList();
Console.WriteLine("Tri ascendant par nom (OrderBy):");
Display(bynameAsc);

// d) LINQ OrderByDescending → moyenne décroissante
var byGradeDesc = students.OrderByDescending(s => s.Grade).ToList();
Console.WriteLine("Tri descendant par moyenne (OrderByDescending):");
Display(byGradeDesc);

// e) Sort(Comparison<T>) → longueur du nom (in-place)
var byNameLength = students.ToList(); // copie
byNameLength.Sort((x, y) => x.Name.Length.CompareTo(y.Name.Length));
Console.WriteLine("Tri par longueur du nom (Sort avec Comparison<T>):");
Display(byNameLength);

// 🔥 Bonus : tri multiple → âge croissant puis note décroissante
var multi = students
    .OrderBy(s => s.Age)
    .ThenByDescending(s => s.Grade)
    .ToList();
Console.WriteLine("Tri multiple (âge ↑ puis note ↓) (LINQ OrderBy + ThenByDescending):");
Display(multi);


static void Display(List<Student> list)
{
    foreach (var s in list)
        Console.WriteLine(s);
    Console.WriteLine();
}