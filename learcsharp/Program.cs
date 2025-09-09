// See https://aka.ms/new-console-template for more information
using learcsharp;

Chien rex = new Chien();
rex.Nom = "Rex";           // OK : public
rex.AfficherInfo();        // OK : méthode publique
rex.Presenter();           // OK

class Animal
{
    private string secret = "Je suis privé";      // uniquement dans Animal
    protected string espece = "Animal";           // accessible dans Animal + dérivées
    public string Nom { get; set; }               // accessible partout

    public void AfficherInfo()
    {
        Console.WriteLine($"Nom: {Nom}, Espèce: {espece}");
    }
}

class Chien : Animal
{
    public void Presenter()
    {
        // Console.WriteLine(secret); ❌ Erreur : pas accessible (private)
        Console.WriteLine($"Je suis un {espece} et je m'appelle {Nom}"); // OK (protected + public)
    }
}
