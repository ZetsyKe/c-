using System;
using System.Collections.Generic;

// Задача 1.1

class Wrapper<T>
{
    private readonly T _value;
    public const string TypeName = "Wrapper";

    public Wrapper(T value)
    {
        _value = value;
    }

    public T GetValueOrDefault(T defaultValue)
    {
        if (_value == null || _value.Equals(default(T)))
            return defaultValue;

        return _value;
    }

    public override string ToString()
    {
        return $"{TypeName}: {_value?.ToString() ?? "null"}";
    }
}


// Задача 1.2

class Entity
{
    public readonly Guid Id;

    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public virtual string Description
    {
        get { return $"Entity {Id}"; }
    }
}

class NamedEntity : Entity
{
    public readonly string Name;

    public NamedEntity(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be empty");

        Name = name;
    }

    public override string Description
    {
        get
        {
            if (Name == null)
                return base.Description;

            return $"Named entity \"{Name}\" (Id: {Id})";
        }
    }
}


// Задача 1.3

abstract class Shape
{
    public const double Pi = 3.1415926535;
    public readonly string Color;

    protected Shape(string color)
    {
        Color = color;
    }

    public abstract double GetArea();
}

class Circle : Shape
{
    public readonly double Radius;

    public Circle(string color, double radius) : base(color)
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Pi * Radius * Radius;
    }
}

class Rectangle : Shape
{
    public readonly double Width;
    public readonly double Height;

    public Rectangle(string color, double width, double height) : base(color)
    {
        Width = width;
        Height = height;
    }

    public override double GetArea()
    {
        return Width * Height;
    }
}

static class ShapeHelper
{
    public static void PrintInfo<T>(T shape) where T : Shape
    {
        Console.WriteLine($"Color: {shape.Color}, Area: {shape.GetArea()}");
    }
}


// Задача 1.4

class Employee
{
    public readonly string FullName;
    public readonly decimal BaseSalary;

    public Employee(string name, decimal salary)
    {
        FullName = name;
        BaseSalary = salary;
    }

    public virtual decimal CalculateSalary()
    {
        return BaseSalary;
    }
}

class Manager : Employee
{
    public readonly decimal Bonus;

    public Manager(string name, decimal salary, decimal bonus)
        : base(name, salary)
    {
        Bonus = bonus;
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary + Bonus;
    }
}

class Intern : Employee
{
    public Manager Mentor;

    public Intern(string name, decimal salary, Manager mentor)
        : base(name, salary)
    {
        Mentor = mentor;
    }

    public override decimal CalculateSalary()
    {
        if (Mentor != null)
            return BaseSalary + 0.1m * Mentor.CalculateSalary();

        return BaseSalary;
    }
}


// Задача 1.5

abstract class Message
{
    public const string DefaultAuthor = "System";
    public readonly string Text;

    protected Message(string text)
    {
        Text = text;
    }

    public abstract string GetFormattedText();
}

class EmailMessage : Message
{
    public readonly string Recipient;

    public EmailMessage(string text, string recipient)
        : base(text)
    {
        Recipient = recipient;
    }

    public override string GetFormattedText()
    {
        return $"[{Recipient}] {Text}";
    }
}

class SmsMessage : Message
{
    public readonly string PhoneNumber;

    public SmsMessage(string text, string phone)
        : base(text)
    {
        PhoneNumber = phone;
    }

    public override string GetFormattedText()
    {
        return $"[{PhoneNumber}] {Text}";
    }
}

class MessageProcessor<T> where T : Message
{
    private readonly List<T> _messages = new List<T>();

    public void AddMessage(T message)
    {
        if (message != null)
            _messages.Add(message);
    }

    public void ProcessAll()
    {
        foreach (var m in _messages)
            Console.WriteLine(m?.GetFormattedText());
    }
}


// Задача 1.6

abstract class Vehicle
{
    public readonly int MaxSpeed;
    public const int MinSpeed = 0;

    protected Vehicle(int maxSpeed)
    {
        MaxSpeed = maxSpeed;
    }

    public abstract string GetInfo();
}

class Car : Vehicle
{
    public string FuelType;

    public Car(int speed, string fuel) : base(speed)
    {
        FuelType = fuel;
    }

    public override string GetInfo()
    {
        return $"Car, Fuel: {FuelType}, MaxSpeed: {MaxSpeed}";
    }
}

class Bicycle : Vehicle
{
    public bool HasGears;

    public Bicycle(int speed, bool gears) : base(speed)
    {
        HasGears = gears;
    }

    public override string GetInfo()
    {
        return $"Bicycle, Gears: {HasGears}, MaxSpeed: {MaxSpeed}";
    }
}

static class VehicleHelper
{
    public static T GetFaster<T>(T a, T b) where T : Vehicle
    {
        if (a.MaxSpeed >= b.MaxSpeed)
            return a;

        return b;
    }
}




class Program
{
    static void Main()
    {
       
        Wrapper<int?> w1 = new Wrapper<int?>(5);
        Wrapper<string> w2 = new Wrapper<string>(null);

        Console.WriteLine(w1);
        Console.WriteLine(w2);

        
        NamedEntity ne = new NamedEntity("Alice");
        Console.WriteLine(ne.Description);

        
        Circle c = new Circle("Red", 5);
        Rectangle r = new Rectangle("Blue", 4, 6);
        ShapeHelper.PrintInfo(c);
        ShapeHelper.PrintInfo(r);

       
        Manager m = new Manager("Boss", 50000, 10000);
        Intern i = new Intern("Intern", 20000, m);

        Employee[] staff = { m, i };
        foreach (var e in staff)
            Console.WriteLine($"{e.FullName}: {e.CalculateSalary()}");

    
        MessageProcessor<Message> processor = new MessageProcessor<Message>();
        processor.AddMessage(new EmailMessage("Hello", "mail@test.com"));
        processor.AddMessage(new SmsMessage("Hi", "+12345"));
        processor.ProcessAll();

     
        Car car = new Car(200, "Petrol");
        Bicycle bike = new Bicycle(40, true);

        var faster = VehicleHelper.GetFaster(car, bike);
        Console.WriteLine("Faster: " + faster.GetInfo());
    }
}
