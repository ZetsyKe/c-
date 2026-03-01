using System;
using System.Collections.Generic;

// Задача 1

class Book
{
    public string Title;
    public string Author;
    public int Year;

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
}

// Задача 2

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int StudentId { get; set; }

    public double AverageGrade { get; private set; }

    public Student(string firstName, string lastName, int id)
    {
        FirstName = firstName;
        LastName = lastName;
        StudentId = id;
        AverageGrade = 0.0;
    }

    public void UpdateGrade(double newGrade)
    {
        AverageGrade = newGrade;
    }
}

// Задача 3

enum TrafficLightColor { Red, Yellow, Green }

class TrafficLight
{
    public TrafficLightColor CurrentColor { get; set; }

    public TrafficLight(TrafficLightColor startColor)
    {
        CurrentColor = startColor;
    }

    public void ChangeColor()
    {
        if (CurrentColor == TrafficLightColor.Red)
            CurrentColor = TrafficLightColor.Green;
        else if (CurrentColor == TrafficLightColor.Green)
            CurrentColor = TrafficLightColor.Yellow;
        else
            CurrentColor = TrafficLightColor.Red;
    }
}

// Задача 4

class Rectangle
{
    public double Width { get; private set; }
    public double Height { get; private set; }

    public Rectangle(double width, double height)
    {
        if (width > 0 && height > 0)
        {
            Width = width;
            Height = height;
        }
        else
        {
            Width = 1;
            Height = 1;
        }
    }

    public double Area
    {
        get { return Width * Height; }
    }

    public double Perimeter
    {
        get { return 2 * (Width + Height); }
    }
}

// Задача 5

class BankAccount
{
    public string AccountNumber { get; }
    public string OwnerName { get; set; }
    public decimal Balance { get; private set; }

    public BankAccount(string number, string owner)
    {
        AccountNumber = number;
        OwnerName = owner;
        Balance = 0;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
            Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance)
            Balance -= amount;
    }
}

// Задача 6

class LibraryBook
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }
    public bool IsAvailable { get; set; }

    public LibraryBook(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
        IsAvailable = true;
    }
}

class Library
{
    private List<LibraryBook> books = new List<LibraryBook>();

    public void AddBook(LibraryBook book)
    {
        books.Add(book);
    }

    public List<LibraryBook> FindBooksByAuthor(string author)
    {
        List<LibraryBook> result = new List<LibraryBook>();

        foreach (var book in books)
            if (book.Author == author)
                result.Add(book);

        return result;
    }

    public void BorrowBook(string title)
    {
        foreach (var book in books)
        {
            if (book.Title == title && book.IsAvailable)
            {
                book.IsAvailable = false;
                break;
            }
        }
    }
}

// Задача 7

class GameCharacter
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Strength { get; set; }

    public GameCharacter(string name, int strength)
    {
        Name = name;
        Health = 100;
        Strength = strength;
    }

    public void Attack(GameCharacter enemy)
    {
        enemy.Health -= Strength;
        Console.WriteLine(Name + " атаковал " + enemy.Name);
    }

    public void Attack(GameCharacter enemy, int multiplier)
    {
        enemy.Health -= Strength * multiplier;
        Console.WriteLine(Name + " нанес усиленный удар " + enemy.Name);
    }

    public void Heal(int amount)
    {
        Health += amount;
        if (Health > 100)
            Health = 100;
    }
}

// Задача 8

enum DeviceStatus { Off, On, Standby }

class SmartDevice
{
    public string Name { get; set; }
    public DeviceStatus Status { get; set; }
    public double PowerConsumption { get; set; }

    public SmartDevice(string name, double power)
    {
        Name = name;
        PowerConsumption = power;
        Status = DeviceStatus.Off;
    }
}

class SmartRoom
{
    public List<SmartDevice> Devices { get; set; } = new List<SmartDevice>();

    public double TotalPowerConsumption
    {
        get
        {
            double total = 0;
            foreach (var device in Devices)
                if (device.Status == DeviceStatus.On)
                    total += device.PowerConsumption;
            return total;
        }
    }

    public void AddDevice(SmartDevice device)
    {
        Devices.Add(device);
    }

    public void TurnOnAllDevices()
    {
        foreach (var device in Devices)
            device.Status = DeviceStatus.On;
    }

    public void TurnOffAllDevices()
    {
        foreach (var device in Devices)
            device.Status = DeviceStatus.Off;
    }
}

// Задача 9

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

class OrderItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public decimal TotalPrice
    {
        get { return Product.Price * Quantity; }
    }
}

class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    public decimal OrderTotal
    {
        get
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.TotalPrice;
            return total;
        }
    }

    public void AddItem(Product product, int quantity)
    {
        Items.Add(new OrderItem { Product = product, Quantity = quantity });
    }
}

// Задача 10

enum Direction { None, Up, Down }
enum DoorStatus { Open, Closed }

class Elevator
{
    private int CurrentFloor;
    private Direction Direction;
    private DoorStatus DoorStatus;
    private bool IsMoving;

    public int MaxFloor { get; }
    public int MinFloor => 1;

    public Elevator(int maxFloor)
    {
        MaxFloor = maxFloor;
        CurrentFloor = 1;
        Direction = Direction.None;
        DoorStatus = DoorStatus.Closed;
        IsMoving = false;
    }

    public void Call(int targetFloor)
    {
        MoveTo(targetFloor);
    }

    public void MoveTo(int targetFloor)
    {
        if (targetFloor < MinFloor || targetFloor > MaxFloor)
            return;

        DoorStatus = DoorStatus.Closed;
        IsMoving = true;

        if (targetFloor > CurrentFloor)
            Direction = Direction.Up;
        else if (targetFloor < CurrentFloor)
            Direction = Direction.Down;

        CurrentFloor = targetFloor;

        IsMoving = false;
        Direction = Direction.None;
    }

    public void OpenDoor()
    {
        if (!IsMoving)
            DoorStatus = DoorStatus.Open;
    }

    public void CloseDoor()
    {
        DoorStatus = DoorStatus.Closed;
    }

    public string Status
    {
        get
        {
            if (IsMoving)
                return "Moving " + Direction;
            else
                return "Stopped on floor " + CurrentFloor + ", doors " + DoorStatus;
        }
    }
}


class Program
{
    static void Main()
    {
        Console.WriteLine("Задачи 1–10 реализованы.");
    }
}
