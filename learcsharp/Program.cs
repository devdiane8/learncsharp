// See https://aka.ms/new-console-template for more information
using learcsharp;

// Données de départ
var studentsOriginal = new List<Student>
        {
            new Student { Name = "Alice",   Age = 20, Grade = 15.5 },
            new Student { Name = "Bob",     Age = 17, Grade = 12.3 },
            new Student { Name = "Charlie", Age = 21, Grade = 17.8 },
            new Student { Name = "David",   Age = 18, Grade = 9.5  },
            new Student { Name = "Eve",     Age = 22, Grade = 18.5 },
            new Student { Name = "Annie",   Age = 22, Grade = 11.9 },
            new Student { Name = null,      Age = 19, Grade = 13.2 },
            new Student { Name = "Bryan",   Age = 23, Grade = 16.4 },
            new Student { Name = "Ana",     Age = 20, Grade = 14.0 }
        };

// On travaille sur des copies selon les besoins
var students = studentsOriginal.ToList();

// ========== 1) FILTRES ==========
// A. majeurs (Age >= 18)
List<Student> majors1 = null!;
majors1 = students.FindAll(IsAdult);

List<Student> majors2 = null!;
majors2 = students.FindAll(s => s.Age >= 18);

CheckEq("1A majeurs (équivalence)", majors1, majors2);

// B. note >= 16
var topGrades1 = students.FindAll(s => s.Grade >= 16);
var topGrades2 = students.Where(s => s.Grade >= 16).ToList();
CheckEq("1B note >= 16 (FindAll vs Where)", topGrades1, topGrades2);

// C. nom contient "an" (case-insensitive)
var withAn = students.FindAll(s =>
    (s.Name ?? "").Contains("an", StringComparison.OrdinalIgnoreCase));
ExpectNames("1C contient 'an'", withAn, "Charlie", "Eve", "Bryan", "Ana");

// ========== 2) RECHERCHE / EXISTENCE ==========
// A. premier avec note < 10
var firstLow = students.Find(s => s.Grade < 10);
Console.WriteLine($"2A Find note<10 => {firstLow}");

// B. index du 1er nom de 5 lettres
int idx5 = students.FindIndex(s => (s.Name ?? "").Length == 5);
Console.WriteLine($"2B FindIndex Name.Length==5 => {idx5} (attendu: index de 'Alice' = 0)");

// C. existe au moins un étudiant de 22 ans ?
bool any22 = students.Exists(s => s.Age == 22);
Console.WriteLine($"2C Exists Age==22 => {any22} (attendu: True)");

// ========== 3) SUPPRESSION ==========
var copyForRemoval = students.ToList();
int removed = copyForRemoval.RemoveAll(s => s.Grade < 12);
Console.WriteLine($"3 RemoveAll note<12 => supprimés: {removed}");
// Vérifie qu'on n'a pas modifié la liste originale
Console.WriteLine($"3 Original intact ? {students.Count == studentsOriginal.Count}");

// ========== 4) TRI ==========
// A. Sort par âge croissant
var byAgeAsc = students.ToList();
byAgeAsc.Sort((a, b) => a.Age.CompareTo(b.Age));
Console.WriteLine("4A Tri âge ↑");
PrintFew(byAgeAsc);

// B. Sort + Reverse = âge décroissant
var byAgeDesc = students.ToList();
byAgeDesc.Sort((a, b) => a.Age.CompareTo(b.Age));
byAgeDesc.Reverse();
Console.WriteLine("4B Tri âge ↓");
PrintFew(byAgeDesc);

// C. LINQ : OrderBy(Name) puis ThenByDescending(Grade)
var nameThenGrade = students
    .OrderBy(s => s.Name, StringComparer.OrdinalIgnoreCase) // gère null plus bas
    .ThenByDescending(s => s.Grade)
    .ToList();
Console.WriteLine("4C Tri Name ↑ puis Grade ↓");
PrintFew(nameThenGrade);

// D. Comparison<T> : longueur du nom puis alphabétique
var byNameLenThenAlpha = students.ToList();
byNameLenThenAlpha.Sort((x, y) =>
{
    int lenX = (x.Name ?? "").Length;
    int lenY = (y.Name ?? "").Length;
    int byLen = lenX.CompareTo(lenY);
    if (byLen != 0) return byLen;
    return string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
});
Console.WriteLine("4D Tri par longueur du nom puis alpha");
PrintFew(byNameLenThenAlpha);

// ========== 5) CLOSURES ==========
// A. Fabrique de prédicats qui capture minAge
int minAge = 18;
Predicate<Student> predAdults = AdultsWithThreshold(minAge);
var adultsSet1 = students.FindAll(predAdults);
Console.WriteLine($"5A closure minAge={minAge} -> count={adultsSet1.Count}");

// B. On modifie minAge après coup et on réévalue
minAge = 21; // la lambda créée plus haut CAPTURE la variable, pas sa valeur
var adultsSet2 = students.FindAll(predAdults);
Console.WriteLine($"5B closure minAge={minAge} (après modif) -> count={adultsSet2.Count} (devrait changer)");

// C. Variante sans capture: static lambda
//    on passe le seuil via une méthode auxiliaire
var adultsStatic = students.FindAll(s => IsAdultWithMin(s, 20));
var adultsStatic2 = students.FindAll(static s => IsAdultWithMin(s, 20)); // C# 9+: aucune capture possible
CheckEq("5C static lambda (résultats identiques)", adultsStatic, adultsStatic2);

// ========== 6) FindAll vs LINQ (ordre identique ?) ==========
var f1 = students.FindAll(s => s.Age >= 20 && s.Grade >= 14);
var f2 = students.Where(s => s.Age >= 20 && s.Grade >= 14).ToList();
CheckEq("6 FindAll vs Where same order", f1, f2);

// ========== 7) Robustesse (null + case) ==========
var startsWithA = students.FindAll(s =>
    !string.IsNullOrEmpty(s.Name) &&
    s.Name!.StartsWith("A", StringComparison.OrdinalIgnoreCase));
ExpectNames("7 startsWith 'A'", startsWithA, "Alice", "Annie", "Ana");

Console.WriteLine("\n== Fin ==");


// ----- Méthodes utilitaires demandées / à connaître -----

// 1A (méthode nommée pour Predicate<T>)
static bool IsAdult(Student s) => s.Age >= 18;

// 5A (closure) : fabrique un prédicat qui CAPTURE la variable minAge
static Predicate<Student> AdultsWithThreshold(int minAge)
    => s => s.Age >= minAge;

// 5C (aucune capture) : passe minAge en paramètre (compatible static lambda appelante)
static bool IsAdultWithMin(Student s, int minAge) => s.Age >= minAge;

// ----- Mini helpers de test -----

static void PrintFew(IEnumerable<Student> list, int take = 5)
{
    foreach (var s in list.Take(take))
        Console.WriteLine("  " + s);
    Console.WriteLine();
}

static void CheckEq(string label, List<Student> a, List<Student> b)
{
    bool equal = a.Count == b.Count && a.Zip(b).All(t =>
        (t.First.Name ?? "") == (t.Second.Name ?? "") &&
        t.First.Age == t.Second.Age &&
        Math.Abs(t.First.Grade - t.Second.Grade) < 1e-9);

    Console.WriteLine($"{label}: {(equal ? "OK" : "FAIL")}");
}

static void ExpectNames(string label, IEnumerable<Student> seq, params string[] expected)
{
    var names = seq.Select(s => s.Name ?? "<null>").ToList();
    bool ok = names.OrderBy(n => n).SequenceEqual(expected.OrderBy(n => n));
    Console.WriteLine($"{label}: {(ok ? "OK" : "FAIL")} -> [{string.Join(", ", names)}]");
}