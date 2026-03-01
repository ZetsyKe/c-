using System;
using System.Collections.Generic;

// Задача 1

class MathConstants
{
    public const double Pi = 3.1415926535;
    public static readonly double E = 2.7182818284;
}


// Задача 2

class Book
{
    public readonly string Title;
    public string Author; // будет изменено в задаче 3
    public int Pages;

    public Book(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
    }

    public void PrintInfo()
    {
        if (Author == null)
            Console.WriteLine($"Название: {Title}, Автор: Автор неизвестен, Страниц: {Pages}");
        else
            Console.WriteLine($"Название: {Title}, Автор: {Author}, Страниц: {Pages}");
    }
}


// Задача 4

class Library
{
    public const int MaxBooks = 1000;

    private Book[] _catalog;
    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Library()
    {
        _catalog = new Book[MaxBooks];
    }

    public void AddBook(Book book)
    {
        for (int i = 0; i < _catalog.Length; i++)
        {
            if (_catalog[i] == null)
            {
                _catalog[i] = book;
                break;
            }
        }
    }
}


// Задача 5

class Student
{
    public string Name { get; }
    private string _email;
    public string Email
    {
        get { return _email; }
        set
        {
            if (value == null)
            {
                _email = null;
                return;
            }

            if (value.Contains("@"))
                _email = value;
        }
    }

    private List<int> _grades;
    public List<int> Grades
    {
        get { return _grades; }
    }

    public Student(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Имя не может быть пустым");

        Name = name;
        _grades = new List<int>();
    }
}


// Задача 6

class Product
{
    public readonly int Id;
    public string Name { get; }
    public decimal Price;

    public Product(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}

class Customer
{
    public int Id { get; }
    public string FullName;
    public string Address;

    public Customer(int id, string fullName, string address)
    {
        Id = id;
        FullName = fullName;
        Address = address;
    }
}

class Order
{
    public int OrderId { get; }
    public Customer Customer { get; }
    private List<Product> _products;

    public List<Product> Products
    {
        get { return _products; }
    }

    public Order(int orderId, Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException("Customer не может быть null");

        OrderId = orderId;
        Customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product p)
    {
        if (p != null)
            _products.Add(p);
    }

    public decimal TotalPrice
    {
        get
        {
            decimal total = 0;
            foreach (var product in _products)
                total += product.Price;

            return total;
        }
    }
}


// Main

class Program
{
    static void Main()
    {
        // Проверка задачи 1
        Console.WriteLine("Pi = " + MathConstants.Pi);
        Console.WriteLine("E = " + MathConstants.E);

        Console.WriteLine();

        // Проверка задач 2 и 3
        Book book1 = new Book("1984", "George Orwell", 300);
        Book book2 = new Book("Unknown Book", null, 150);

        book1.PrintInfo();
        book2.PrintInfo();

        Console.WriteLine();

        // Проверка задачи 4
        Library library = new Library();
        library.Name = "City Library";
        library.AddBook(book1);

        Console.WriteLine("Библиотека: " + library.Name);

        Console.WriteLine();

        // Проверка задачи 5
        Student student = new Student("Иван");
        student.Email = "ivan@mail.com";
        student.Grades.Add(5);
        student.Grades.Add(4);

        Console.WriteLine("Студент: " + student.Name);
        Console.WriteLine("Email: " + student.Email);

        Console.WriteLine();

        // Проверка задачи 6
        Customer customer = new Customer(1, "Петр Иванов", null);
        Product p1 = new Product(1, "Ноутбук", 50000);
        Product p2 = new Product(2, "Мышь", 1500);

        Order order = new Order(1001, customer);
        order.AddProduct(p1);
        order.AddProduct(p2);

        Console.WriteLine("Сумма заказа: " + order.TotalPrice);
    }
}
