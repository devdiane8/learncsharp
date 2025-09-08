// See https://aka.ms/new-console-template for more information
using learcsharp;

Console.WriteLine("Hello, World!");

Car car = new Car("Toyota", "Lexus", true);
Customer customer = new Customer("Diane", "123 Main Street", "diane@gmail.com");
customer.SetDetail("I am a customer");

car.Drive();

Console.WriteLine(car.Brand);