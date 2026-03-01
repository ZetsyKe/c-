using System;
using System.Collections.Generic;

interface IAnimal
{
    void MakeSound();
}

class Dog : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Гав");
    }
}

class Cat : IAnimal
{
    public void MakeSound()
    {
        Console.WriteLine("Мяу");
    }
}

interface IDevice
{
    void TurnOn();
    void TurnOff();
}

class TV : IDevice
{
    public void TurnOn()
    {
        Console.WriteLine("Телевизор включен");
    }

    public void TurnOff()
    {
        Console.WriteLine("Телевизор выключен");
    }
}

class Radio : IDevice
{
    public void TurnOn()
    {
        Console.WriteLine("Радио включено");
    }

    public void TurnOff()
    {
        Console.WriteLine("Радио выключено");
    }
}

class RemoteControl
{
    public static void UseDevice(IDevice device)
    {
        device.TurnOn();
        device.TurnOff();
    }
}

interface IShape
{
    double GetArea();
}

class Square : IShape
{
    public double Side;

    public Square(double side)
    {
        Side = side;
    }

    public double GetArea()
    {
        return Side * Side;
    }
}

class Rectangle : IShape
{
    public double Width;
    public double Height;

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double GetArea()
    {
        return Width * Height;
    }
}

interface ICleaner
{
    void Clean();
}

interface IChargable
{
    void Charge();
}

class Robot : ICleaner, IChargable
{
    public void Clean()
    {
        Console.WriteLine("Робот убирает комнату");
    }

    public void Charge()
    {
        Console.WriteLine("Робот заряжается");
    }
}

interface IStorage
{
    void Save(string data);
    void Load();
}

class DatabaseStorage : IStorage
{
    public void Save(string data)
    {
        Console.WriteLine("Данные сохранены в базу");
    }

    public void Load()
    {
        Console.WriteLine("Данные загружены из базы");
    }
}

class AgeChecker
{
    public static void CheckAge(int age)
    {
        if (age < 0 || age > 120)
            throw new ArgumentOutOfRangeException("Возраст вне допустимого диапазона");

        Console.WriteLine("Возраст корректный");
    }
}

class Program
{
    static void Main()
    {
        List<IAnimal> animals = new List<IAnimal>
        {
            new Dog(),
            new Cat()
        };

        foreach (var animal in animals)
            animal.MakeSound();

        Console.WriteLine();

        RemoteControl.UseDevice(new TV());
        RemoteControl.UseDevice(new Radio());

        Console.WriteLine();

        List<IShape> shapes = new List<IShape>
        {
            new Square(4),
            new Rectangle(3, 5)
        };

        foreach (var shape in shapes)
            Console.WriteLine("Площадь: " + shape.GetArea());

        Console.WriteLine();

        Robot robot = new Robot();
        robot.Clean();
        robot.Charge();

        Console.WriteLine();

        IStorage storage = new DatabaseStorage();
        storage.Save("test");
        storage.Load();

        Console.WriteLine();

        try
        {
            Console.Write("Введите первое число: ");
            int a = int.Parse(Console.ReadLine());

            Console.Write("Введите второе число: ");
            int b = int.Parse(Console.ReadLine());

            Console.WriteLine("Результат: " + (a / b));
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Ошибка: деление на ноль");
        }

        Console.WriteLine();

        try
        {
            Console.Write("Введите число: ");
            int number = int.Parse(Console.ReadLine());
            Console.WriteLine("Вы ввели: " + number);
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: введено не число");
        }

        Console.WriteLine();

        try
        {
            AgeChecker.CheckAge(150);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Ошибка возраста: " + ex.Message);
        }

        Console.WriteLine();

        try
        {
            int[] arr = new int[5];
            Console.WriteLine(arr[9]);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Ошибка: выход за пределы массива");
        }

        Console.WriteLine();

        try
        {
            Console.WriteLine("Работа с файлом...");
            throw new Exception("Ошибка");
        }
        catch (Exception)
        {
            Console.WriteLine("Произошла ошибка");
        }
        finally
        {
            Console.WriteLine("Связь с ресурсом закрыта");
        }
    }
}
