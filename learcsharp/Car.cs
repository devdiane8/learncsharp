using System;

namespace learcsharp;

public class Car
{
    // member variable
    // private hides the variable from other classes
    private string _model = "";

    private string _brand = "";

    private bool _isLuxury;

    // Property
    // With lambda expression
    public string Model { get => _model; set => _model = value; }

    /*
    public string Model
    {
        get
        {
            return _model;
        }
        set
        {
            _model = value;
        }
    }
    */
    public string Brand
    {

        get
        {
            if (_isLuxury)
            {
                return _brand + " - Luxury Edition";
            }
            else
            {
                return _brand;
            }
        }

        set
        {
            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Value is null or empty ${value}");
                _brand = "DEFAULTVALUE";
            }
            else
            {
                _brand = value;
            }
        }

    }

    public bool IsLuxury
    {
        get => _isLuxury;
        set => _isLuxury = value;
    }

    // Custom Constructor
    public Car(string model, string brand, bool isLuxury)
    {
        Model = model;
        Brand = brand;
        Console.WriteLine($"A {Brand} of the" +
            $" model {Model} has been created");
        IsLuxury = isLuxury;
    }

    public void Drive()
    {
        Console.WriteLine("I  am driving");
    }
}
