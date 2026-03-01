using System;

// Задание 1

class User
{
    private static int _userCount = 0;
    public static int IdCounter = 0;

    public int Id { get; private set; }
    public string Name { get; set; }

    public User(string name)
    {
        IdCounter++;
        _userCount++;

        Id = IdCounter;
        Name = name;
    }

    public static int GetTotalUsers()
    {
        return _userCount;
    }
}


// Задание 2

class Planet
{
    private const double G = 6.67430e-11;

    public string Name { get; }
    public double Mass { get; }
    public double Radius { get; }

    private Planet(string name, double mass, double radius)
    {
        Name = name;
        Mass = mass;
        Radius = radius;
    }

    public double CalculateGravity()
    {
        return G * Mass / (Radius * Radius);
    }

    public static readonly Planet Mercury =
        new Planet("Mercury", 3.3011e23, 2439700);

    public static readonly Planet Venus =
        new Planet("Venus", 4.8675e24, 6051800);

    public static readonly Planet Earth =
        new Planet("Earth", 5.972e24, 6371000);

    public static readonly Planet Mars =
        new Planet("Mars", 6.4171e23, 3389500);
}


// Задание 3

abstract class LogLevel
{
    public string Name { get; }

    protected LogLevel(string name)
    {
        Name = name;
    }

    public abstract ConsoleColor GetConsoleColor();

    public static readonly LogLevel Info = new InfoLevel();
    public static readonly LogLevel Warning = new WarningLevel();
    public static readonly LogLevel Error = new ErrorLevel();

    private class InfoLevel : LogLevel
    {
        public InfoLevel() : base("Info") { }

        public override ConsoleColor GetConsoleColor()
        {
            return ConsoleColor.Green;
        }
    }

    private class WarningLevel : LogLevel
    {
        public WarningLevel() : base("Warning") { }

        public override ConsoleColor GetConsoleColor()
        {
            return ConsoleColor.Yellow;
        }
    }

    private class ErrorLevel : LogLevel
    {
        public ErrorLevel() : base("Error") { }

        public override ConsoleColor GetConsoleColor()
        {
            return ConsoleColor.Red;
        }
    }
}

static class Logger
{
    public static void Log(string message, LogLevel level)
    {
        Console.ForegroundColor = level.GetConsoleColor();
        Console.WriteLine($"[{level.Name}] {message}");
        Console.ResetColor();
    }
}




class Program
{
    static void Main()
    {
        //  1
        User u1 = new User("Иван");
        User u2 = new User("Мария");
        User u3 = new User("Алексей");

        Console.WriteLine("ID пользователей:");
        Console.WriteLine(u1.Id);
        Console.WriteLine(u2.Id);
        Console.WriteLine(u3.Id);

        Console.WriteLine("Всего пользователей: " + User.GetTotalUsers());

        Console.WriteLine();

        //  2
        Console.WriteLine("Планета: " + Planet.Earth.Name);
        Console.WriteLine("Гравитация: " + Planet.Earth.CalculateGravity());

        Console.WriteLine("Планета: " + Planet.Mars.Name);
        Console.WriteLine("Гравитация: " + Planet.Mars.CalculateGravity());

        Console.WriteLine();

        // 3
        Logger.Log("Система запущена", LogLevel.Info);
        Logger.Log("Память заканчивается", LogLevel.Warning);
        Logger.Log("Критический сбой!", LogLevel.Error);
    }
}
