using System;
using System.Collections.Generic;

// Задача 11


class CalendarEvent
{
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public int DurationMinutes { get; set; }

    public CalendarEvent(string name, DateTime date, int duration)
    {
        EventName = name;
        EventDate = date;
        DurationMinutes = duration;
    }

    public bool IsToday
    {
        get { return EventDate.Date == DateTime.Now.Date; }
    }

    public TimeSpan? TimeUntilEvent
    {
        get
        {
            if (EventDate > DateTime.Now)
                return EventDate - DateTime.Now;
            else
                return null;
        }
    }
}


// Задача 12


enum Currency { RUB, USD, EUR }

class Money
{
    public decimal Amount { get; set; }
    public Currency Currency { get; set; }

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public Money ConvertTo(Currency targetCurrency, decimal rate)
    {
        return new Money(Amount * rate, targetCurrency);
    }
}

class Wallet
{
    private List<Money> moneyList = new List<Money>();

    public void AddMoney(Money money)
    {
        moneyList.Add(money);
    }

    public decimal TotalInRub
    {
        get
        {
            decimal total = 0;
            foreach (var m in moneyList)
            {
                if (m.Currency == Currency.RUB)
                    total += m.Amount;
                if (m.Currency == Currency.USD)
                    total += m.Amount * 90;
                if (m.Currency == Currency.EUR)
                    total += m.Amount * 100;
            }
            return total;
        }
    }
}


// Задача 13
class AppSettings
{
    private string _theme;
    private int _fontSize;
    private bool _notifications;

    public string Theme
    {
        get { return _theme; }
        set
        {
            if (value == "Light" || value == "Dark" || value == "System")
                _theme = value;
            else
                _theme = "Light";
        }
    }

    public int FontSize
    {
        get { return _fontSize; }
        set
        {
            if (value >= 8 && value <= 72)
                _fontSize = value;
            else
                _fontSize = 14;
        }
    }

    public bool IsNotificationsEnabled
    {
        get { return _notifications; }
        set { _notifications = value; }
    }

    public AppSettings()
    {
        ResetToDefault();
    }

    public AppSettings(string theme, int fontSize, bool notif)
    {
        Theme = theme;
        FontSize = fontSize;
        IsNotificationsEnabled = notif;
    }

    public void ResetToDefault()
    {
        _theme = "Light";
        _fontSize = 14;
        _notifications = true;
    }
}


// Задача 14



class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

class TechProduct : Product
{
    public int WarrantyMonths { get; set; }
}

class Cart
{
    private List<Product> products = new List<Product>();

    public void Add(Product product)
    {
        products.Add(product);
    }

    public void Add(Product product, int quantity)
    {
        for (int i = 0; i < quantity; i++)
            products.Add(product);
    }

    public void Add(TechProduct product)
    {
        products.Add(product);
        Console.WriteLine("Добавлен тех. товар. Гарантия: " + product.WarrantyMonths + " мес.");
    }

    public decimal TotalPrice
    {
        get
        {
            decimal total = 0;
            foreach (var p in products)
                total += p.Price;
            return total;
        }
    }
}


// Задача 15

enum PlayerRank { Novice, Adept, Expert, Master }

class Player
{
    public string Name { get; set; }
    public int Experience { get; set; }

    public PlayerRank Rank
    {
        get
        {
            if (Experience < 1000) return PlayerRank.Novice;
            if (Experience < 5000) return PlayerRank.Adept;
            if (Experience < 10000) return PlayerRank.Expert;
            return PlayerRank.Master;
        }
    }

    public Player(string name, int exp)
    {
        Name = name;
        Experience = exp;
    }

    public void GainExperience(int amount)
    {
        Experience += amount;
    }
}


// Задача 16

class Movie
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int DurationMinutes { get; set; }
    public double Rating { get; set; }

    public Movie(string title, string genre, int duration, double rating)
    {
        Title = title;
        Genre = genre;
        DurationMinutes = duration;
        Rating = rating;
    }
}

class MovieLibrary
{
    private static List<Movie> allMovies = new List<Movie>();
    private List<Movie> localMovies = new List<Movie>();

    public static int TotalMoviesCount
    {
        get { return allMovies.Count; }
    }

    public void AddMovie(Movie m)
    {
        localMovies.Add(m);
        allMovies.Add(m);
    }

    public List<Movie> GetMoviesByGenre(string genre)
    {
        List<Movie> result = new List<Movie>();
        foreach (var m in localMovies)
            if (m.Genre == genre)
                result.Add(m);
        return result;
    }

    public static List<Movie> GetGlobalTopRated(int count)
    {
        List<Movie> sorted = new List<Movie>(allMovies);
        sorted.Sort((a, b) => b.Rating.CompareTo(a.Rating));
        return sorted.GetRange(0, Math.Min(count, sorted.Count));
    }
}


// Задача 17


class Temperature
{
    private double _celsius;

    public double Celsius
    {
        get { return _celsius; }
        set { _celsius = value; }
    }

    public double Fahrenheit
    {
        get { return _celsius * 9 / 5 + 32; }
        set { _celsius = (value - 32) * 5 / 9; }
    }

    public double Kelvin
    {
        get { return _celsius + 273.15; }
        set { _celsius = value - 273.15; }
    }

    public Temperature() { _celsius = 0; }
    public Temperature(double c) { _celsius = c; }

    public Temperature(string input)
    {
        string[] parts = input.Split(' ');
        double value = double.Parse(parts[0]);

        if (parts[1] == "C") _celsius = value;
        if (parts[1] == "F") _celsius = (value - 32) * 5 / 9;
    }

    public override string ToString()
    {
        return Celsius + "°C (" + Fahrenheit + "°F, " + Kelvin + "K)";
    }
}


// Задача 18

enum ReservationStatus { Pending, Confirmed, Cancelled, Completed }

class Reservation
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public int TableNumber { get; set; }
    public DateTime DateTime { get; set; }

    private ReservationStatus _status;

    public ReservationStatus Status
    {
        get { return _status; }
        private set { _status = value; }
    }

    public Reservation(int id, string name, int table, DateTime date)
    {
        Id = id;
        CustomerName = name;
        TableNumber = table;
        DateTime = date;
        _status = ReservationStatus.Pending;
    }

    public void Confirm()
    {
        if (_status == ReservationStatus.Pending)
            _status = ReservationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (_status != ReservationStatus.Completed)
            _status = ReservationStatus.Cancelled;
    }

    public void MarkAsCompleted()
    {
        if (_status == ReservationStatus.Confirmed)
            _status = ReservationStatus.Completed;
    }
}


// Задача 19

abstract class Shape
{
    public abstract double Area { get; }
    public abstract double Perimeter { get; }

    public virtual void PrintInfo()
    {
        Console.WriteLine("Площадь: " + Area);
        Console.WriteLine("Периметр: " + Perimeter);
    }
}

class Circle : Shape
{
    public double Radius { get; set; }
    public Circle(double r) { Radius = r; }

    public override double Area => Math.PI * Radius * Radius;
    public override double Perimeter => 2 * Math.PI * Radius;
}

class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }

    public Rectangle(double w, double h)
    {
        Width = w;
        Height = h;
    }

    public override double Area => Width * Height;
    public override double Perimeter => 2 * (Width + Height);
}

class Triangle : Shape
{
    public double A, B, C;

    public Triangle(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    public override double Perimeter => A + B + C;

    public override double Area
    {
        get
        {
            double p = Perimeter / 2;
            return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
        }
    }
}

class ShapeCollection
{
    private List<Shape> shapes = new List<Shape>();

    public void AddShape(Shape shape)
    {
        shapes.Add(shape);
    }

    public void PrintAllAreas()
    {
        foreach (var s in shapes)
            Console.WriteLine("Площадь: " + s.Area);
    }

    public double GetTotalPerimeter()
    {
        double total = 0;
        foreach (var s in shapes)
            total += s.Perimeter;
        return total;
    }
}
// Задача 20
enum PrivacyLevel { Public, FriendsOnly, Private }

class UserProfile
{
    public string Username { get; set; }
    public DateTime BirthDate { get; set; }
    public int FriendsCount { get; set; }
    public int PostsCount { get; set; }
    public PrivacyLevel Privacy { get; set; }

    public int Age
    {
        get
        {
            int age = DateTime.Now.Year - BirthDate.Year;
            if (BirthDate > DateTime.Now.AddYears(-age)) age--;
            return age;
        }
    }

    public UserProfile(string name, DateTime birth)
    {
        Username = name;
        BirthDate = birth;
        FriendsCount = 0;
        PostsCount = 0;
        Privacy = PrivacyLevel.Public;
    }

    public UserProfile(string name, DateTime birth, int friends, int posts)
    {
        Username = name;
        BirthDate = birth;
        FriendsCount = friends;
        PostsCount = posts;
        Privacy = PrivacyLevel.Public;
    }

    public void AddFriend() { FriendsCount++; }
    public void AddPost() { PostsCount++; }

    public string GetPublicInfo()
    {
        if (Privacy == PrivacyLevel.Public)
            return Username + ", возраст: " + Age + ", друзей: " + FriendsCount + ", постов: " + PostsCount;

        if (Privacy == PrivacyLevel.FriendsOnly)
            return Username + ", возраст: " + Age;

        return Username;
    }
}

class SocialNetwork
{
    private List<UserProfile> users = new List<UserProfile>();

    public void AddUser(UserProfile user)
    {
        users.Add(user);
    }

    public UserProfile FindMostActiveUser()
    {
        UserProfile best = null;
        double max = 0;

        foreach (var u in users)
        {
            if (u.FriendsCount == 0) continue;

            double ratio = (double)u.PostsCount / u.FriendsCount;

            if (ratio > max)
            {
                max = ratio;
                best = u;
            }
        }

        return best;
    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine("Все задачи 11–20 находятся в одном файле.");
    }
}
