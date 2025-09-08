using System;

namespace learcsharp;

public class Customer
{

    public string Name { get; set; }

    public string Address { get; set; }

    public string Contact { get; set; }
    public string? Detail { get; set; }

    public string? ContactNumber { get; set; }

    private readonly string PrivateLocal;
    public string PrivateReadonly { get; } = "Private Readonly";


    public Customer(string name, string address = "N/A", string contact = "N/A")
    {
        Name = name;
        Address = address;
        Contact = contact;
        
    }

    public void SetDetail(string detail, string contactNumber = "N/A")
    {
        Detail = detail;
        ContactNumber = contactNumber;
        Console.WriteLine($"Detail: {Detail}");
    }



}
