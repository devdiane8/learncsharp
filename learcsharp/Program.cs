// See https://aka.ms/new-console-template for more information

// 1. Déclarer l’interface IPayment

Checkout checkout = new Checkout(new CreditCardPayment());
checkout.Process(100000);
Checkout checkout1 = new Checkout(new PaypalPayment());
checkout1.Process(50000);
public interface IPayment
{
    void Pay(decimal amount);
}


public class PaypalPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Paypal payment for {amount}");
    }
}

public class CreditCardPayment : IPayment
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Credit card payment for {amount}");
    }
}

public class Checkout
{
    private readonly IPayment _payment;

    public Checkout(IPayment payment)
    {
        _payment = payment;
    }

    public void Process(decimal amount)
    {
        _payment.Pay(amount);
    }
}