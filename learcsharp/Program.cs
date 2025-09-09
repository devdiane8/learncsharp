// See https://aka.ms/new-console-template for more information
using learcsharp;


 // 1. Définir le dictionnaire
        Dictionary<int, Student> students = new Dictionary<int, Student>();

        // 2. Ajouter des étudiants
        students.Add(1, new Student(1, "Alice", 15.5));
        students.Add(2, new Student(2, "Bob", 12.0));
        students.Add(3, new Student(3, "Charlie", 17.2));

        // 3. Afficher la liste des étudiants
        Console.WriteLine("Liste des étudiants :");
        foreach (var kvp in students)
        {
            Console.WriteLine(kvp.Value);
        }

        // 4. Recherche par Id
        Console.WriteLine("\nEntrez l'Id de l'étudiant à rechercher :");
        int idRecherche = int.Parse(Console.ReadLine());
        if (students.TryGetValue(idRecherche, out Student etu))
        {
            Console.WriteLine("Étudiant trouvé : " + etu);
        }
        else
        {
            Console.WriteLine("Aucun étudiant avec cet Id.");
        }

        // 5. Supprimer un étudiant
        Console.WriteLine("\nEntrez l'Id de l'étudiant à supprimer :");
        int idSupp = int.Parse(Console.ReadLine());
        if (students.Remove(idSupp))
        {
            Console.WriteLine("Étudiant supprimé avec succès.");
        }
        else
        {
            Console.WriteLine("Impossible de supprimer : Id inexistant.");
        }

        // 6. Afficher à nouveau
        Console.WriteLine("\nNouvelle liste des étudiants :");
        foreach (var kvp in students)
        {
            Console.WriteLine(kvp.Value);
        }