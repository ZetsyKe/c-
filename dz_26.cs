using System;

class Program
{
    // 1
    delegate double MathOperation(double a, double b);

    static double Add(double a, double b)
    {
        return a + b;
    }

    static double Multiply(double a, double b)
    {
        return a * b;
    }

    // 2
    delegate string StringModifier(string text);

    static string ToUpperCase(string text)
    {
        return text.ToUpper();
    }

    static string ToReverse(string text)
    {
        char[] arr = text.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    // ===================== 3. Фильтрация массива =====================
    delegate bool IsPositive(int x);

    static bool Check(int x)
    {
        return x > 0;
    }

    static void Filter(int[] numbers, IsPositive checker)
    {
        foreach (int num in numbers)
        {
            if (checker(num))
            {
                Console.Write(num + " ");
            }
        }
        Console.WriteLine();
    }

    // 5
    delegate void DisplayInfo();

    static void Method1()
    {
        Console.WriteLine("Метод 1");
    }

    static void Method2()
    {
        Console.WriteLine("Метод 2");
    }

    static void Method3()
    {
        Console.WriteLine("Метод 3");
    }

    static void Main()
    {
        // 1
        MathOperation op;

        op = Add;
        Console.WriteLine("Сложение: " + op(3, 4));

        op = Multiply;
        Console.WriteLine("Умножение: " + op(3, 4));

        // 2
        StringModifier mod;
        string input = "hello";

        mod = ToUpperCase;
        Console.WriteLine(mod(input));

        mod = ToReverse;
        Console.WriteLine(mod(input));

        // 3
        int[] arr = { -3, 5, -1, 8, 0 };
        Filter(arr, Check);

        // 4
        Account acc = new Account();

        acc.notify = EmailLogger;
        acc.notify += SmsLogger;

        acc.ChangeBalance(100);

        // 5
        DisplayInfo info = Method1;
        info += Method2;
        info += Method3;

        info();
    }

    // 4
    static void EmailLogger(string message)
    {
        Console.WriteLine("Email: " + message);
    }

    static void SmsLogger(string message)
    {
        Console.WriteLine("SMS: " + message);
    }
}

class Account
{
    public delegate void Notify(string message);
    public Notify notify;

    private double balance;

    public void ChangeBalance(double amount)
    {
        balance += amount;
        notify?.Invoke("Баланс изменен: " + balance);
    }
}
