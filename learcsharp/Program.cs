// See https://aka.ms/new-console-template for more information
using learcsharp;

//creating a new list and initialise it 
var students = new List<Student>
        {
            new Student { Name = "Alice",   Age = 20, Grade = 15.5 },
            new Student { Name = "Bob",     Age = 22, Grade = 12.3 },
            new Student { Name = "Charlie", Age = 21, Grade = 17.8 },
            new Student { Name = "David",   Age = 20, Grade = 14.0 },
            new Student { Name = "Eve",     Age = 22, Grade = 18.5 }
        };

// a) Moyenne >= 15
var goodGrades = students.FindAll(s => s.Grade >= 15);

// b) Âge entre 20 et 22 inclus
var age20to22 = students.FindAll(s => s.Age >= 20 && s.Age <= 22);

// c) Méthode prédicat réutilisable (méthode de classe)
static bool IsHonor(Student s) => s.Grade >= 16 && s.Age >= 20;
var honors = students.FindAll(IsHonor);

Console.WriteLine(">=15 : " + string.Join(" | ", goodGrades));
Console.WriteLine("Âge 20..22 : " + string.Join(" | ", age20to22));
Console.WriteLine("Honors : " + string.Join(" | ", honors));