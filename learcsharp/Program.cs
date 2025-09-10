
var payments = new IPayment[]
          {
                new CreditCardPayment(),
                new PaypalPayment(),
                new CryptoPayment() // ajout sans changer CheckoutService
          };

var checkout = new CheckoutService(payments);

// 1) Découplage + utilisation simple
var r1 = checkout.Process("credit-card", 100m);
var r2 = checkout.Process("paypal", 49.99m);
Console.WriteLine(r1);
Console.WriteLine(r2);

// 2) Extensibilité : le nouveau provider "crypto" marche sans changer le service
var r3 = checkout.Process("crypto", 250m);
Console.WriteLine(r3);

// 3) Code générique + réutilisation : traiter plusieurs montants avec tous les providers
var receipts = checkout.ProcessAll<IPayment>(new[] { 10m, 20m });
foreach (var rc in receipts)
    Console.WriteLine(rc);


// ---1) Contrat (Découplage) ---
public interface IPayment
{
    string Name { get; }
    Receipt Pay(decimal amount);
}

public sealed class Receipt
{
    public decimal Amount { get; }
    public string Provider { get; }
    public DateTime Timestamp { get; }

    public Receipt(decimal amount, string provider, DateTime timestamp)
    {
        Amount = amount;
        Provider = provider;
        Timestamp = timestamp;
    }

    public override string ToString() =>
        $"[{Timestamp:HH:mm:ss}] {Provider} => {Amount:C}";
}

// --- 2) Réutilisation du code via classe abstraite ---
public abstract class PaymentBase : IPayment
{
    public abstract string Name { get; }

    public Receipt Pay(decimal amount)
    {
        // Logique commune (validation, horodatage, etc.)
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be > 0");

        // Hook optionnel pour comportement spécifique avant/après
        BeforePay(amount);
        var receipt = CreateReceipt(amount);
        AfterPay(receipt);

        return receipt;
    }

    // Méthode utilitaire commune : REUTILISÉE par tous les paiements
    protected Receipt CreateReceipt(decimal amount) =>
        new Receipt(amount, Name, DateTime.Now);

    // Hooks (extensibles par les dérivés)
    protected virtual void BeforePay(decimal amount) { /* ex: audit, métriques... */ }
    protected virtual void AfterPay(Receipt receipt) { /* ex: traçage... */ }
}

// --- 3) Implémentations concrètes (héritent et réutilisent PaymentBase) ---
public sealed class CreditCardPayment : PaymentBase
{
    public override string Name => "credit-card";

    protected override void BeforePay(decimal amount)
    {
        // Exemple : vérification simple
        // Console.WriteLine("CC: Pré-autorisation OK");
    }
}

public sealed class PaypalPayment : PaymentBase
{
    public override string Name => "paypal";
}

// --- 5) Extensibilité : nouvelle implémentation, sans toucher au service ---
public sealed class CryptoPayment : PaymentBase
{
    public override string Name => "crypto";

    protected override void BeforePay(decimal amount)
    {
        // Exemple : vérifier un solde en cache, etc.
    }
}

// --- 4) Service découplé + méthodes génériques ---
public class CheckoutService
{
    private readonly Dictionary<string, IPayment> _providers;

    // DI : on injecte un ensemble d'IPayment (découplage total des classes concrètes)
    public CheckoutService(IEnumerable<IPayment> payments)
    {
        _providers = payments.ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);
    }

    // Utilisation polymorphique : on ne connaît que le contrat
    public Receipt Process(string providerName, decimal amount)
    {
        if (!_providers.TryGetValue(providerName, out var provider))
            throw new InvalidOperationException($"Provider '{providerName}' not registered.");

        return provider.Pay(amount);
    }

    // Méthode générique : traite des séquences d'amounts avec tous les providers
    public IEnumerable<Receipt> ProcessAll<T>(IEnumerable<decimal> amounts) where T : IPayment
    {
        // On sélectionne seulement les providers du type T (ex: PaymentBase, IPayment, etc.)
        var selected = _providers.Values.OfType<T>().Cast<IPayment>().ToArray();
        foreach (var amount in amounts)
            foreach (var p in selected)
                yield return p.Pay(amount);
    }
}

// --- Démo ---
