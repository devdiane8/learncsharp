// See https://aka.ms/new-console-template for more information
using learcsharp;

// Héritage simple
Chien rex = new Chien { Nom = "Rex" };
rex.SeDeplacer();   // de Animal
rex.Allaiter();     // de Mammifere
rex.Aboyer();       // de Chien

// Héritage hiérarchique
Chat minou = new Chat { Nom = "Minou" };
minou.SeDeplacer();
minou.Miauler();

// Polymorphisme
Animal a = new Chien { Nom = "Poli" };
a.SeDeplacer(); // méthode d'Animal

// Interface
IDomestique domestique = new Chien();
domestique.VivreAvecHumain();
class Animal
{
    public string Nom { get; set; }
    public void SeDeplacer() => Console.WriteLine("Je me déplace.");
}

class Mammifere : Animal
{
    public void Allaiter() => Console.WriteLine("Je nourris mes petits.");
}

interface IDomestique
{
    void VivreAvecHumain();
}

class Chien : Mammifere, IDomestique
{
    public void Aboyer() => Console.WriteLine("Wouf !");
    public void VivreAvecHumain() => Console.WriteLine("Je vis avec les humains comme animal de compagnie.");
}

class Chat : Mammifere, IDomestique
{
    public void Miauler() => Console.WriteLine("Miaou !");
    public void VivreAvecHumain() => Console.WriteLine("Je vis avec les humains, mais je suis indépendant !");
}
