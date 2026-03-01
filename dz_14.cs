using System;

// Задача 1

class Book
{
    public string Title;
    public string Author;
}

// Задача 2

class Rectangle
{
    private double _width;
    private double _height;

    public void SetDimensions(double w, double h)
    {
        _width = w;
        _height = h;
    }

    public double GetArea()
    {
        return _width * _height;
    }
}

// Задача 3

class User
{
    public string Login { get; set; }
    public string Password { get; set; }
    public int Id { get; private set; }

    public User(int id)
    {
        Id = id;
    }
}

// Задача 4

class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public Car(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }
}

// Задача 5

class Money
{
    public double Amount { get; set; }
    public double ExchangeRate { get; set; }

    public double AmountInRubles
    {
        get { return Amount * ExchangeRate; }
    }
}

// Задача 6

enum DayTime { Morning, Afternoon, Evening, Night }

class Greeter
{
    public void SayHello(DayTime time)
    {
        switch (time)
        {
            case DayTime.Morning:
                Console.WriteLine("Доброе утро!");
                break;
            case DayTime.Afternoon:
                Console.WriteLine("Добрый день!");
                break;
            case DayTime.Evening:
                Console.WriteLine("Добрый вечер!");
                break;
            case DayTime.Night:
                Console.WriteLine("Доброй ночи!");
                break;
        }
    }
}

// Задача 7

class MathHelper
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }
}

// Задача 8

class Person
{
    private int _age;

    public int Age
    {
        get { return _age; }
        set
        {
            if (value < 0 || value > 120)
            {
                Console.WriteLine("Ошибка: некорректный возраст");
            }
            else
            {
                _age = value;
            }
        }
    }
}

// Задача 9

enum AccessLevel { Guest, User, Admin }

class Account
{
    public string Username { get; set; }
    public AccessLevel Role { get; set; }

    public Account(string username, AccessLevel role)
    {
        Username = username;
        Role = role;
    }

    public void ShowMenu()
    {
        switch (Role)
        {
            case AccessLevel.Admin:
                Console.WriteLine("Full Access");
                break;
            case AccessLevel.User:
                Console.WriteLine("Limited Access");
                break;
            case AccessLevel.Guest:
                Console.WriteLine("Read Only");
                break;
        }
    }
}

// Задача 10

enum HeroType { Warrior, Mage, Archer }

class Hero
{
    public string Name { get; }
    private int _health;
    public HeroType Type { get; set; }

    public int Health
    {
        get { return _health; }
        set
        {
            if (value > 100)
                _health = 100;
            else if (value < 0)
                _health = 0;
            else
                _health = value;
        }
    }

    public bool IsAlive
    {
        get { return _health > 0; }
    }

    public Hero(string name, HeroType type)
    {
        Name = name;
        Type = type;
        Health = 100;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public void TakeDamage(int damage, bool isCritical)
    {
        if (isCritical)
            Health -= damage * 2;
        else
            Health -= damage;
    }
}



class Program
{
    static void Main()
    {

        Book book = new Book();
        book.Title = "1984";
        book.Author = "George Orwell";
        Console.WriteLine(book.Title + " - " + book.Author);

        Rectangle rect = new Rectangle();
        rect.SetDimensions(5, 3);
        Console.WriteLine("Area: " + rect.GetArea());

        MathHelper math = new MathHelper();
        Console.WriteLine(math.Add(2, 3));
        Console.WriteLine(math.Add(2, 3, 4));
        Console.WriteLine(math.Add(2.5, 3.7));

        Greeter g = new Greeter();
        g.SayHello(DayTime.Morning);

        Hero hero = new Hero("Arthas", HeroType.Warrior);
        hero.TakeDamage(30, true);
        Console.WriteLine("HP: " + hero.Health);
        Console.WriteLine("Alive: " + hero.IsAlive);
    }
}
