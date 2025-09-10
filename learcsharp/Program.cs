

public interface IPayment
{
    public void ProcessPayment();
}

public class CreditCardPayment : IPayment
{
    public void ProcessPayment()
    {
        Console.WriteLine($"Processing credit card payment");
    }
}

public class PayPalPayment : IPayment
{

    public void ProcessPayment()
    {
        Console.WriteLine($"Processing PayPal payment");
    }
}

public class Exercise
{
    public void ProcessPayments()
    {
        CreditCardPayment ccPayment = new CreditCardPayment();
        PayPalPayment payPalPayment = new PayPalPayment();

        ccPayment.ProcessPayment();
        payPalPayment.ProcessPayment();
    }
}